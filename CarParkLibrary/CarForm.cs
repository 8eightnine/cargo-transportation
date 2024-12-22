using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace CarPark
{
    public partial class CarForm : Form
    {
        private Car _car;
        public CarForm(Car car)
        {
            _car = car;
            InitializeComponent();
            FillForm();
        }

        private void FillForm()
        {
            if (_car._isNew == 0)
            {
                #region filling the values
                this.Text = $"Автомобиль № {_car.ID}";
                stateNumberBox.Text = _car.StateNumber;
                modelTextBox.Text = _car.Model;
                weightLimitBox.Text = _car.WeightLimit.ToString();
                issueDatePicker.Value = _car.IssueDate;
                repairDatePicker.Value = _car.RepairDate;
                mileageBox.Text = _car.Mileage.ToString();
                photoBox.Text = _car.Photo;
                #endregion

                if (_car.Photo.Length > 0)
                {
                    pictureBox.Load(_car.Photo);
                }
            }
            FillBrandAndUsage();
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_car._isNew == 0)
                    {
                        string command = $"UPDATE 'Car_List' SET " +
                            $"StateNumber = '{stateNumberBox.Text}', " +
                            $"BrandID = '{_car.BrandID}', " +
                            $"Model = '{modelTextBox.Text}', " +
                            $"WeightLimit = '{weightLimitBox.Text}', " +
                            $"Usage = '{_car.Usage}', " +
                            $"IssueDate = '{issueDatePicker.Value.ToString("dd.MM.yyyy")}'," +
                            $"RepairDate = '{repairDatePicker.Value.ToString("dd.MM.yyyy")}', " +
                            $"Mileage = '{mileageBox.Text}', " +
                            $"Photo = '{photoBox.Text}' " +
                            $"WHERE ID = {_car.ID}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewCar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private int CheckValidity()
        {
            if (modelTextBox.Text.Length == 0)
                throw new Exception("Поле 'Модель' не может быть пустым");

            if (stateNumberBox.Text.Length != 0)
            {
                // Н392ОА46
                if (stateNumberBox.Text.Length != 8 && stateNumberBox.Text.Length != 9)
                    throw new Exception("Поле 'Государственный номер' содержит недопустимое количество символов");
            }
            else throw new Exception("Поле 'Государственный номер' не может быть пустым");

            if (mileageBox.Text.Length != 0)
            {
                if (!mileageBox.Text.All(char.IsDigit)) {
                    throw new Exception("Поле 'Пробег' содержит недопустимые символы");
                }
            }
            else throw new Exception("Поле 'Пробег' не может быть пустым");

            if (weightLimitBox.Text.Length != 0)
            {
                if (!weightLimitBox.Text.All(char.IsDigit))
                {
                    throw new Exception("Поле 'Лимит веса' содержит недопустимые символы");
                }
            }
            else throw new Exception("Поле 'Лимит веса' не может быть пустым");

            return 1;
        }

        private void CreateNewCar()
        {
            string command = $"INSERT INTO 'Car_List' (StateNumber, BrandID, Model, WeightLimit, Usage, IssueDate, RepairDate, Mileage, Photo) VALUES" +
                            $"('{stateNumberBox.Text}', " +
                            $"'{_car.BrandID}', " +
                            $"'{modelTextBox.Text}', " +
                            $"'{weightLimitBox.Text}', " +
                            $"'{_car.Usage}', " +
                            $"'{issueDatePicker.Value.ToString("dd.MM.yyyy")}', " +
                            $"'{repairDatePicker.Value.ToString("dd.MM.yyyy")}', " +
                            $"'{mileageBox.Text}', " +
                            $"'{photoBox.Text}')";
            Database.WriteData("Databases\\make.db", command, null);
            MessageBox.Show("Запись сохранена");
        }

        private void FillBrandAndUsage()
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Car_Brand'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                brandComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt.Dispose();
            brandComboBox.SelectedIndex = _car.BrandID - 1;

            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Usage_list'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                comboBox1.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            comboBox1.SelectedIndex = _car.Usage - 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                _car.Usage = selectedItem.Id;
            }
        }

        private void brandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brandComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _car.BrandID = selectedItem.Id;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveEntry(sender, e);
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