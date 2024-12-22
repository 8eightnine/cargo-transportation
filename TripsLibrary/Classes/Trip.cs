using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cargo_transportation.Classes;

namespace Trips
{
    public class Trip
    {
        private int _ID;
        private int _carID;
        private DateTime _arrivalDate;
        private int _driverID = -1;
        public int _isNew;

        public int CarID {  get { return _carID; } set { _carID = value; } }
        public int ID { get { return _ID; } }
        public int DriverID { get { return _driverID; }  set { _driverID = value; } }
        public DateTime ArrivalDate { get { return _arrivalDate; } set { _arrivalDate = value; } }


        public static Trip ParseToTrip(DataRow dr)
        {
            Trip trip;
            if (dr == null)
                return null;
            else
            {
                trip = new Trip();
                trip._ID = Int32.Parse(dr.ItemArray[0].ToString());
                trip.CarID = Database.ReadSingleInt("Databases\\make.db", $"SELECT ID FROM Car_List WHERE Model = '{dr.ItemArray[1].ToString()}'");
                trip.ArrivalDate = DateTime.ParseExact(dr.ItemArray[2].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                trip.DriverID = Int32.Parse(dr.ItemArray[3].ToString());

                return trip;
            }
        }

    }
}
