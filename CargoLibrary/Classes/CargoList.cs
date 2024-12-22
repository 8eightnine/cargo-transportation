using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo
{
    public class CargoList
    {
        private int _id;
        private int _orderID;
        private int _cargoID;
        private string _insuranceCost;
        private int _quantity;
        public int _isNew = 0;

        public int ID { get { return _id; } }
        public int OrderID { get { return _orderID; } set { _orderID = value; } }
        public int CargoID { get { return _cargoID; } set { _cargoID = value; } }
        public string InsuranceCost { get { return _insuranceCost; } set {_insuranceCost = value; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; } }

        public static CargoList ParseToCargoList(DataRow dr)
        {
            CargoList cargoList = new CargoList();
            Int32.TryParse(dr[0].ToString(), out cargoList._id);
            Int32.TryParse(dr[1].ToString(), out cargoList._orderID);
            Int32.TryParse(dr[2].ToString(), out cargoList._cargoID);
            cargoList._insuranceCost = dr[3].ToString();
            Int32.TryParse(dr[4].ToString(), out cargoList._quantity);

            return cargoList;
        }
    }
}
