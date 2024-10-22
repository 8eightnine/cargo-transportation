using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace cargo_transportation
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private DataTable _dataTable;
        private string _currentModule;
        public ToolStripItemCollection DefaultControls;

        public MainForm(User user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramMenu menu = new ProgramMenu();
            usernameLabel.Text = "Добро пожаловать, " + currentUser.Login + "!";
            ToolStripItemCollection temp = menu.Populate();
            int size = temp.Count;
            for (int i = size - 1; i >= 0; i--)
            {
                menuStrip1.Items.Add(temp[i]);
            }
            DefaultControls = menu.Populate();

            // TODO: wrap it into a click
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source='Databases\\test.db';Version=3; FailIfMissing=False"))
                {
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM 'Order'";
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void ChangeStatusStrip(string status)
        {
            databaseStatusLabel.Text = "Статус базы данных: " + status;
        }
    }
}
