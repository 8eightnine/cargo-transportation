using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public class ProgramMenu
    {
        public ToolStripItemCollection Populate(User user)
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\users.db", "SELECT * FROM Menu", dt);

            int rowsCount = dt.Rows.Count;
            var parentValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString().Equals("0"));
            var childValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString() != "0");
            ToolStripMenuItem[] toolStripItems = new ToolStripMenuItem[rowsCount + 1];

            foreach (DataRow dr in dt.Rows)
            {
                if (Int64.Parse(dr["ParentID"].ToString()) == 0)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Name = dr["LibraryName"].ToString() + "-" + dr["Function"].ToString();
                    tsmi.Text = dr["Name"].ToString();
                    tsmi.Tag = dr["ID"].ToString();
                    tsmi.ToolTipText = dr["Order"].ToString();
                    if (!tsmi.Tag.Equals("NULL"))
                        tsmi.Click += new EventHandler(MenuItemClickHandler);
                    // Отключаем те пункты, к которым закрыт доступ
                    if (user.rights.FirstOrDefault(r => r.name == tsmi.Text.ToString()).read == 0)
                        tsmi.Enabled = false;

                    toolStripItems[Int64.Parse(dr["ID"].ToString())] = tsmi;
                }

            }

            foreach (DataRow dr in dt.Rows)
            {
                if (Int64.Parse(dr["ParentID"].ToString()) != 0)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();

                    tsmi.Name = dr["LibraryName"].ToString() + "-" + dr["Function"].ToString();
                    tsmi.Text = dr["Name"].ToString();
                    tsmi.Tag = dr["ID"].ToString();
                    tsmi.ToolTipText = dr["Order"].ToString();
                    tsmi.Click += new EventHandler(MenuItemClickHandler);
                    // Отключаем те пункты, к которым закрыт доступ
                    if (user.rights.FirstOrDefault(r => r.name == tsmi.Text.ToString()).read == 0)
                        tsmi.Enabled = false;

                    var test = toolStripItems[Int64.Parse(dr["ParentID"].ToString())].DropDownItems.Add(tsmi);
                }
            }

            toolStripItems = toolStripItems.Where(item => item != null).ToArray();

            foreach (ToolStripMenuItem tsmi in toolStripItems)
            {
                var orderedItems = tsmi.DropDownItems.Cast<ToolStripMenuItem>()
                .OrderByDescending(item => -Int32.Parse(item.ToolTipText))
                .ToList();
                tsmi.DropDownItems.Clear();
                foreach (var item in orderedItems)
                {
                    tsmi.DropDownItems.Add(item);
                }
            }
            var result = (from i in toolStripItems orderby -Int64.Parse(i.ToolTipText) select i).ToList();

            MenuStrip strip = new MenuStrip();
            for (int i = 0; i < toolStripItems.Count(); i++)
            {
                strip.Items.Add(result[i]);
            }

            dt.Dispose();
            return strip.Items;
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStrip strip;
            string[] values = new string[2];
            if (clickedItem.OwnerItem == null)
            {
                strip = clickedItem.Owner;
                values = clickedItem.Name.ToString().Split('-');
                strip.Parent.Tag = clickedItem.Text;
                LibInvoke.InvokeFunction(values[0], values[1], (Form)strip.Parent);
            }
            else
            {
                var stripParent = clickedItem.OwnerItem;
                values = clickedItem.Name.ToString().Split('-');
                stripParent.Owner.Parent.Tag = clickedItem.Text;
                LibInvoke.InvokeFunction(values[0], values[1], (Form)stripParent.Owner.Parent);
            }
        }

        public static int RegisterNewUser(User user, string username, string password)
        {
            string cmd = $"INSERT INTO Users (Username, Password) VALUES ('{username}', '{password}')";
            Database.WriteData("Databases\\users.db", cmd);
            DataTable users = new DataTable();
            Database.ReadData("Databases\\users.db", $"SELECT * FROM Users WHERE Username = '{username}'", users);
            DataRow userRow = users.Rows[0];
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\users.db", "SELECT * FROM Menu", dt);
            int rowsCount = dt.Rows.Count;
            user.rights = new Rights[rowsCount];
            string command = "INSERT INTO Rights (UserID, ModuleID, Read, Write, Edit, Del) ";
            for (int i = 0; i < rowsCount; i++)
            {
                string temp;
                DataRow moduleRow = dt.Rows[i];
                if (moduleRow.ItemArray[3].ToString() == "Management")
                {
                    temp = command + $"VALUES ('{userRow.ItemArray[0]}', '{moduleRow.ItemArray[0].ToString()}', {0}, {0}, {0}, {0})";
                    user.rights[i].name = moduleRow.ItemArray[3].ToString();
                    user.rights[i].read = 0;
                    user.rights[i].write = user.rights[i].edit = user.rights[i].delete = 0;
                    Database.WriteData("Databases\\users.db", temp);
                    continue;
                }
                else
                    temp = command + $"VALUES ('{userRow.ItemArray[0]}', '{moduleRow.ItemArray[0].ToString()}', {1}, {0}, {0}, {0})";
                try
                {
                    Database.WriteData("Databases\\users.db", temp);
                    user.rights[i].name = moduleRow.ItemArray[3].ToString();
                    user.rights[i].read = 1;
                    user.rights[i].write = user.rights[i].edit = user.rights[i].delete = 0;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}

