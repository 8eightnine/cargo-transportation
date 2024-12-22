using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using cargo_transportation;
using cargo_transportation.Classes;

namespace CompanyClients
{
    public partial class CompanyClientForm : Form
    {
        // Working variables
        CompanyClient _compClient;
        private int _bankID;

        public CompanyClientForm(CompanyClient compClient)
        {
            InitializeComponent();
            _compClient = compClient;
            FillForm(compClient);
        }

        private void FillForm(CompanyClient physClient)
        {
            if (_compClient._isNew == 0)
            {
                #region filling the values
                this.Text = $"Юридическое лицо № {_compClient.ID}";
                contactNameBox.Text = _compClient.ClientName;
                ceoNameBox.Text = _compClient.CEOName;
                addressBox.Text = _compClient.Address;
                phoneNumberBox.Text = _compClient.PhoneNumber;
                bankSSNBox.Text = _compClient.BankSSN;
                #endregion
            }
            FillBankID();
        }

        private void SaveEntry(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    if (_compClient._isNew == 0)
                    {
                        string command = $"UPDATE 'Company_Person' SET " +
                            $"ClientName = '{contactNameBox.Text}', " +
                            $"NameOfCEO = '{ceoNameBox.Text}', " +
                            $"Address = '{addressBox.Text}', " +
                            $"PhoneNumber = '{phoneNumberBox.Text}', " +
                            $"BankID = '{_bankID}', " +
                            $"BankSSN = '{bankSSNBox.Text}' " +
                            $"WHERE ID = {_compClient.ID}";
                        Database.WriteData("Databases\\make.db", command, null);
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                        CreateNewCompPerson();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private void CreateNewCompPerson()
        {
            string command = $"INSERT INTO 'Company_Person' (ClientName, NameOfCEO, Address, PhoneNumber, BankID, BankSSN) VALUES" +
                            $"('{contactNameBox.Text}', " +
                            $"'{ceoNameBox.Text}', " +
                            $"'{addressBox.Text}', " +
                            $"'{phoneNumberBox.Text}', " +
                            $"{_bankID}, " +
                            $"'{bankSSNBox.Text}')";
            Database.WriteData("Databases\\make.db", command, null);
            MessageBox.Show("Запись сохранена");
        }

        private int CheckValidity()
        {
            ValidityChecker.CheckIfStringValid(contactNameBox.Text, "ФИО контактного лица");
            ValidityChecker.CheckIfStringValid(ceoNameBox.Text, "ФИО руководителя");
            ValidityChecker.CheckIfIntValid(_bankID.ToString(), "ID банка");

            if (bankSSNBox.Text.Length != 0)
            {
                if (!bankSSNBox.Text.All(char.IsDigit))
                {
                    throw new Exception("Поле 'Номер счета' содержит недопустимые символы");
                }
            }
            else throw new Exception($"Поле 'Номер счета' не может быть пустым");

            ValidityChecker.CheckPhone(phoneNumberBox.Text, "Контактный номер");

            string command = $"SELECT CASE WHEN EXISTS ( SELECT 1 FROM Company_Person WHERE BankSSN = '{_compClient.BankSSN}' ) THEN 1 ELSE 0 END AS Result;";
            int result = Database.ReadSingleInt("Databases\\make.db", command);
            if (result == 1 && _compClient.BankSSN != bankSSNBox.Text)
            {
                throw new Exception("Этот номер банковского счета уже привязан к другому юридическому лицу");
            }

            return 1;
        }

        public void FillBankID()
        {
            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Bank'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                bankIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt.Dispose();
            bankIDComboBox.SelectedIndex = _compClient.BankID - 1;
        }

        private void bankIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bankIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _bankID = selectedItem.Id;
            }
        }
    }
}

public class ComboBoxItem
{
    public int Id { get; set; }
    public string DisplayText { get; set; }

    public override string ToString()
    {
        return DisplayText;
    }
}