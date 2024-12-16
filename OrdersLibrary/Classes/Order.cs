using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    public class Order
    {
        private int _ID;
        private DateTime _orderDate;
        private string _senderName;
        private string _senderAddress;
        private int _clientID;
        private string _clientAddress;
        private int _tripLength;
        private int _cost;
        private int _cargoListID;
        private int _tripID;

        // Cargo list
        //TODO

        public Order()
        {

        }

        public Order(int iD, DateTime dateTime, string senderName, string senderAddress, int clientID, string clientAddress, int tripLength, int cost, int cargoListID, int tripId)
        {
            _ID = iD;
            _orderDate = dateTime;
            _senderName = senderName;
            _senderAddress = senderAddress;
            _clientID = clientID;
            _clientAddress = clientAddress;
            _tripLength = tripLength;
            _cost = cost;
            _cargoListID = cargoListID;
            _tripID = tripId;
        }

        public int ID { get { return _ID; } }
        public DateTime OrderDate { get { return _orderDate; } set { _orderDate = value; } }
        public string SenderName {  get { return _senderName; } set { _senderName = value; } }
        public string SenderAddress {  get { return _senderAddress; } set {_senderAddress = value; } }
        public int ClientID { get { return _clientID; } set { _clientID = value; } }
        public string ClientAddress { get { return _clientAddress; } set { _clientAddress = value; } }
        public int TripLength { get { return _tripLength; } set { _tripLength = value; } }
        public int Cost { get { return _cost; } set { _cost = value; } }
        public int CargoListID { get {return _cargoListID; } set {_cargoListID = value; } }
        public int TripID {  get { return _tripID; } set {_tripID = value; } }


        public static Order ParseToOrder(DataRow dr)
        {
            Order order;
            if (dr == null)
                return null;
            else
            {
                order = new Order();
                order._ID = Int32.Parse(dr.ItemArray[0].ToString());
                order._orderDate = DateTime.ParseExact(dr.ItemArray[1].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                order._senderName = dr.ItemArray[2].ToString();
                order._senderAddress = dr.ItemArray[3].ToString();
                order._clientID = Int32.Parse(dr.ItemArray[4].ToString());
                order._clientAddress = dr.ItemArray[5].ToString();
                Int32.TryParse(dr.ItemArray[6].ToString(), out order._tripLength);
                Int32.TryParse(dr.ItemArray[7].ToString(), out order._cost);
                Int32.TryParse(dr.ItemArray[8].ToString(), out order._cargoListID);
                Int32.TryParse(dr.ItemArray[9].ToString(), out order._tripID);
                
                return order;
            }
        }

        // TODO!
        public int CheckForValidity()
        {
            return 0;
        }

        // TODO!
        public Dictionary<string, object> ToParameters()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Value1", "test"},
                { "@Value2", "test"},
                { "@Value3", "test"},
                { "@Value4", "test"},
                { "@Value5", "test"},
                { "@Value6", "test"},
                { "@Value7", "test"},
                { "@Value8", "test"},
                { "@Value9", "test"},
                { "@Value10", "test"},
            };
            
            return parameters;
        }
    }
}
