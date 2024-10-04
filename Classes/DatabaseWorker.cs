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
    internal static class DatabaseWorker
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;

        public static SQLiteConnection Connect(string fileName)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3; FailIfMissing=False");
                connection.Open();
                return connection;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
                return null;
            }
        }

        public static SQLiteDataAdapter GetDataAdapter(DataTable data, SQLiteCommand command)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);
            return adapter;
        }
    }
}
