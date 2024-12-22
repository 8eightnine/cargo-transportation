using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public static class ValidityChecker
    {
        public static int CheckIfStringValid(string value, string name)
        {
            if (value.Length != 0)
            {
                foreach (char c in value)
                {

                    if (!char.IsLetter(c) && c != ' ')
                    {
                        throw new Exception($"Поле '{name}' содержит недопустимые символы");
                    }

                }
            }
            else throw new Exception($"Поле '{name}' не может быть пустым");
            return 1;
        }

        public static int CheckIfIntValid(string value, string name)
        {
            if (value.Length != 0)
            {
                foreach (char c in value)
                {

                    if (!char.IsDigit(c))
                    {
                        throw new Exception($"Поле '{name}' содержит недопустимые символы");
                    }

                }

                int temp;
                if (Int32.TryParse(value, out temp) == false)
                    throw new Exception($"Поле '{name}' содержит неккоректное значение");
            }
            else throw new Exception($"Поле '{name}' не может быть пустым");
            return 1;
        }

        public static int CheckPhone(string value, string name)
        {
            if (value.Length != 0)
            {
                if (value.Length != 11)
                {
                    throw new Exception($"Поле '{name}' содержит некорректное количество символов");
                }

                if (!value.All(char.IsNumber))
                {
                    throw new Exception($"Поле '{name}' содержит недопустимые символы");
                }
            }
            else throw new Exception($"Поле '{name}' не может быть пустым");
            return 1;
        }
    }
}
