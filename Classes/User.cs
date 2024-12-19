using System;
using System.Data;

namespace cargo_transportation.Classes
{
    public struct Rights
    {
        public string name { get; set; }
        public int read { get; set; }
        public int write { get; set; }
        public int edit { get; set; }
        public int delete { get; set; }
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
                _password = Hash.hashPassword(value);
            }
            get
            {
                return _password;
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

        public void AddRights(DataTable rules, DataTable modules)
        {
            this.rights = new Rights[rules.Rows.Count];
            //MessageBox.Show(rules.Rows.Count.ToString());
            for (int i = 0; i < rules.Rows.Count; i++)
            {
                int libID = Int32.Parse(rules.Rows[i].ItemArray[0].ToString());
                // Присваиваем права
                this.rights[i].name = modules.Rows[libID - 1].ItemArray[1].ToString();
                this.rights[i].read = Int32.Parse(rules.Rows[i].ItemArray[1].ToString());
                this.rights[i].write = Int32.Parse(rules.Rows[i].ItemArray[2].ToString());
                this.rights[i].edit = Int32.Parse(rules.Rows[i].ItemArray[3].ToString());
                this.rights[i].delete = Int32.Parse(rules.Rows[i].ItemArray[4].ToString());


            }
            // Чистим ненужные данные
            rules.Dispose();
            modules.Dispose();
        }
    }
}
