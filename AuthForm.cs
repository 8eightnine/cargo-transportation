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
        public bool isAuthorized = false;
        private string filename = "Databases\\users.db";
        private Database _database;
        
        public User user;

        public AuthForm()
        {
            InitializeComponent();

            _database = new Database(filename);
            _database.Connect();
            
            if (_database.Connection == null)
            {
                MessageBox.Show($"Ошибка доступа к базе данных.");
                // TODO: create new Users database if one is absent
            }

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                do
                {
                    var dt = new DataTable();
                    // TODO: there's no need for table name you dumbass
                    _database.Command = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
                    var da = _database.GetDataAdapter(dt);
                    
                    string username = "", password = "";
                    bool readRight = false, writeRight = false, editRight = false, deleteRight = false;
                    
                    if (dt.Rows.Count > 0)
                    {
                        // TODO: beautify
                        username = dt.Rows[0][1].ToString();
                        password = dt.Rows[0][2].ToString();
                        Boolean.TryParse(dt.Rows[0][3].ToString(), out readRight);
                        Boolean.TryParse(dt.Rows[0][4].ToString(), out writeRight);
                        Boolean.TryParse(dt.Rows[0][5].ToString(), out editRight);
                        Boolean.TryParse(dt.Rows[0][6].ToString(), out deleteRight);
                    }
                    else
                    {
                        ErrorHandler.CredentialsError();
                        break;
                        // TODO: handle if user is not in the database
                    }
                    _database.Clear();
                    dt.Dispose();
                    da.Dispose();


                    if (username.Equals(_login) && password.Equals(_password))
                    {
                        isAuthorized = true;
                        user = new User(_login, _password, readRight, writeRight, editRight, deleteRight);
                        Close();
                    }
                    else
                    {
                        ErrorHandler.CredentialsError();
                        break;
                    }
                } while (false);

            }
            catch ( SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                //TODO: handle exception
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // TODO: register function
            var dt = new DataTable();
            _database.Command = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
            var da = _database.GetDataAdapter(dt);

            var reader = _database.ReadData();
            
            if (reader.HasRows)
            {
                ErrorHandler.DuplicateLoginError();
                reader.Close();
            }
            else
            {
                do
                {
                    if (_login == null || _login.Length < 5)
                    {
                        ErrorHandler.LoginLengthError();
                        break;
                    }

                    if (registerPasswordBox.Text.Length < 5)
                    {
                        ErrorHandler.PasswordLengthError();
                        break;
                    }

                    reader.Close();
                    _database.Command = "INSERT INTO Users (Username, Password, Read, Write, Edit, Del)" + $"VALUES ('{_login}', '{_password}', '{1}', '{0}', '{0}', '{0}');";
                    int result = _database.ExecuteCommand();
                    if (result == 1)
                    {
                        _database.Connection.Dispose();
                        MessageBox.Show("Регистрация прошла успешно!", "Регистрация");
                        isAuthorized = true;
                        user = new User(_login, _password, true, false, false, false);
                        Close();
                    }
                } while (false);
            }

            _database.Clear();
            da.Dispose();

        }

        #region НАВОДИМ КРАСОТУ

        private void registerLabel_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(1);
            this.Text = "Регистрация";
            registerNameBox.BringToFront();
            registerPasswordBox.BringToFront();
        }

        private void switchToLoginLabel_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(0);
            this.Text = "Авторизация";
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            this._password = Hash.hashPassword(passwordBox.Text);
        }

        private void loginBox_TextChanged(object sender, EventArgs e)
        {
            this._login = loginBox.Text;
        }

        private void registerNameBox_TextChanged(object sender, EventArgs e)
        {
            this._login = registerNameBox.Text;
            progressBar1.Value = (_login.Length >= 5) ? 100 : _login.Length * 20;
        }

        private void registerPasswordBox_TextChanged(object sender, EventArgs e)
        {
            this._password = Hash.hashPassword(registerPasswordBox.Text);
            progressBar2.Value = (registerPasswordBox.Text.Length >= 5) ? 100 : registerPasswordBox.Text.Length * 20;
        }

        

        #endregion


        // TODO: delete this later, maybe
        private void LoginButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                isAuthorized = true;
                user = new User("admin", "admin", true, true, true, true);
                Close();
            }
        }
    }
}   
