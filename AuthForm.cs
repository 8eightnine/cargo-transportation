using cargo_transportation.Classes;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace cargo_transportation
{
    public partial class AuthForm : Form
    {
        private string _login;
        private string _password;
        public bool isAuthorized = false;
        
        public User user;

        public AuthForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                do
                {
                    var dt = new DataTable();
                    using (SQLiteConnection connection = new SQLiteConnection("Data Source='Databases\\users.db';Version=3; FailIfMissing=False"))
                    {
                        connection.Open();
                        using (SQLiteCommand cmd = connection.CreateCommand())
                        { 
                            cmd.CommandText = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }

                    string username = "", password = "";
                    int id;
                    
                    if (dt.Rows.Count > 0)
                    {
                        id = Int32.Parse(dt.Rows[0][0].ToString());
                        username = dt.Rows[0][1].ToString();
                        password = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        ErrorHandler.CredentialsError();
                        break;
                    }

                    dt.Dispose();


                    if (username.Equals(_login) && password.Equals(_password))
                    {
                        isAuthorized = true;
                        user = new User(_login, _password);
                        DataTable rights = Database.GetRights(id);
                        DataTable modules = Database.GetModules();
                        user.AddRights(rights, modules);
                        Close();
                    }
                    else
                    {
                        ErrorHandler.CredentialsError();
                        break;
                    }
                } while (false);

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection("Data Source='Databases\\users.db';Version=3; FailIfMissing=False"))
            {
                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Users WHERE " + "Username" + "='" + _login + "'";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0)
            {
                ErrorHandler.DuplicateLoginError();
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

                    user = new User(_login, _password);
                    if (ProgramMenu.RegisterNewUser(user, _login, _password) == 1)
                    {
                        MessageBox.Show("Регистрация прошла успешно!", "Регистрация");
                        isAuthorized = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при регистрации, попробуйте еще раз", "Регистрация");
                    }

                } while (false);
            }

        }

        #region Интерфейс

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


        // Вход без ввода данных - для тестирования
        private void LoginButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                user = new User(_login, _password);
                loginBox.Text = passwordBox.Text = "admin";
                LoginButton.PerformClick();
            }
        }
    }
}   
