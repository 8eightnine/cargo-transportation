using System;
using System.Linq;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace Banks
{
    public partial class AddNewForm : Form
    {
        public AddNewForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    string command = $"INSERT INTO 'Bank' (Name, PaymentAccount) VALUES ('{bankNameBox.Text}','{paymentAccountBox.Text}');";
                    Database.WriteData("Databases\\make.db", command);
                    Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public int CheckValidity()
        {
            if (bankNameBox.Text.Length != 0)
            {
                if (!bankNameBox.Text.Any(char.IsLetterOrDigit))
                {
                    throw new Exception("Поле 'Название' содержит недопустимые символы");
                }
            }
            else throw new Exception("Поле 'Название' не может быть пустым");

            if (paymentAccountBox.Text.Length != 0)
            {
                if (!paymentAccountBox.Text.Any(char.IsDigit))
                {
                    throw new Exception("Поле 'Счет оплаты' содержит недопустимые символы");
                }
            }
            else throw new Exception("Поле 'Счет оплаты' не может быть пустым");

            return 1;
        }
    }
}
