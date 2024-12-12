using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DriverCategory
{
    public class DriverCategory
    {
        // Controls
        private static DataGridView dataGridView;
        private static Button addNewButton;
        private static ContextMenuStrip contextMenuStrip;
        private static ToolStripMenuItem AddMenuItem;
        private static ToolStripMenuItem DeleteMenuItem;
        private static TextBox textBox;

        // Working varaibles
        private static DataTable dataTable = new DataTable();
        internal static object databaseObject;

        public static void ShowDriverCategory(Form mainForm)
        {
            #region Designer
            var components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            var dataGridView1 = new DataGridView();
            var menuStrip1 = new MenuStrip();
            var contextMenuStrip1 = new ContextMenuStrip(components);
            var AddToolStripMenuItem = new ToolStripMenuItem();
            var DeleteToolStripMenuItem = new ToolStripMenuItem();
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
            dataGridView = dataGridView1;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            AddToolStripMenuItem,
            DeleteToolStripMenuItem});
            contextMenuStrip1.Name = "contextMenuStrip";
            contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            contextMenuStrip = contextMenuStrip1;
            // 
            // AddToolStripMenuItem
            // 
            AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            AddToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            AddToolStripMenuItem.Text = "Добавить";
            AddToolStripMenuItem.Click += new EventHandler(AddNewEntry);
            // 
            // DeleteToolStripMenuItem
            // 
            DeleteToolStripMenuItem.Name = "изToolStripMenuItem";
            DeleteToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            DeleteToolStripMenuItem.Text = "Удалить";
            DeleteToolStripMenuItem.Click += new EventHandler(DeleteEntry);
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
            Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "cargo-transportation");
            Type programMenuType = asm.GetType("cargo_transportation.Classes.ProgramMenu");
            object programMenuInstance = Activator.CreateInstance(programMenuType);
            MethodInfo populateMethod = programMenuType.GetMethod("Populate");
            ToolStripItemCollection result = (ToolStripItemCollection)populateMethod?.Invoke(programMenuInstance, null);
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
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Заказы";
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            mainForm.ResumeLayout();
            mainForm.PerformLayout();
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Справочники | Категории водителей";
            #endregion
            
            Type databaseType = asm.GetType("cargo_transportation.Classes.Database");
            object databaseInstance = Activator.CreateInstance(databaseType);
            databaseObject = databaseInstance;
            populateTable(dataGridView, databaseObject);
        }
        private static void TextBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                sb.AppendFormat("CONVERT({0}, System.String) LIKE '%{1}%' OR ", column.Name, textBox.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            (dataGridView.DataSource as DataTable).DefaultView.RowFilter = sb.ToString();
            dataGridView.Refresh();
        }
        private static void populateTable(DataGridView dataGridView, object databaseObject)
        {
            if (dataTable.Rows.Count > 0)
                dataTable.Clear();

            MethodInfo populateMethod = databaseObject.GetType().GetMethod("ReadData");
            var result = populateMethod?.Invoke(databaseObject, new object[] { "Databases\\make.db", "SELECT * FROM 'Category_List'", dataTable });
            dataGridView.DataSource = dataTable;
        }
        private static void AddNewEntry(object sender, EventArgs e)
        {
            AddNewForm fo = new AddNewForm();
            fo.ShowDialog();
            string temp = fo.valueToChange;
            if (temp != null)
            {
                MethodInfo addDataMethod = databaseObject.GetType().GetMethod("WriteData");
                var command = $"INSERT INTO Category_List (ID, Value) VALUES (@Value1, @Value2)";
                var parameters = new Dictionary<string, object>
                {
                    { "@Value1", dataGridView.Rows.Count },
                    { "@Value2", temp }
                };
                var result = addDataMethod?.Invoke(databaseObject, new object[] { "Databases\\make.db", command, parameters });
                populateTable(dataGridView, databaseObject);
            }
        }
        private static void DeleteEntry(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
                var rowIndex = dataGridView.SelectedCells[0].RowIndex;
                var rowData = dataGridView.Rows[rowIndex].Cells[1].Value;
                MessageBox.Show(rowIndex + " --- " + rowData);
                MethodInfo addDataMethod = databaseObject.GetType().GetMethod("WriteData");
                var command = $"DELETE FROM Category_List WHERE ID = @Value1 AND Value = @Value2";
                var parameters = new Dictionary<string, object>
                {
                    { "@Value1", rowIndex + 1 },
                    { "@Value2", rowData }
                };
                var result = addDataMethod?.Invoke(databaseObject, new object[] { "Databases\\make.db", command, parameters });
                populateTable(dataGridView, databaseObject);
            }
            else
            {
                MessageBox.Show("Выберите одну запись для удаления.");
            }
        }
    }
}
