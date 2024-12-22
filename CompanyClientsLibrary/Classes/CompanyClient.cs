using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyClients
{
    public class CompanyClient
    {
        private int _id;
        private string _clientName;
        private string _ceoName;
        private string _address;
        private string _phoneNumber;
        private int _bankID;
        private string _bankSSN;
        public int _isNew = 0;

        public int ID { get { return _id; } }
        public string ClientName { get { return _clientName; } set { _clientName = value; } }
        public string CEOName { get { return _ceoName; } set { _ceoName = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public int BankID { get { return _bankID; } set { _bankID = value; } }
        public string BankSSN { get { return _bankSSN; } set { _bankSSN = value; } }



        public static CompanyClient ParseTo(DataRow dr)
        {
            CompanyClient compClient;
            if (dr == null)
                return null;
            else
            {
                compClient = new CompanyClient();
                compClient._id = Int32.Parse(dr.ItemArray[0].ToString());
                compClient.ClientName = dr.ItemArray[1].ToString();
                compClient.CEOName = dr.ItemArray[2].ToString();
                compClient.Address = dr.ItemArray[3].ToString();
                compClient.PhoneNumber = dr.ItemArray[4].ToString();
                compClient.BankID = Int32.Parse(dr.ItemArray[5].ToString());
                compClient.BankSSN = dr.ItemArray[6].ToString();

                return compClient;
            }
        }
    }
}
