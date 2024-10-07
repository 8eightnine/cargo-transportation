using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    internal class ProgramMenu
    {
        public ToolStripItemCollection Populate()
        {
            Database temp = new Database("Databases\\menu.db");
            DataTable dt = new DataTable();
            temp.Connect();

            temp.Command = "SELECT * FROM Menu";
            temp.GetDataAdapter(dt);
            var parentValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString().Equals("0")).ToList();
            var childValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString() != "0").ToList();
            ToolStripMenuItem[] toolStripItems = new ToolStripMenuItem[parentValues.Count];

            foreach (DataRow dr in parentValues)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();

                tsmi.Name = dr["LibraryName"].ToString();
                tsmi.Text = dr["Name"].ToString();
                tsmi.Tag = dr["Function"].ToString();
                if (!tsmi.Tag.Equals("NULL"))
                    tsmi.Click += new EventHandler(MenuItemClickHandler);

                toolStripItems[Int64.Parse(dr["Order"].ToString())] = tsmi;
            }

            foreach (DataRow dr in childValues)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();

                tsmi.Name = dr["LibraryName"].ToString();
                tsmi.Text = dr["Name"].ToString();
                tsmi.Tag = dr["Function"].ToString();
                tsmi.Click += new EventHandler(MenuItemClickHandler);
                string parentName = parentValues[Int32.Parse(dr["ParentID"].ToString())].Field<string>(2);
                MessageBox.Show(parentName);
                
                toolStripItems[Int64.Parse(parentValues[parentName].ToString())].DropDownItems.Add(tsmi);
                // TODO: swap existing options if the new one is higher in the priority list
            }

            ToolStrip strip = new ToolStrip();
            for (int i = 0; i < toolStripItems.Count(); i++)
            {
                strip.Items.Add(toolStripItems[i]);

            }

            return strip.Items;
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            switch (clickedItem.Name)
            {
                case "About":
                    {
                        //TODO: make this shit work
                        MessageBox.Show("Test: " + clickedItem.Tag);
                        break;
                    }
                case "Orders":
                    {
                        MessageBox.Show("Test: " + clickedItem.Tag);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }


    }
}
