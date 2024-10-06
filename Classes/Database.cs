using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    internal class Database
    {
        private static string _path;
        private static string _name;
        private static SQLiteConnection _connection;
        private static SQLiteCommand _command;
        private string _status;


        public Database(string path) 
        {
            _path = path;
            _command = new SQLiteCommand();
            _status = "Connecting..";
        }

        public SQLiteConnection Connection {
            get
            {
                return _connection;
            }
            set 
            {
                _connection = value;
            }
        }

        public string Command {
            set
            {
                _command.CommandText = value;
            }
        }

        public string Path { 
            set
            {
                _path = value; 
            }
        }

        public string Name
        {
            set
            {
                _name = value;
            }
        }

        public string Status { 
            get
            {
                return _status; 
            }
        }

        public int Connect()
        {
            try
            {
                _connection = new SQLiteConnection("Data Source=" + _path + ";Version=3; FailIfMissing=False");
                _connection.Open();
                _command.Connection = _connection;
                _status = "Connected";
                return 1;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
                return 0;
            }
        }

        public int ExecuteCommand()
        {
            return _command.ExecuteNonQuery();
        }

        public SQLiteDataAdapter GetDataAdapter(DataTable data)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(_command);
            adapter.Fill(data);
            return adapter;
        }

        public SQLiteDataReader ReadData()
        {
            SQLiteDataReader reader = _command.ExecuteReader();
            return reader;
        }

        public void Clear()
        {
            _command.Dispose();
        }
    }
}
