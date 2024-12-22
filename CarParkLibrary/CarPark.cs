using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cargo_transportation;
using cargo_transportation.Classes;

namespace CarPark
{
    public class CarPark
    {
        // Controls
        private static DataGridView dataGridView;
        private static Button addNewButton;
        private static TextBox textBox;

        // Working varaibles
        private static DataTable dataTable = new DataTable();
        internal static object databaseObject;
        internal static User currentUser;
        internal static MainForm _mainForm;
        internal static string moduleName;
        //internal PhysClient client;

        public static void ShowCars(MainForm mainForm)
        {
            #region Designer
            currentUser = mainForm.currentUser;
            moduleName = mainForm.Tag.ToString();
            var components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            var dataGridView1 = new DataGridView();
            var menuStrip1 = new MenuStrip();
            var contextMenuStrip1 = new ContextMenuStrip(components);
            var AddToolStripMenuItem = new ToolStripMenuItem();
            var DeleteToolStripMenuItem = new ToolStripMenuItem();
            var EditToolStripMenuItem = new ToolStripMenuItem();
            var addNewEntryButton = new Button();
            var textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            contextMenuStrip1.SuspendLayout();
            mainForm.SuspendLayout();
            mainForm.Controls.Clear();
            // 
            // dataGridView
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Location = new System.Drawing.Point(12, 28);
            dataGridView1.Name = "dataGridView";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new System.Drawing.Size(780, 392);
            dataGridView1.TabIndex = 2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView = dataGridView1;
            // 
            // contextMenuStrip
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
            AddToolStripMenuItem.Click += new EventHandler(AddNewEntry);
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
            DeleteToolStripMenuItem.Click += new EventHandler(DeleteEntry);
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
            textBox1.Location = new System.Drawing.Point(12, 426);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox";
            textBox1.Size = new System.Drawing.Size(268, 20);
            textBox1.TabIndex = 6;
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox = textBox1;


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
            mainForm.Controls.Add(textBox1);
            mainForm.Controls.Add(addNewButton);
            mainForm.Controls.Add(dataGridView1);
            mainForm.MinimumSize = new System.Drawing.Size(818, 494);
            mainForm.Name = "MainForm";
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            mainForm.ResumeLayout();
            mainForm.PerformLayout();
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Парк машин";
            #endregion

            populateTable(dataGridView);
        }
        private static void TextBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                sb.AppendFormat("CONVERT([{0}], System.String) LIKE '%{1}%' OR ", column.Name, textBox.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            (dataGridView.DataSource as DataTable).DefaultView.RowFilter = sb.ToString();
            dataGridView.Refresh();
        }
        private static void populateTable(DataGridView dataGridView)
        {
            if (dataTable.Rows.Count > 0)
                dataTable.Clear();
            dataTable = Database.GetCars();
            dataGridView.DataSource = dataTable;
        }
        private static void AddNewEntry(object sender, EventArgs e)
        {
            Car car = new Car();
            car._isNew = 1;
            CarForm fo = new CarForm(car);
            fo.ShowDialog();
            populateTable(dataGridView);
        }
        private static void EditEntry(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var rowIndex = dataGridView.SelectedCells[0].RowIndex;
                DataRow dr = ((DataRowView)dataGridView.Rows[rowIndex].DataBoundItem).Row;
                Car car;
                DataTable dt = new DataTable();
                Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Car_List' WHERE ID = '{dr.ItemArray[0]}'", dt);
                DataRow dr2 = dt.Rows[0];
                car = Car.ParseToCar(dr2);
                CarForm fo = new CarForm(car);
                fo.ShowDialog();
            }
            populateTable(dataGridView);
        }
        private static void DeleteEntry(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var rowIndex = dataGridView.SelectedCells[0].RowIndex;
                string rowData = dataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                foreach (DataGridViewCell dataGridCell in dataGridView.Rows[rowIndex].Cells)
                {
                    rowData += $" - {dataGridCell.Value.ToString()}";
                }
                DialogResult dialogResult = MessageBox.Show($"Вы хотите удалить следующую строку:\n {rowData}", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Database.DeleteCar(Int32.Parse(dataGridView.Rows[rowIndex].Cells[0].Value.ToString()), dataGridView.Rows[rowIndex].Cells[1].Value.ToString());
                    populateTable(dataGridView);
                }
            }
            else
            {
                MessageBox.Show("Выберите одну запись для удаления.");
            }
        }
    }
}
