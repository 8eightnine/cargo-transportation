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
            int rowsCount = dt.Rows.Count;
            var parentValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString().Equals("0"));
            var childValues = dt.AsEnumerable().Where(x => x["ParentID"].ToString() != "0");
            ToolStripMenuItem[] toolStripItems = new ToolStripMenuItem[rowsCount + 1];

            foreach (DataRow dr in dt.Rows)
            {
                if (Int64.Parse(dr["ParentID"].ToString()) == 0)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    // TODO: Fix this shit! Or make a parser
                    tsmi.Name = dr["LibraryName"].ToString() + "-" + dr["Function"].ToString();
                    tsmi.Text = dr["Name"].ToString();
                    tsmi.Tag = dr["ID"].ToString();
                    tsmi.ToolTipText = dr["Order"].ToString();
                    if (!tsmi.Tag.Equals("NULL"))
                        tsmi.Click += new EventHandler(MenuItemClickHandler);

                    toolStripItems[Int64.Parse(dr["ID"].ToString())] = tsmi;
                }
                
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (Int64.Parse(dr["ParentID"].ToString()) != 0)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();

                    tsmi.Name = dr["LibraryName"].ToString();
                    tsmi.Text = dr["Name"].ToString();
                    tsmi.Tag = dr["Function"].ToString();
                    tsmi.ToolTipText = dr["Order"].ToString();
                    tsmi.Click += new EventHandler(MenuItemClickHandler);
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

            ToolStrip strip = new ToolStrip();
            for (int i = 0; i < toolStripItems.Count(); i++)
            {
                strip.Items.Add(result[i]);

            }

            return strip.Items;
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            var values = clickedItem.Name.ToString().Split('-');
            

            switch (values[0])
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
