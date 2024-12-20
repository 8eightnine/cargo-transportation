﻿using System;
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
            ReadData("Databases\\users.db", command, table);
            return table;
        }

        public static DataTable GetModules()
        {
            DataTable table = new DataTable();
            string command = $"SELECT ID, Name FROM Menu";
            ReadData("Databases\\users.db", command, table);
            return table;
        }

        public static DataTable GetOrders()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Order'", table);
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

        public static DataTable GetTrips()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Trip'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "ID машины";
            table.Columns[2].ColumnName = "Дата доставки";
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
            WriteData("Databases\\make.db", command, parameters);
        }

        public static void DeleteTrip(int id, string value)
        {
            var command = $"DELETE FROM 'Trip' WHERE ID = @Value1 AND ArrivalDate = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            WriteData("Databases\\make.db", command, parameters);
        }

        public static DataTable GetValues(string database)
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", $"SELECT * FROM '{database}'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "Значение";
            return table;
        }

        public static void UpdateUser(User user)
        {
            string command = $"UPDATE Users SET Password = '{user.Password}' WHERE Username = '{user.Login}'";
            WriteData("Databases\\users.db", command, null);
        }

        public static DataTable GetPhysClients()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Phys_Person'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "ФИО";
            table.Columns[2].ColumnName = "Контактный номер";
            table.Columns[3].ColumnName = "Паспорт";
            table.Columns[4].ColumnName = "Дата выдачи";
            table.Columns[5].ColumnName = "Кем выдано";
            return table;
        }

        public static void DeletePhysClient(int id, string value)
        {
            var command = $"DELETE FROM 'Phys_Person' WHERE ID = @Value1 AND PhoneNumber = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            WriteData("Databases\\make.db", command, parameters);
        }

        public static DataTable GetCompanyClients()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Company_Person'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "ФИО контакта";
            table.Columns[2].ColumnName = "ФИО директора";
            table.Columns[3].ColumnName = "Адрес регистрации";
            table.Columns[4].ColumnName = "Контактный номер";
            table.Columns[5].ColumnName = "ID банка";
            table.Columns[6].ColumnName = "Номер банковского счета";
            return table;
        }

        public static void DeleteCompanyClient(int id, string value)
        {
            var command = $"DELETE FROM 'Company_Person' WHERE ID = @Value1 AND NameOfCEO = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            WriteData("Databases\\make.db", command, parameters);
        }

        public static DataTable GetCargo()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Cargo'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "Название";
            table.Columns[2].ColumnName = "Единица измерения";
            table.Columns[3].ColumnName = "Вес";

            return table;
        }

        public static void DeleteCargo(int id, string value)
        {
            var command = $"DELETE FROM 'Cargo' WHERE ID = @Value1 AND Name = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            WriteData("Databases\\make.db", command, parameters);
        }

        public static DataTable GetCargoLists()
        {
            DataTable table = new DataTable();
            ReadData("Databases\\make.db", "SELECT * FROM 'Cargo_List'", table);
            table.Columns[0].ColumnName = "ID";
            table.Columns[1].ColumnName = "ID заказа";
            table.Columns[2].ColumnName = "ID груза";
            table.Columns[3].ColumnName = "Страховая стоимость";
            table.Columns[4].ColumnName = "Количество";

            return table;
        }

        public static void DeleteCargoList(int id, string value)
        {
            var command = $"DELETE FROM 'Cargo_List' WHERE ID = @Value1 AND CargoID = @Value2";
            var parameters = new Dictionary<string, object>
                    {
                        { "@Value1", id},
                        { "@Value2", value }
                    };
            WriteData("Databases\\make.db", command, parameters);
        }
    }
}
