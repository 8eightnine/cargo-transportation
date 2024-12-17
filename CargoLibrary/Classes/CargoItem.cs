using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Classes
{
    public class CargoItem
    {
        private int _ID;
        private string _name;
        private string _unit;
        private int _weight;

        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Unit { get { return _unit; } set { _unit = value; } }
        public int Weight { get { return _weight; } set { _weight = value; } }

        public CargoItem() { }


    }
}
