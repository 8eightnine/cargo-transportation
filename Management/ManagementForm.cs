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

namespace Management
{
    public partial class ManagementForm : Form
    {
        DataTable users = new DataTable();
        public ManagementForm()
        {
            InitializeComponent();
            ReadUsers();
        }

        private void ReadUsers()
        {
            string command = $"SELECT * FROM 'Users'";
            Database.ReadData("Databases\\users.db", command, users);

            foreach (DataRow dr in users.Rows)
            {
                comboBox1.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRights();
        }

        private void FillRights()
        {
            DataTable dt = new DataTable();
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                int temp = selectedItem.Id;
                string command = $"SELECT Rights.ModuleID, Menu.Name AS ModuleName, Rights.Read, Rights.Write, Rights.Edit, Rights.Del FROM Rights JOIN Menu ON Rights.ModuleID = Menu.ID WHERE Rights.UserID = {temp}";
                Database.ReadData("Databases\\users.db", command, dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Модуль";
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].HeaderText = "Чтение";
                dataGridView1.Columns[3].HeaderText = "Запись";
                dataGridView1.Columns[4].HeaderText = "Редактирование";
                dataGridView1.Columns[5].HeaderText = "Удаление";
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= 2)
            {
                // Получаем новое значение
                string newValue = e.FormattedValue.ToString();

                // Проверяем, является ли значение 0 или 1
                if (newValue != "0" && newValue != "1")
                {
                    MessageBox.Show("Разрешено вводить только 0 или 1.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true; // Отменяем ввод
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem) {
                int userid = selectedItem.Id;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int moduleId, read, write, edit, delete;
                    moduleId = Int32.Parse(row.Cells[0].Value.ToString());
                    read = Int32.Parse(row.Cells[2].Value.ToString());
                    write = Int32.Parse(row.Cells[3].Value.ToString());
                    edit = Int32.Parse(row.Cells[4].Value.ToString());
                    delete = Int32.Parse(row.Cells[5].Value.ToString());
                    string command = $"UPDATE 'Rights' SET Read = {read}, Write = {write}, Edit = {edit}, Del = {delete} WHERE UserID = {userid} AND ModuleID = {moduleId}";
                    Database.WriteData("Databases\\users.db", command);
                }
                MessageBox.Show("Сохранено");
            }
        }
    }
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}

