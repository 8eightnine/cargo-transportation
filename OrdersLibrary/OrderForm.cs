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

namespace Orders
{
    public partial class OrderForm : Form
    {
        // Working variables
        Order currentOrder;
        
        public OrderForm(Order order)
        {
            InitializeComponent();
            currentOrder = order;
            FillForm(order);
        }

        private void FillForm(Order order)
        {
            this.Text = $"Заказ № {order.ID}";
            dateTimePicker1.Value = order.OrderDate;
            senderNameBox.Text = order.SenderName;
            senderAddressBox.Text = order.SenderAddress;
            clientIDBox.Text = order.ClientID.ToString();
            deliveryAddressBox.Text = order.ClientAddress;
            tripCostBox.Text = order.Cost.ToString();
            tripLengthBox.Text = order.TripLength.ToString();
            tripIDBox.Text= order.TripID.ToString();


            DataTable cargoList = new DataTable();
            string command = $"SELECT * FROM Cargo_List";
            Database.ReadData("Databases\\make.db", command, cargoList);
            int count = cargoList.Rows.Count;
            for (int i = 1; i < count + 1; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }

            // Populating the Cargo List Grid View -- TODO: make it a separate function
            if (order.CargoListID != 0)
            {
                cargoList.Rows.Clear();
                cargoList.Columns.RemoveAt(0);
                command = $"SELECT CargoID, InsuranceCost, Quantity FROM Cargo_List WHERE ID = {order.CargoListID}";
                comboBox1.SelectedValue = order.CargoListID.ToString();
                Database.ReadData("Databases\\make.db", command, cargoList);
                CargoListGridView.DataSource = cargoList;
                CargoListGridView.Refresh();
            }

            
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            MessageBox.Show("SAVED");
        }

    }
}
