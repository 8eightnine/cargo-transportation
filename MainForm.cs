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
        public MainForm(User user)
        {
            currentUser = user;
            InitializeComponent();
            if (user.Read == true)
            {
                MessageBox.Show("Congrats! You can read!");
            }
            
        }
    }
}
