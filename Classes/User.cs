using System.Data;

namespace cargo_transportation.Classes
{
    public struct Rights
    {
        public string name;
        public int read;
        public int write;
        public int edit;
        public int delete;
    };

    public class User
    {
        public Rights[] rights;
        private string _login;
        private string _password;
        private bool _read;
        private bool _write;
        private bool _edit;
        private bool _delete;

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public string Password
        {
            set
            {
                Password = Hash.Hash.hashPassword(value);
            }
        }
        public string Login
        {
            get { return _login; }
        }

        public bool Read
        {
            get { return _read; }
            set { _read = value; }
        }
        public bool Write
        {
            get { return _write; }
            set { _write = value; }
        }
        public bool Edit
        {
            get { return _edit; }
            set
            {
                _edit = value;
            }
        }

        public bool Delete
        {
            get { return _delete; }
            set { _delete = value; }
        }

        public void AddRights(DataTable data)
        {
            for(int i = 0; i < data.Rows.Count; i++) 
            {
                string name = data.Rows[i].ItemArray[0].ToString();
            }
        }
    }
}
