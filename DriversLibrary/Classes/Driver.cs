using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drivers
{
    public class Driver
    {
        private int _id;
        private int _tableNumber;
        private string _name;
        private int _experience;
        private int _category;
        private int _class;
        private DateTime _dateOfBirth;
        public int _isNew = 0;


        public int Id { get { return _id; } }
        public int TableNumber { get { return _tableNumber; } set { _tableNumber = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int Experience { get { return _experience; } set { _experience = value; } }
        public int Category { get { return _category; } set { _category = value; } }
        public int Class { get { return _class; } set { _class = value; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; } }


        public static Driver ParseToOrder(DataRow dr)
        {
            Driver driver;
            if (dr == null)
                return null;
            else
            {
                driver = new Driver();
                driver._id = Int32.Parse(dr.ItemArray[0].ToString());
                driver.Name = dr.ItemArray[1].ToString();
                driver.DateOfBirth = DateTime.ParseExact(dr.ItemArray[3].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                driver.TableNumber = Int32.Parse(dr.ItemArray[2].ToString());
                driver.Experience = Int32.Parse(dr.ItemArray[4].ToString());
                driver.Category = Int32.Parse(dr.ItemArray[5].ToString());
                driver.Class = Int32.Parse(dr.ItemArray[6].ToString());

                return driver;
            }
        }
    }
}
