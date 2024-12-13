using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverClass
{
    public partial class AddNewForm : Form
    {
        public string valueToChange { get; set; }
        public AddNewForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = textBox2.Text;
            if (temp.Length == 0 || temp.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show("Ошибка: проверьте правильность ввода значения.");
                textBox2.Text = "";
                valueToChange = "!ERROR";
            }
            else
            {
                valueToChange = temp;
                Close();
            }

        }
    }
}
