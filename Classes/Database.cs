using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    internal class Database
    {
        public static DataTable ReadData(string path, string command, DataTable dt)
        {
            string fullPath = $"Data Source='{path}';Version=3; FailIfMissing=False";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(fullPath))
                {
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = command;
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        if (adapter != null)
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
    }
}
