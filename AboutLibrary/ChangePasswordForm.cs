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

namespace About
{
    public partial class ChangePasswordForm : Form
    {
        private static User _user;
        public ChangePasswordForm(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Hash.hashPassword(textBox1.Text) == _user.Password)
            {
                if (textBox2.Text == textBox3.Text)
                {
                    _user.Password = textBox3.Text;
                    try
                    {
                        Database.UpdateUser(_user);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MessageBox.Show("Пароль успешно изменен", "", MessageBoxButtons.OK);
                    Close();
                }
                else MessageBox.Show("Проверьте правильность ввода нового пароля", "", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Текущий пароль введен неверно!", "", MessageBoxButtons.OK);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
