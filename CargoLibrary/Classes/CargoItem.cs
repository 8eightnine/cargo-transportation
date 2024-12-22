using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo
{
    public class CargoItem
    {
        private int _ID;
        private string _name;
        private string _unit;
        private int _weight;
        public int _isNew = 0;

        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Unit { get { return _unit; } set { _unit = value; } }
        public int Weight { get { return _weight; } set { _weight = value; } }

        public static CargoItem ParseToCargoItem(DataRow dr)
        {
            CargoItem cargoItem = new CargoItem();
            cargoItem._ID = Int32.Parse(dr[0].ToString());
            cargoItem.Name = dr[1].ToString();
            cargoItem.Unit = dr[2].ToString();
            cargoItem.Weight = Int32.Parse(dr[3].ToString());
            return cargoItem;
        }


    }
}
