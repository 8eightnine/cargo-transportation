using cargo_transportation.Classes;
using cargo_transportation.Classes.Hash;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace cargo_transportation
{
    public partial class AuthForm : Form
    {
        private string _login;
        private string _password;
        private bool isAuthorized;
        private string filename = "Databases\\users.db";
        private SQLiteConnection con;

        public AuthForm()
        {
            InitializeComponent();
            con = DatabaseWorker.Connect(filename);
            if (con == null)
            {
                this.Close();
                return;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT * FROM Users";
            cmd.Connection = con;
            SQLiteDataAdapter da = DatabaseWorker.GetDataAdapter(dt, cmd);

            var tmp = (from x in dt.AsEnumerable() where (x["Username"].ToString() == _login && x["Password"].ToString() == _password) select x).FirstOrDefault();

            
                // TODO: close this window and initialize main window
              MessageBox.Show($"Username: {tmp["Username"]}\nPassword: {tmp["Password"]}");
           

            //if (loginBox.Text.Length > 0 && passwordBox.Text.Length > 0)
            //{
            //    loginBox.Text = Hash.hashPassword(passwordBox.Text);
            //}
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(1);
            this.Text = "Регистрация";
        }

        private void switchToLoginLabel_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(0);
            this.Text = "Авторизация";
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // TODO: make the registration possible
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            this._password = Hash.hashPassword(passwordBox.Text);
        }

        private void loginBox_TextChanged(object sender, EventArgs e)
        {
            this._login = Hash.hashPassword(loginBox.Text);
        }
    }
}
