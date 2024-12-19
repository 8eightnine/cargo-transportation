using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public class Database
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

        public static void WriteData(string path, string command, Dictionary<string, object> parameters = null)
        {
            string fullPath = $"Data Source='{path}';Version=3; FailIfMissing=False";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = command;

                        // Add parameters to prevent SQL injection
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        cmd.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static int ReadSingleInt(string path, string command)
        {
            string fullPath = $"Data Source='{path}';Version=3; FailIfMissing=False";
            int result = 0;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(fullPath))
                {
                    connection.Open(); // Open the connection
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = command;

                        object scalarResult = cmd.ExecuteScalar();
                        if (scalarResult != null && int.TryParse(scalarResult.ToString(), out int value))
                        {
                            result = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        public static string ReadSingleString(string path, string command)
        {
            string fullPath = $"Data Source='{path}';Version=3; FailIfMissing=False";
            string result = string.Empty;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(fullPath))
                {
                    connection.Open(); // Open the connection
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = command;

                        object scalarResult = cmd.ExecuteScalar();
                        if (scalarResult != null)
                        {
                            result = scalarResult.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        public static DataTable GetRights(int userid)
        {
            DataTable table = new DataTable();
            string command = $"SELECT ModuleID, Read, Write, Edit, Del FROM Rights WHERE UserID = {userid}";
            Database.ReadData("Databases\\users.db", command, table);
            return table;
        }

        public static DataTable GetModules()
        {
            DataTable table = new DataTable();
            string command = $"SELECT ID, Name FROM Menu";
            Database.ReadData("Databases\\users.db", command, table);
            return table;
        }

        public static DataTable GetOrders()
        {
            DataTable table = new DataTable();
            Database.ReadData("Databases\\make.db", "SELECT * FROM 'Order'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "Дата заказа";
            table.Columns[2].ColumnName = "Отправитель";
            table.Columns[3].ColumnName = "Адрес отправителя";
            table.Columns[4].ColumnName = "ID получателя";
            table.Columns[5].ColumnName = "Адрес получателя";
            table.Columns[6].ColumnName = "Длина маршрута";
            table.Columns[7].ColumnName = "Стоимость заказа";
            table.Columns[8].ColumnName = "ID поездки";
            return table;
        }

        public static void DeleteOrder(int id, string value)
        {
            var command = $"DELETE FROM 'Order' WHERE ID = @Value1 AND Sender = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            Database.WriteData("Databases\\make.db", command, parameters);
        }

        public static DataTable GetValues(string database)
        {
            DataTable table = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM '{database}'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "Значение";
            return table;
        }









    }
}
