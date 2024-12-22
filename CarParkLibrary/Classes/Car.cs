using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark
{
    public class Car
    {
        private int _id;
        private string _stateNumber;
        private int _brandID;
        private string _model;
        private int _weightLimit;
        private int _usage;
        private DateTime _issueDate;
        private DateTime _repairDate;
        private int _mileage;
        private string _photo;
        public int _isNew;

        public int ID { get { return _id; } }
        public string StateNumber { get { return _stateNumber; } set { _stateNumber = value; } }
        public int BrandID { get { return _brandID; } set { _brandID = value; } }
        public string Model { get { return _model; } set { _model = value; } }
        public int WeightLimit { get { return _weightLimit; } set { _weightLimit = value; } }
        public int Usage { get { return _usage; } set { _usage = value; } }
        public DateTime IssueDate { get { return _issueDate; } set { _issueDate = value; } }
        public DateTime RepairDate { get { return _repairDate; } set { _repairDate = value; } }
        public int Mileage { get { return _mileage; } set { _mileage = value; } }
        public string Photo { get { return _photo; } set { _photo = value; } }


        public static Car ParseToCar(DataRow dr)
        {
            Car car = new Car();
            car._id = Int32.Parse(dr[0].ToString());
            car._stateNumber = dr[1].ToString();
            car._brandID = Int32.Parse(dr[2].ToString());
            car._model = dr[3].ToString();
            car._weightLimit = Int32.Parse(dr[4].ToString());
            car._usage = Int32.Parse(dr[5].ToString());
            car._issueDate = DateTime.ParseExact(dr[6].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            car._repairDate = DateTime.ParseExact(dr[7].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            car._mileage = Int32.Parse(dr[8].ToString());
            car._photo = dr[9].ToString();
            return car;
        }

    }
}
