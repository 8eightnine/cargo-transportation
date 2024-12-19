using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cargo_transportation;
using cargo_transportation.Classes;

namespace Cargo
{
    public class Cargo
    {
        // Controls
        private static DataGridView dataGridViewCargo;
        private static DataGridView dataGridViewList;
        private static Button addNewButton;
        private static TextBox textBoxCargo;
        private static TextBox textBoxList;

        // Working varaibles
        private static DataTable dataTable1 = new DataTable();
        private static DataTable dataTable2 = new DataTable();
        internal static object databaseObject;
        internal static User currentUser;
        internal static MainForm _mainForm;
        internal static string moduleName;
        //internal PhysClient client;

        public static void ShowCargo(MainForm mainForm)
        {
            #region Designer
            currentUser = mainForm.currentUser;
            moduleName = mainForm.Tag.ToString();
            var components1 = new System.ComponentModel.Container();
            var components2 = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            var dataGridView1 = new DataGridView();
            var dataGridView2 = new DataGridView();
            var menuStrip1 = new MenuStrip();
            var contextMenuStrip1 = new ContextMenuStrip(components1);
            var contextMenuStrip2 = new ContextMenuStrip(components2);
            var AddToolStripMenuItem = new ToolStripMenuItem();
            var DeleteToolStripMenuItem = new ToolStripMenuItem();
            var EditToolStripMenuItem = new ToolStripMenuItem();
            var addNewEntryButton = new Button();
            var textBox1 = new TextBox();
            var textBox2 = new TextBox();
            var label1 = new Label();
            var label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView2)).BeginInit();
            contextMenuStrip1.SuspendLayout();
            mainForm.SuspendLayout();
            mainForm.Controls.Clear();
            // 
            // dataGridView1
            //
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Location = new System.Drawing.Point(12, 56);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(385, 368);
            dataGridView1.TabIndex = 5;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewList = dataGridView1;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.ContextMenuStrip = contextMenuStrip2;
            dataGridView2.Location = new System.Drawing.Point(405, 56);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new System.Drawing.Size(385, 368);
            dataGridView2.TabIndex = 6;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCargo = dataGridView2;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            AddToolStripMenuItem,
            DeleteToolStripMenuItem,
            EditToolStripMenuItem});
            contextMenuStrip1.Name = "contextMenuStrip";
            contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            AddToolStripMenuItem,
            DeleteToolStripMenuItem,
            EditToolStripMenuItem});
            contextMenuStrip1.Name = "contextMenuStrip";
            contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            // 
            // AddToolStripMenuItem
            // 
            AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            AddToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            AddToolStripMenuItem.Text = "Добавить";
            AddToolStripMenuItem.Click += new EventHandler(AddNewEntryList);
            if (currentUser.rights.Where(r => r.name == moduleName).FirstOrDefault().write.Equals(0))
            {
                AddToolStripMenuItem.Enabled = false;
            }
            // 
            // DeleteToolStripMenuItem
            // 
            DeleteToolStripMenuItem.Name = "изToolStripMenuItem";
            DeleteToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            DeleteToolStripMenuItem.Text = "Удалить";
            DeleteToolStripMenuItem.Click += new EventHandler(DeleteEntryList);
            if (currentUser.rights.Where(r => r.name == moduleName).FirstOrDefault().delete.Equals(0))
            {
                DeleteToolStripMenuItem.Enabled = false;
            }
            // 
            // EditToolStripMenuItem
            // 
            EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            EditToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            EditToolStripMenuItem.Text = "Изменить";
            EditToolStripMenuItem.Click += new EventHandler(EditEntry);
            if (currentUser.rights.Where(r => r.name == moduleName).FirstOrDefault().edit.Equals(0))
            {
                EditToolStripMenuItem.Enabled = false;
            }
            // 
            // textBox
            // 
            textBox1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            textBox1.Location = new System.Drawing.Point(22, 426);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox";
            textBox1.Size = new System.Drawing.Size(368, 20);
            textBox1.TabIndex = 6;
            textBox1.TextChanged += TextBox1_TextChanged;
            textBoxList = textBox1;
            // 
            // textBox
            // 
            textBox2.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            textBox2.Location = new System.Drawing.Point(415, 426);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox";
            textBox2.Size = new System.Drawing.Size(368, 20);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += TextBox2_TextChanged;
            textBoxCargo = textBox2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(563, 40);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 13);
            label1.TabIndex = 7;
            label1.Text = "Грузы на складе";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(160, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(92, 13);
            label2.TabIndex = 8;
            label2.Text = "Перечни грузов";
            #endregion

            #region adding controls to the form
            ProgramMenu menu = new ProgramMenu();
            ToolStripItemCollection result = menu.Populate(currentUser);
            int size = result.Count;
            for (int i = size - 1; i >= 0; i--)
            {
                menuStrip1.Items.Add(result[i]);
            }
            mainForm.Controls.Add(menuStrip1);
            mainForm.Controls.Add(label1);
            mainForm.Controls.Add(label2);
            mainForm.Controls.Add(textBox1);
            mainForm.Controls.Add(textBox2);
            mainForm.Controls.Add(addNewButton);
            mainForm.Controls.Add(dataGridView1);
            mainForm.Controls.Add(dataGridView2);
            mainForm.MinimumSize = new System.Drawing.Size(818, 494);
            mainForm.Name = "MainForm";
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            mainForm.ResumeLayout();
            mainForm.PerformLayout();
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Заказы";
            #endregion

            populateTableLists(dataGridViewList);
            populateTableCargo(dataGridViewCargo);
        }
        private static void TextBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridViewList.Columns)
            {
                sb.AppendFormat("CONVERT([{0}], System.String) LIKE '%{1}%' OR ", column.Name, textBoxList.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            (dataGridViewList.DataSource as DataTable).DefaultView.RowFilter = sb.ToString();
            dataGridViewList.Refresh();
        }
        private static void TextBox2_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridViewCargo.Columns)
            {
                sb.AppendFormat("CONVERT([{0}], System.String) LIKE '%{1}%' OR ", column.Name, textBoxCargo.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            (dataGridViewCargo.DataSource as DataTable).DefaultView.RowFilter = sb.ToString();
            dataGridViewCargo.Refresh();
        }
        private static void populateTableLists(DataGridView dataGridView)
        {
            if (dataTable1.Rows.Count > 0)
                dataTable1.Clear();
            dataTable1 = Database.GetCargoLists();
            dataGridView.DataSource = dataTable1;
            dataGridView.Refresh();
        }
        private static void populateTableCargo(DataGridView dataGridView)
        {
            if (dataTable2.Rows.Count > 0)
                dataTable2.Clear();
            dataTable2 = Database.GetCargo();
            dataGridView.DataSource = dataTable2;
        }
        private static void AddNewEntryList(object sender, EventArgs e)
        {
            //Order order = new Order();
            //order._isNew = 1;
            //OrderForm fo = new OrderForm(order);
            //fo.ShowDialog();
        }
        private static void AddNewEntryCargo(object sender, EventArgs e)
        {
            //Order order = new Order();
            //order._isNew = 1;
            //OrderForm fo = new OrderForm(order);
            //fo.ShowDialog();
        }
        private static void EditEntry(object sender, EventArgs e)
        {
            //if (dataGridView.SelectedRows.Count == 1)
            //{
            //    var rowIndex = dataGridView.SelectedCells[0].RowIndex;
            //    DataRow dr = ((DataRowView)dataGridView.Rows[rowIndex].DataBoundItem).Row;
            //    Order order;
            //    order = Order.ParseToOrder(dr);
            //    OrderForm fo = new OrderForm(order);
            //    fo.ShowDialog();
            //}
        }
        private static void DeleteEntryList(object sender, EventArgs e)
        {
            if (dataGridViewCargo.SelectedRows.Count == 1)
            {
                var rowIndex = dataGridViewList.SelectedCells[0].RowIndex;
                string rowData = "";
                foreach (DataGridViewCell dataGridCell in dataGridViewList.Rows[rowIndex].Cells)
                {
                    rowData += $"{dataGridCell.Value.ToString()} - ";
                }
                DialogResult dialogResult = MessageBox.Show($"Вы хотите удалить следующую строку:\n {rowData}", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Database.DeleteCargoList(rowIndex + 1, dataGridViewList.Rows[rowIndex].Cells[2].Value.ToString());
                    populateTableLists(dataGridViewList);
                }
            }
            else
            {
                MessageBox.Show("Выберите одну запись для удаления.");
            }
        }

        private static void DeleteEntryCargo(object sender, EventArgs e)
        {
            if (dataGridViewCargo.SelectedRows.Count == 1)
            {
                var rowIndex = dataGridViewCargo.SelectedCells[0].RowIndex;
                string rowData = "";
                foreach (DataGridViewCell dataGridCell in dataGridViewCargo.Rows[rowIndex].Cells)
                {
                    rowData += $"{dataGridCell.Value.ToString()} - ";
                }
                DialogResult dialogResult = MessageBox.Show($"Вы хотите удалить следующую строку:\n {rowData}", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Database.DeleteCargo(rowIndex + 1, dataGridViewCargo.Rows[rowIndex].Cells[1].Value.ToString());
                    populateTableCargo(dataGridViewCargo);
                }
            }
            else
            {
                MessageBox.Show("Выберите одну запись для удаления.");
            }
        }
    }
}
