using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace cargo_transportation
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private Database _database;
        private DataTable _dataTable;
        private string _currentModule;
        private ControlCollection _defaultControls;

        public MainForm(User user)
        {
            _database = new Database("Databases\\db.db");
            _database.Connect();
            currentUser = user;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProgramMenu menu = new ProgramMenu();
            usernameLabel.Text = "Добро пожаловать, " + currentUser.Login + "!";
            ChangeStatusStrip(_database.Status);
            ToolStripItemCollection temp = menu.Populate();
            int size = temp.Count;
            for (int i = size - 1; i >= 0; i--)
            {
                menuStrip1.Items.Add(temp[i]);
            }

            menuStrip1.Items[0].PerformClick();
            _currentModule = menuStrip1.Items[0].Name;
        }

        private void ChangeStatusStrip(string status)
        {
            databaseStatusLabel.Text = "Статус базы данных: " + status;
        }

        public static void InvokeStringMethod(string typeName, string methodName, Form form)
        {
            // Get the Type for the class
            Type calledType = Type.GetType(typeName);

            // Invoke the method itself. The string returned by the method winds up in s.
            // Note that stringParam is passed via the last parameter of InvokeMember,
            // as an array of Objects.
            calledType.InvokeMember(
                            methodName,
                            BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Static,
                            null,
                            null,
                            new Object[] { form });

            
        }
    }
}
