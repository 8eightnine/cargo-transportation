namespace cargo_transportation.Classes
{
    public class User
    {
        private string _login;
        private string _password;
        private bool _read;
        private bool _write;
        private bool _edit;
        private bool _delete;

        public User(string login, string password, bool r, bool w, bool e, bool d)
        {
            _login = login;
            _password = password;
            _read = r;
            _write = w;
            _edit = e;
            _delete = d;
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
    }
}
