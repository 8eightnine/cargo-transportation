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
        public User user;
        private SQLiteConnection con;

        public AuthForm()
        {
            InitializeComponent();
            con = DatabaseWorker.Connect(filename);
            if (con == null)
            {
                throw new Exception("Не удалось установить соединение с базой данных пользователей");
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                do
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    DataTable dt = new DataTable();
                    cmd.CommandText = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
                    cmd.Connection = con;
                    SQLiteDataAdapter da = DatabaseWorker.GetDataAdapter(dt, cmd);

                    var reader = cmd.ExecuteReader();
                    string username = "", password = "";
                    bool readRight = false, writeRight = false, editRight = false, deleteRight = false;
                    if (reader.HasRows)
                    {
                        reader.Read();
                        username = reader.GetString(1);
                        password = reader.GetString(2);
                        readRight = reader.GetBoolean(3);
                        writeRight = reader.GetBoolean(4);
                        editRight = reader.GetBoolean(5);
                        deleteRight = reader.GetBoolean(6);
                        reader.Close();
                    }
                    else
                    {
                        ErrorHandler.CredentialsError();
                        break;
                        // TODO: handle if user is not in the database
                    }

                    cmd.Dispose();
                    dt.Dispose();
                    da.Dispose();
                    reader.Dispose();


                    if (username.Equals(_login) && password.Equals(_password))
                    {
                        //TODO: activate main window
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
            SQLiteCommand cmd = new SQLiteCommand();
            DataTable dt = new DataTable();
            cmd.CommandText = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
            cmd.Connection = con;
            SQLiteDataAdapter da = DatabaseWorker.GetDataAdapter(dt, cmd);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ErrorHandler.DuplicateLoginError();
                reader.Close();
            }
            else
            {
                do
                {
                    if (_login.Length < 5)
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
                    cmd.CommandText = "INSERT INTO Users (Username, Password, Read, Write, Edit, Del)" + $"VALUES ('{_login}', '{_password}', '{1}', '{0}', '{0}', '{0}');";
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Регистрация прошла успешно!", "Регистрация");
                        isAuthorized = true;
                        user = new User(_login, _password, true, false, false, false);
                        Close();
                    }
                } while (false);
            }

            cmd.Dispose();
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
    }
}   
