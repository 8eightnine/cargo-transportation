using cargo_transportation.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace cargo_transportation
{
    public partial class MainForm : Form
    {
        public User currentUser;

        public MainForm(User user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramMenu menu = new ProgramMenu();
            ToolStripItemCollection temp = menu.Populate(currentUser);
            int size = temp.Count;
            for (int i = size - 1; i >= 0; i--)
            {
                menuStrip.Items.Add(temp[i]);
            }

            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", "SELECT * FROM 'Order'", dt);
        }
    }
}
