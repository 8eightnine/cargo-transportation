using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using cargo_transportation;
using System.Threading.Tasks;

#nullable disable

namespace cargo_transportation.ViewModels
{
    public class AutorizationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly Authorization auth = new Authorization();
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; 
            }
        }

        public bool Autorize
        {
            get
            {
                return auth.Autorize(_login, _password);
            }
        }
    }
}
