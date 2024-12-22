using System;
using System.Data;

namespace Orders
{
    public class OrderClient
    {
        public int _id;
        public string _fullName;
        public string _phoneNumber;
        public int _physPersonID;
        public int _companyPersonID;
        public int _isNew;

        public string _displayText;

        public override string ToString()
        {
            return _displayText;
        }

        public static OrderClient ParseToClient(DataRow dr)
        {
            OrderClient client = new OrderClient();
            client._id = Int32.Parse(dr[0].ToString());
            client._fullName = dr[1].ToString();
            client._phoneNumber = dr[2].ToString();
            Int32.TryParse(dr[3].ToString(), out client._physPersonID);
            Int32.TryParse(dr[4].ToString(), out client._companyPersonID);
            return client;
        }
    }
}
