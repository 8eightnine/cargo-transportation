using System;
using System.Windows.Forms;

namespace Trips
{
    public partial class ExportForm : Form
    {
        public int variant = -1;
        public ExportForm(int id)
        {
            InitializeComponent();
            variant = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // word
            variant = 1;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // excel
            variant = 2;
            Close();
        }
    }
}
