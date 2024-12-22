using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cargo;
using cargo_transportation.Classes;

namespace Orders
{
    public partial class OrderForm : Form
    {

        Order currentOrder;
        OrderClient client = new OrderClient();
        private static int client_is_new = 0;

        public OrderForm(Order order)
        {
            InitializeComponent();
            currentOrder = order;
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
                deliveryAddressBox.Text = order.ClientAddress;
                costBox.Text = order.Cost.ToString();
                tripLengthBox.Text = order.TripLength.ToString();
                #endregion

            }
            FillClientAndTrip();
            FillPhysAndCompanyBoxes();
        }

        private void FillClientAndTrip()
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Trip'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                tripIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[0].ToString() + " - " + dr[2].ToString()
                });
            }
            dt.Dispose();
            if (currentOrder._isNew != 1)
                tripIDComboBox.SelectedIndex = currentOrder.TripID - 1;

            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Client'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                clientIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            if (currentOrder._isNew != 1)
                clientIDComboBox.SelectedIndex = currentOrder.ClientID - 1;
        }

        private void showCargoList_Click(object sender, EventArgs e)
        {
            CargoListForm cargoListForm = new CargoListForm(currentOrder.ID);
            cargoListForm.Show();
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
                    if (client_is_new == 1)
                    {
                        client._phoneNumber = phoneNumberBox.Text;
                        client._fullName = clientNameBox.Text;
                        CreateNewClient();
                    }

                    string command = "";
                    if (currentOrder._isNew != 1)
                    {
                        command = $"UPDATE 'Order' SET " +
                            $"Date = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"Sender = '{senderNameBox.Text}', " +
                            $"SenderAddress = '{senderAddressBox.Text}', " +
                            $"Client = {client._id}, " +
                            $"RecipientAddress = '{deliveryAddressBox.Text}', " +
                            $"TripLength = {Int32.Parse(tripLengthBox.Text)}, " +
                            $"Cost = {Int32.Parse(costBox.Text)}, " +
                            $"Trip = {currentOrder.TripID} " +
                            $"WHERE ID = {currentOrder.ID}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                    {
                        command = $"INSERT INTO 'Order' (Date, Sender, SenderAddress, Client, RecipientAddress, TripLength, Cost, Trip) VALUES" +
                            $"('{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"'{senderNameBox.Text}', " +
                            $"'{senderAddressBox.Text}', " +
                            $"{client._id}, " +
                            $"'{deliveryAddressBox.Text}', " +
                            $"{Int32.Parse(tripLengthBox.Text)}, " +
                            $"{Int32.Parse(costBox.Text)}, " +
                            $"{currentOrder.TripID})";
                        Database.WriteData("Databases\\make.db", command);
                        MessageBox.Show("Запись сохранена");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private int CheckValidity()
        {
            ValidityChecker.CheckIfStringValid(senderNameBox.Text, "ФИО отправителя");
            ValidityChecker.CheckIfStringValid(senderAddressBox.Text, "Адрес отправителя");
            ValidityChecker.CheckIfStringValid(deliveryAddressBox.Text, "Адрес доставки");
            ValidityChecker.CheckIfIntValid(tripLengthBox.Text, "Длина поездки");
            ValidityChecker.CheckIfIntValid(costBox.Text, "Стоимость заказа");

            if (tripIDComboBox.SelectedIndex != -1)
            {
                DataTable dt = new DataTable();
                string command = $"SELECT CASE WHEN NOT EXISTS (SELECT 1 FROM Trip WHERE ID = {currentOrder.TripID}) THEN -1 WHEN EXISTS (SELECT 1 FROM \"Order\" WHERE Trip = {currentOrder.TripID}) THEN 0 ELSE 1 END AS Result;";
                int result = Database.ReadSingleInt("Databases\\make.db", command);
                switch (result)
                {
                    case 0: { throw new Exception("Поездка с таким ID уже используется для другого заказа"); }
                    default: { break; }
                }
            }
            else throw new Exception("Поле 'Поездка' не может быть пустым");

            ValidityChecker.CheckIfStringValid(clientNameBox.Text, "ФИО клиента");
            ValidityChecker.CheckPhone(phoneNumberBox.Text, "Номер телефона клиента");

            return 1;
        }

        private void CreateNewClient()
        {
            string command = $"INSERT INTO 'Client' (FullName, PhoneNumber, PhysPersonID, CompanyID) VALUES " +
                $"(@Value1, " +
                $"@Value2, " +
                $"@Value3, " +
                $"@Value4);";
                
            var parameters = new Dictionary<string, object>
            {
                { "@Value1", $"{clientNameBox.Text}"},
                { "@Value2", $"{phoneNumberBox.Text}"},
                { "@Value3", null},
                { "@Value4", null}
            };
            if (client._physPersonID != 0)
                parameters["@Value3"] = $"{client._physPersonID}";
            if (client._companyPersonID != 0)
                parameters["@Value4"] = $"{client._companyPersonID}";

            Database.WriteData("Databases\\make.db", command, parameters);
        }

        private void tripIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tripIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                currentOrder.TripID = selectedItem.Id;
            }
        }

        private void clientIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                currentOrder.ClientID = selectedItem.Id;
                DataTable dt = new DataTable();
                Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Client' WHERE ID = {currentOrder.ClientID}", dt);
                FillClientInfo(dt.Rows[0]);
                dt.Dispose();
            }
        }

        private void FillClientInfo(DataRow dr)
        {
            client = OrderClient.ParseToClient(dr);
            clientNameBox.Text = client._fullName;
            phoneNumberBox.Text = client._phoneNumber;
            FillPhysAndCompanyBoxes();
        }

        private void FillPhysAndCompanyBoxes()
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Phys_Person'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                physPersonComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt.Dispose();
            if ((currentOrder._isNew == 1 && currentOrder.ClientID > 0) || currentOrder._isNew != 1)
                physPersonComboBox.SelectedIndex = client._physPersonID - 1;

            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Client'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                companyPersonComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            if ((currentOrder._isNew == 1 && currentOrder.ClientID > 0) || currentOrder._isNew != 1)
                companyPersonComboBox.SelectedIndex = client._companyPersonID - 1;
        }

        private void physPersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (physPersonComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                client._physPersonID = selectedItem.Id;
            }
        }

        private void companyPersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (companyPersonComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                client._companyPersonID = selectedItem.Id;
            }
        }

        private void createNewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (createNewCheckBox.Checked)
            {
                clientIDComboBox.Visible = false;
                label11.Visible = false;
                client_is_new = 1;
            }
            else
            {
                clientIDComboBox.Visible = true;
                label11.Visible = true;
                client_is_new = 0;
            }
        }
    }
}
