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
    public partial class NewListForm : Form
    {
        private CargoList _cargoList;
        public NewListForm(CargoList newList)
        {
            InitializeComponent();
            _cargoList = newList;
            FillForm();
        }

        private void FillForm()
        {
            if (_cargoList._isNew != 1)
            {
                insuranceBox.Text = _cargoList.InsuranceCost;
                quantityBox.Text = _cargoList.Quantity.ToString();
            }

            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Order'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                orderIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString() + " - " + dr[2].ToString()
                });
            }
            dt.Dispose();
            if (_cargoList._isNew != 1 && _cargoList.OrderID != 0)
                orderIDComboBox.SelectedIndex = _cargoList.CargoID - 1;

            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Cargo'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                cargoIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            if (_cargoList._isNew != 1 && _cargoList.CargoID != 0)
                cargoIDComboBox.SelectedIndex = _cargoList.CargoID - 1;
        }

        private int CheckValidity()
        {
            if (insuranceBox.Text.Length != 0)
            {
                foreach (char c in insuranceBox.Text)
                {
                    if (!char.IsDigit(c))
                        throw new Exception("Поле 'Страховая стоимость' содержит недопустимые символы");
                }
            }
            else
                throw new Exception("Поле 'Страховая стоимость' не может быть пустым");

            ValidityChecker.CheckIfIntValid(quantityBox.Text, "Количество");

            return 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveEntry();
        }

        private void SaveEntry()
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_cargoList._isNew != 1)
                    {
                        string command = $"UPDATE 'Cargo_List' SET OrderId = {_cargoList.OrderID}, CargoID = {_cargoList.CargoID}, InsuranceCost = '{insuranceBox.Text}', Quantity = {Int32.Parse(quantityBox.Text)} WHERE ID = {_cargoList.ID}";
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
            string command = $"INSERT INTO 'Cargo_List'(OrderId, CargoId, InsuranceCost, Quantity) VALUES ({_cargoList.OrderID}, {_cargoList.CargoID}, {insuranceBox.Text}, {Int32.Parse(quantityBox.Text)});";
            Database.WriteData("Databases\\make.db", command);
            MessageBox.Show("Запись сохранена");
        }

        private void orderIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (orderIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _cargoList.OrderID = selectedItem.Id;
            }
        }

        private void cargoIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargoIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _cargoList.CargoID = selectedItem.Id;
            }
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