using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public static class ErrorHandler
    {
        public static void PasswordLengthError()
        {
            MessageBox.Show("Пароль должен содержать более 5 символов", "Ошибка при регистрации");
        }

        public static void LoginLengthError()
        {
            MessageBox.Show("Логин должен содержать более 5 символов", "Ошибка при регистрации");
        }

        public static void DuplicateLoginError()
        {
            MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка при регистрации");
        }

        public static void CredentialsError()
        {
            MessageBox.Show("Логин или пароль введены неверно или такой пользователь не существует", "Ошибка при авторизации");
        }
    }
}
