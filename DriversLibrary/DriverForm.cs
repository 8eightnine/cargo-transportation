using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace Drivers
{
    
    public partial class DriverForm : Form
    {
        Driver _driver;
        private int _categoryID = -1;
        private int _classID = -1;
        public DriverForm(Driver driver)
        {
            InitializeComponent();
            _driver = driver;
            FillForm();
        }

        private void FillForm()
        {
            if (_driver._isNew != 1)
            {
                fullNameBox.Text = _driver.Name;
                tableNumberBox.Text = _driver.TableNumber.ToString();
                dateTimePicker1.Value = _driver.DateOfBirth;
                this.Name = "Водитель № " + _driver.Id;
                experienceBox.Text = _driver.Experience.ToString();
            }
            FillClassAndCategory();
            

            //TODO: сделать справочник категорий и классов при изменении значения видимым
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_driver._isNew == 0)
                    {
                        string command = $"UPDATE 'Driver' SET " +
                            $"FullName = '{fullNameBox.Text}', " +
                            $"TableNumber = {Int32.Parse(tableNumberBox.Text)}, " +
                            $"DateOfBirth = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"Experience = {Int32.Parse(experienceBox.Text)}, " +
                            $"Category = {_categoryID}, " +
                            $"Class = {_classID} " +
                            $"WHERE ID = {_driver.Id}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewDriver();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private void CreateNewDriver()
        {
            string command = $"INSERT INTO 'Driver' ( FullName, TableNumber, DateOfBirth, Experience, Category, Class) VALUES (" +
                            $"'{fullNameBox.Text}', " +
                            $"{Int32.Parse(tableNumberBox.Text)}, " +
                            $"'{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"{Int32.Parse(experienceBox.Text)}, " +
                            $"{_categoryID}, " +
                            $"{_classID});";
            Database.WriteData("Databases\\make.db", command, null);
            MessageBox.Show("Запись сохранена");
        }

        private int CheckValidity()
        {
            ValidityChecker.CheckIfStringValid(fullNameBox.Text, "ФИО");
            ValidityChecker.CheckIfIntValid(experienceBox.Text, "Стаж");

            if (categoryComboBox.SelectedIndex == -1)
                throw new Exception("Поле 'Категория' не может быть пустым");
            
            if (classComboBox.SelectedIndex == -1)
                throw new Exception("Поле 'Класс' не может быть пустым");

            if (tableNumberBox.Text.Length != 0)
            {
                foreach (char c in tableNumberBox.Text)
                {
                    if ((c <= '9' && c >= '0') || c == '.')
                    {

                    }
                    else throw new Exception("Поле 'Паспорт' содержит недопустимые символы");
                }

                int temp;
                if (Int32.TryParse(tableNumberBox.Text, out temp) == false)
                    throw new Exception("Поле 'Табельный номер' содержит некорректное значение");

                string command = $"SELECT CASE WHEN EXISTS ( SELECT 1 FROM Driver WHERE TableNumber = {temp} ) THEN 0 ELSE 1 END AS Result;";
                int result = Database.ReadSingleInt("Databases\\make.db", command);
                if (result == 0 && _driver.TableNumber != Int32.Parse(tableNumberBox.Text))
                {
                    throw new Exception("Значение поля 'Табельный номер' уже есть в системе, используйте уникальное значение");
                }
            }
            else throw new Exception("Поле 'Табельный номер' не может быть пустым");

            if (dateTimePicker1.Value > DateTime.Now)
            {
                throw new Exception("Поле 'Дата рождения' содержит недопустимое значение");
            }

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveEntry(sender, e);
        }

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _categoryID = selectedItem.Id;
            }
        }

        private void classComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _classID = selectedItem.Id;
            }
        }
        
        private void FillClassAndCategory()
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Category_List'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                categoryComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt.Dispose();
            categoryComboBox.SelectedIndex = _driver.Category - 1;
            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Class_List'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                classComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            classComboBox.SelectedIndex = _driver.Class - 1;
        }
    }
}
public class ComboBoxItem
{
    public int Id { get; set; }
    public string DisplayText { get; set; }

    public override string ToString()
    {
        return DisplayText;
    }
}