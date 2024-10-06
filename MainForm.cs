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

namespace cargo_transportation
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private Database _database;
        public MainForm(User user)
        {
            _database = new Database("Databases\\db.db");
            currentUser = user;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            usernameLabel.Text = "Добро пожаловать, " + currentUser.Login + "!";
            ChangeStatusStrip(_database.Status);
            //FillToolStrip();
        }

        private void ChangeStatusStrip(string status)
        {
            databaseStatusLabel.Text = "Статус базы данных: " + status;
        }

        private void FillToolStrip()
        {
            Database temp = new Database("Databases\\menu.db");
            temp.Connect();

            temp.Command = "SELECT * FROM Section";
            var dt = new DataTable();
            var da = temp.GetDataAdapter(dt);
            var reader = _database.ReadData();

            foreach (DataRow dr in dt.Rows)
            {
                reader.GetInt64(0);
            }
        }

    }
}
