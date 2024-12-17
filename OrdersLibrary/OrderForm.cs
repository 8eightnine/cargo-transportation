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
            #region DOGSHIT
            this.Text = $"Заказ № {order.ID}";
            dateTimePicker1.Value = order.OrderDate;
            senderNameBox.Text = order.SenderName;
            senderAddressBox.Text = order.SenderAddress;
            clientIDBox.Text = order.ClientID.ToString();
            deliveryAddressBox.Text = order.ClientAddress;
            tripCostBox.Text = order.Cost.ToString();
            tripLengthBox.Text = order.TripLength.ToString();
            tripIDBox.Text= order.TripID.ToString();
            #endregion

            DataTable cargoList = new DataTable();

            // Populating the Cargo List Grid View -- TODO: make it a separate function
            if (order.CargoListID != 0)
            {
                string command = $"SELECT CAST(CargoID as varchar(10)) as CargoID, Quantity, InsuranceCost FROM Cargo_List WHERE ID = {order.CargoListID}";
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
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
