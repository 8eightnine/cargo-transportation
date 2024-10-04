namespace cargo_transportation.Classes
{
    internal class User
    {
        private string _login;
        private string _password;
        private bool _read;
        private bool _write;
        private bool _gagaga;
        private bool _jujuju;

        public string Password
        {
            set
            {
                this.Password = Hash.Hash.hashPassword(value);
            }
        }
        public string Login
        {
            get { return this._login; }
        }

        public bool Read
        {
            get { return this._read; }
            set { this._read = value; }
        }
        public bool Write
        {
            get { return this._write; }
            set { this._write = value; }
        }
        public bool Gagaga
        {
            get { return this._gagaga; }
            set
            {
                this._gagaga = value;
            }
        }

        public bool Jujuju
        {
            get { return this._jujuju; }
            set { this._jujuju = value; }
        }
    }
}
