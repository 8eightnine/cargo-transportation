using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace Cargo
{
    public partial class NewCargoForm : Form
    {
        private CargoItem _currentItem;
        public NewCargoForm(CargoItem newItem)
        {
            InitializeComponent();
            _currentItem = newItem;
            FillForm();
        }

        private void FillForm()
        {
            if (_currentItem._isNew != 1)
            {
                nameBox.Text = _currentItem.Name;
                textBox1.Text = _currentItem.Unit;
                textBox2.Text = _currentItem.Weight.ToString();
            }
        }

        private void SaveEntry()
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_currentItem._isNew != 1)
                    {
                        string command = $"UPDATE 'Cargo' SET Name = '{nameBox.Text}', Unit = '{textBox1.Text}', Weight = {Int32.Parse(textBox2.Text)} WHERE ID = {_currentItem.ID};";
                        Database.WriteData("Databases\\make.db", command);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewEntry();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateNewEntry()
        {
            string command = $"INSERT INTO 'Cargo' (Name, Unit, Weight) VALUES ('{nameBox.Text}', '{textBox1.Text}', {Int32.Parse(textBox2.Text)});";
            Database.WriteData("Databases\\make.db", command);
            MessageBox.Show("Запись сохранена");
        }

        private int CheckValidity()
        {
            if (nameBox.Text.Length == 0)
                throw new Exception("Поле 'Название' не может быть пустым");
            if (textBox1.Text.Length == 0)
                throw new Exception("Поле 'Единица измерения' не может быть пустым");
            ValidityChecker.CheckIfIntValid(textBox2.Text, "Вес");

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveEntry();
        }
    }
}
