using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysClients
{
    public class PhysClient
    {
        private int _id;
        private string _name;
        private string _phoneNumber;
        private string _passportInfo;
        private DateTime _passportDate;
        private string _passportIssuer;
        public int _isNew = 0;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public string PassportInfo { get { return _passportInfo; } set { _passportInfo = value; } }
        public DateTime PassportDate { get { return _passportDate; } set { _passportDate = value; } }
        public string PassportIssuer { get { return _passportIssuer; } set { _passportIssuer = value; } }


        public static PhysClient ParseTo(DataRow dr)
        {
            PhysClient physClient;
            if (dr == null)
                return null;
            else
            {
                physClient = new PhysClient();
                physClient._id = Int32.Parse(dr.ItemArray[0].ToString());
                physClient.Name = dr.ItemArray[1].ToString();
                physClient.PhoneNumber = dr.ItemArray[2].ToString();
                physClient.PassportInfo = dr.ItemArray[3].ToString();
                physClient.PassportDate = DateTime.ParseExact(dr.ItemArray[4].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                physClient.PassportIssuer = dr.ItemArray[5].ToString();

                return physClient;
            }
        }
    }
}
