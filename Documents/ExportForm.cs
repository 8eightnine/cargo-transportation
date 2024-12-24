using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documents
{
    public partial class ExportForm : Form
    {
        public ExportParams exportParams = new ExportParams();

        public ExportForm()
        {
            InitializeComponent();
        }
        
        private int CheckValidity()
        {
            if (!wordExportCheckBox.Checked && !ExcelExportCheckBox.Checked)
            {
                throw new Exception("Не выбран тип файла для экспорта");
            }
            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    exportParams.reportType = listBox1.SelectedIndex;
                    exportParams.exportToExcel = ExcelExportCheckBox.Checked;
                    exportParams.exportToWord = wordExportCheckBox.Checked;
                    exportParams.filterValue = dateTimePicker1.Value;
                    Documents.ExportFile(exportParams);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                label2.Text = "Вывести заказы с датой доставки до:";
            }
            else if (listBox1.SelectedIndex == 4)
            {
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                label2.Text = "Вывести машины с датой ремонта до:";
            }
            else
            {
                label2.Visible = false;
                dateTimePicker1.Visible = false;
            }
        }
    }
}
