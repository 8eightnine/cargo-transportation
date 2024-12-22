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
    public partial class CargoListForm : Form
    {
        int id = -1;
        public CargoListForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void CargoListForm_Load(object sender, EventArgs e)
        {
            DataTable clientData = new DataTable();
            string command = $"SELECT CAST(CargoID as varchar(10)) as CargoID, Quantity, InsuranceCost FROM Cargo_List WHERE OrderID = {id}";
            Database.ReadData("Databases\\make.db", command, clientData);
            dataGridView1.DataSource = clientData;
            foreach (DataGridViewRow dr in dataGridView1.Rows) 
            {
                string name = Database.ReadSingleString("Databases\\make.db", $"SELECT Name FROM 'Cargo' WHERE ID = {Int32.Parse(dr.Cells[0].Value.ToString())};");
                dr.Cells[0].Value = name;
            }
            dataGridView1.Columns[0].HeaderText = "Товар";
            dataGridView1.Columns[2].HeaderText = "Страховая стоимость";
            dataGridView1.Columns[1].HeaderText = "Количество";
            dataGridView1.Refresh();
        }
    }
}
