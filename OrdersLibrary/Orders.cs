using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Orders
{
    public class Orders
    {
        // Controls
        private static DataGridView dataGridView;
        private static Button addNewButton;
        private static ContextMenuStrip contextMenuStrip;
        private static ToolStripMenuItem открытьToolStripMenuItem;
        private static ToolStripMenuItem изToolStripMenuItem;
        private static TextBox textBox;


        // Working varaibles
        private static DataTable dataTable = new DataTable();

        public static void ShowOrders(Form mainForm)
        {
            var components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            var dataGridView1 = new DataGridView();
            var menuStrip1 = new MenuStrip();
            var contextMenuStrip1 = new ContextMenuStrip(components);
            var открытьToolStripMenuItem = new ToolStripMenuItem();
            var изToolStripMenuItem = new ToolStripMenuItem();
            var addNewEntryButton = new Button();
            var textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Location = new System.Drawing.Point(12, 28);
            dataGridView1.Name = "dataGridView";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new System.Drawing.Size(780, 392);
            dataGridView1.TabIndex = 2;
            dataGridView = dataGridView1;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            открытьToolStripMenuItem,
            изToolStripMenuItem});
            contextMenuStrip1.Name = "contextMenuStrip";
            contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            contextMenuStrip = contextMenuStrip1;
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            // 
            // изToolStripMenuItem
            // 
            изToolStripMenuItem.Name = "изToolStripMenuItem";
            изToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            изToolStripMenuItem.Text = "Удалить";
            // 
            // addNewEntryButton
            // 
            addNewEntryButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            addNewEntryButton.Location = new System.Drawing.Point(8, 426);
            addNewEntryButton.Name = "addNewButton";
            addNewEntryButton.Size = new System.Drawing.Size(90, 23);
            addNewEntryButton.TabIndex = 3;
            addNewEntryButton.Text = "Добавить";
            addNewEntryButton.UseVisualStyleBackColor = true;
            addNewEntryButton.Click += addNewEntryButton_Click;
            addNewButton = addNewEntryButton;
            // 
            // textBox1
            // 
            textBox1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            textBox1.Location = new System.Drawing.Point(103, 426);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox";
            textBox1.Size = new System.Drawing.Size(268, 20);
            textBox1.TabIndex = 6;
            textBox = textBox1;
            // 
            // MainForm
            //
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
            populateTable(dataGridView);
        }

        private static void populateTable(DataGridView dataGridView)
        {
            Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "cargo-transportation");
            Type databaseType = asm.GetType("cargo_transportation.Classes.Database");
            object databaseInstance = Activator.CreateInstance(databaseType);
            MethodInfo populateMethod = databaseType.GetMethod("ReadData");
            var result = populateMethod?.Invoke(databaseInstance, new object[] { "Databases\\make.db", "SELECT * FROM 'Order'", dataTable });
            dataGridView.DataSource = dataTable;
        }

        private static void addNewEntryButton_Click(object sender, EventArgs e)
        {
            dataGridView.Refresh();
        }
    }
}
