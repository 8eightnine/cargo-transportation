using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;
using Cargo.Classes;

namespace Orders
{
    public partial class OrderForm : Form
    {
        // Working variables
        Order currentOrder;
        public static CargoItem[] fullCargoList;

        static CargoItem[] GetCargoValues()
        {
            DataTable cargoData = new DataTable();
            string command = $"SELECT * FROM Cargo";
            Database.ReadData("Databases\\make.db", command, cargoData);
            CargoItem[] temp = new CargoItem[cargoData.Rows.Count];
            for (int i = 0; i < cargoData.Rows.Count; i++)
            {
                temp[i] = new CargoItem();
                temp[i].ID = Int32.Parse(cargoData.Rows[i].ItemArray[0].ToString());
                temp[i].Name = cargoData.Rows[i].ItemArray[1].ToString();
                temp[i].Unit = cargoData.Rows[i].ItemArray[2].ToString();
                temp[i].Weight = Int32.Parse(cargoData.Rows[i].ItemArray[3].ToString());
            }
            cargoData.Dispose();
            return temp;
        }

        public OrderForm(Order order)
        {
            InitializeComponent();
            currentOrder = order;
            fullCargoList = GetCargoValues();
            FillForm(order);
        }

        private void FillForm(Order order)
        {
            if (order._isNew == 0)
            {
                #region filling the values
                this.Text = $"Заказ № {order.ID}";
                dateTimePicker1.Value = order.OrderDate;
                senderNameBox.Text = order.SenderName;
                senderAddressBox.Text = order.SenderAddress;
                clientIDBox.Text = order.ClientID.ToString();
                deliveryAddressBox.Text = order.ClientAddress;
                tripCostBox.Text = order.Cost.ToString();
                tripLengthBox.Text = order.TripLength.ToString();
                tripIDBox.Text = order.TripID.ToString();
                #endregion
                FillCargoList();
            }
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (currentOrder._isNew == 0)
                    {
                        string command = $"UPDATE 'Order' SET " +
                            $"Date = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"Sender = '{senderNameBox.Text}', " +
                            $"SenderAddress = '{senderAddressBox.Text}', " +
                            $"Client = {Int32.Parse(clientIDBox.Text)}, " +
                            $"RecipientAddress = '{deliveryAddressBox.Text}', " +
                            $"TripLength = {Int32.Parse(tripLengthBox.Text)}, " +
                            $"Cost = {Int32.Parse(tripCostBox.Text)}, " +
                            $"Trip = {Int32.Parse(tripIDBox.Text)} " +
                            $"WHERE ID = {currentOrder.ID}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewOrder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private void CreateNewOrder()
        {
            string command = $"INSERT INTO 'Order' (Date, Sender, SenderAddress, Client, RecipientAddress, TripLength, Cost, Trip) VALUES" +
                            $"('{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"'{senderNameBox.Text}', " +
                            $"'{senderAddressBox.Text}', " +
                            $"{Int32.Parse(clientIDBox.Text)}, " +
                            $"'{deliveryAddressBox.Text}', " +
                            $"{Int32.Parse(tripLengthBox.Text)}, " +
                            $"{Int32.Parse(tripCostBox.Text)}, " +
                            $"{Int32.Parse(tripIDBox.Text)})";
            Database.WriteData("Databases\\make.db", command, null);
            MessageBox.Show("Запись сохранена");
        }

        private void FillCargoList()
        {
            DataTable cargoList = new DataTable();
            string command = $"SELECT CAST(CargoID as varchar(10)) as CargoID, Quantity, InsuranceCost FROM Cargo_List WHERE OrderID = {currentOrder.ID}";
            Database.ReadData("Databases\\make.db", command, cargoList);
            CargoListGridView.DataSource = cargoList;
            CargoListGridView.Columns[0].HeaderText = "Название товара";
            CargoListGridView.Columns[1].HeaderText = "Количество";
            CargoListGridView.Columns[2].HeaderText = "Стоимость";

            CargoListGridView.Refresh();

            foreach (DataGridViewRow dr in CargoListGridView.Rows)
            {
                dr.Cells[0].Value = fullCargoList.Where(item => item.ID == Int32.Parse(dr.Cells[0].Value.ToString())).FirstOrDefault().Name;
            }
        }


        private int CheckValidity()
        {
            if (senderNameBox.Text.Length == 0)
                throw new Exception("Поле 'ФИО отправителя' не может быть пустым");

            if (senderAddressBox.Text.Length == 0)
                throw new Exception("Поле 'Адрес отправителя' не может быть пустым");

            if (deliveryAddressBox.Text.Length == 0)
                throw new Exception("Поле 'Адрес доставки' не может быть пустым");

            if (clientIDBox.Text.Length != 0)
            {
                int temp;
                if (Int32.TryParse(clientIDBox.Text, out temp) == false)
                    throw new Exception("Поле 'ID клиента-получателя' содержит неккоректное значение");
            }
            else throw new Exception("Поле 'ID клиента-получателя' не может быть пустым");

            if (tripCostBox.Text.Length != 0)
            {
                int temp;
                if (Int32.TryParse(tripCostBox.Text, out temp) == false)
                    throw new Exception("Поле 'Стоимость заказа' содержит неккоректное значение");
                
            }
            else throw new Exception("Поле 'Стоимость заказа' не может быть пустым");

            if (tripLengthBox.Text.Length != 0)
            {
                int temp;
                if (Int32.TryParse(tripLengthBox.Text, out temp) == false)
                    throw new Exception("Поле 'Длина поездки' содержит неккоректное значение");

            }
            else throw new Exception("Поле 'Длина поездки' не может быть пустым");

            if (tripIDBox.Text.Length != 0)
            {
                int temp;
                if (Int32.TryParse(tripIDBox.Text, out temp) == false)
                    throw new Exception("Поле 'ID поездки' содержит неккоректное значение");
                if (temp != currentOrder.TripID)
                {
                    DataTable dt = new DataTable();
                    string command = $"SELECT CASE WHEN NOT EXISTS (SELECT 1 FROM Trip WHERE ID = {temp}) THEN -1 WHEN EXISTS (SELECT 1 FROM \"Order\" WHERE Trip = {temp}) THEN 0 ELSE 1 END AS Result;";
                    int result = Database.ReadSingleInt("Databases\\make.db", command);
                    switch (result)
                    {
                        case -1: { throw new Exception("Поездки с таким ID не существует"); break; }
                        case 0: { throw new Exception("Поездка с таким ID уже используется для другого заказа"); break; }
                        default: { break; }
                    }
                }
            }
            else throw new Exception("Поле 'ID поездки' не может быть пустым");

            return 1;
        }
    } 
}
