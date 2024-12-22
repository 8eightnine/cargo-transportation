using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation;
using cargo_transportation.Classes;

namespace PhysClients
{
    public partial class PhysClientForm : Form
    {
        // Working variables
        PhysClient _physClient;

        public PhysClientForm(PhysClient physClient)
        {
            InitializeComponent();
            _physClient = physClient;
            FillForm(physClient);
        }

        private void FillForm(PhysClient physClient)
        {
            if (physClient._isNew == 0)
            {
                #region filling the values
                this.Text = $"Физическое лицо № {physClient.Id}";
                dateTimePicker1.Value = physClient.PassportDate;
                phoneNumberBox.Text = physClient.PhoneNumber;
                passportInfoBox.Text = physClient.PassportInfo;
                fullNameBox.Text = physClient.Name;
                passportIssuerBox.Text = physClient.PassportIssuer;
                
                #endregion
            }
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_physClient._isNew == 0)
                    {
                        string command = $"UPDATE 'Phys_Person' SET " +
                            $"FullName = '{fullNameBox.Text}', " +
                            $"PhoneNumber = '{phoneNumberBox.Text}', " +
                            $"PassportInfo = '{passportInfoBox.Text}', " +
                            $"PassportDate = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"PassportIssuer = '{passportIssuerBox.Text}' " +
                            $"WHERE ID = {_physClient.Id}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewPhysPerson();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private void CreateNewPhysPerson()
        {
            string command = $"INSERT INTO 'Phys_Person' (FullName, PhoneNumber, PassportInfo, PassportDate, PassportIssuer) VALUES" +
                            $"('{fullNameBox.Text}', " +
                            $"'{phoneNumberBox.Text}', " +
                            $"'{passportInfoBox.Text}', " +
                            $"'{dateTimePicker1.Value.ToString("dd.MM.yyyy")}', " +
                            $"'{passportIssuerBox.Text}')";
            Database.WriteData("Databases\\make.db", command, null);
            MessageBox.Show("Запись сохранена");
        }

        private int CheckValidity()
        {
            ValidityChecker.CheckIfStringValid(fullNameBox.Text, "ФИО");
            ValidityChecker.CheckIfStringValid(passportIssuerBox.Text, "Кем выдано");
            ValidityChecker.CheckPhone(phoneNumberBox.Text, "Контактный номер");

            foreach (char c in passportInfoBox.Text)
            {
                if (c >= '0' && c <= '9')
                {

                }
                else throw new Exception("Поле 'Паспорт' содержит некорректные символы");
            }

            if (passportInfoBox.Text.Length != 10)
            {
                throw new Exception("Поле 'Паспорт' содержит некорректное количество символов");
            }
   
            string command = $"SELECT CASE WHEN EXISTS ( SELECT 1 FROM Phys_Person WHERE PassportInfo = '{passportInfoBox.Text}' ) THEN 0 ELSE 1 END AS Result;";
            int result = Database.ReadSingleInt("Databases\\make.db", command);
            if (result == 0 && _physClient.PassportInfo != passportInfoBox.Text)
            {
                throw new Exception("Значение поля 'Паспорт' принадлежит другому человеку");
            }

            if (dateTimePicker1.Value > DateTime.Now)
            {
                throw new Exception("Поле 'Дата выдачи паспорта' содержит неккоректное значение");
            }

            return 1;
        }
    }
}
