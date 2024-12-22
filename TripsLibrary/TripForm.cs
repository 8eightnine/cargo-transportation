using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cargo_transportation.Classes;

namespace Trips
{
    public partial class TripForm : Form
    {
        Trip _currentTrip;
        private int _carID = -1;
        public TripForm(Trip trip)
        {
            InitializeComponent();
            _currentTrip = trip;
            FillForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentTrip._isNew == 0)
            {
                try
                {
                    if (CheckValidity() == 1)
                    {
                        string command = $"UPDATE 'Trip' SET Car = {_carID}, ArrivalDate = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}' WHERE ID = {_currentTrip.ID};";
                        Database.WriteData("Databases\\make.db", command);

                        if (_currentTrip.DriverID != -1)
                        {
                            command = $"UPDATE 'Trip_List' SET DriverID = '{_currentTrip.DriverID}' WHERE ID = '{_currentTrip.ID}';";
                            Database.WriteData("Databases\\make.db", command);
                        }

                        MessageBox.Show("Запись сохранена");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
                CreateNewTrip();
        }

        private int CheckValidity()
        {
            string command = $"SELECT CASE WHEN EXISTS ( SELECT 1 FROM Trip WHERE Car = {_carID} ) THEN 0 ELSE 1 END AS Result;";
            int result = Database.ReadSingleInt("Databases\\make.db", command);
            if (result == 0 && _currentTrip.CarID != _carID)
            {
                throw new Exception("Значение поля 'ID машины' уже есть в системе, используйте ID свободной машины");
            }

            return 1;
        }

        private void CreateNewTrip()
        {
            try
            {
                if (CheckValidity() == 1)
                {
                    string command = $"INSERT INTO 'Trip' (Car, ArrivalDate) VALUES ({_carID}, '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}');";
                    Database.WriteData("Databases\\make.db", command);

                    if (_currentTrip.DriverID != -1)
                    {
                        command = $"INSERT INTO 'Trip_List' (ID, DriverID) VALUES ({_currentTrip.ID}, {_currentTrip.DriverID});";
                        Database.WriteData("Databases\\make.db", command);
                    }
                    MessageBox.Show("Запись сохранена");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void FillForm()
        {
            if (_currentTrip._isNew != 1)
            {
                dateTimePicker1.Value = _currentTrip.ArrivalDate;
            }

            DataTable dt = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Car_List'", dt);
            foreach (DataRow dr in dt.Rows)
            {
                carIDComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[3].ToString()
                });
            }
            dt.Dispose();
            if (_currentTrip._isNew != 1)
                carIDComboBox.SelectedIndex = _currentTrip.CarID - 1;
            DataTable dt1 = new DataTable();
            Database.ReadData("Databases\\make.db", $"SELECT * FROM 'Driver'", dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                driverComboBox.Items.Add(new ComboBoxItem
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    DisplayText = dr[1].ToString()
                });
            }
            dt1.Dispose();
            if (_currentTrip._isNew != 1)
                driverComboBox.SelectedIndex = _currentTrip.DriverID - 1;
        }

        private void carIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carIDComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _carID = selectedItem.Id;
            }
        }

        private void driverComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (driverComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _currentTrip.DriverID = selectedItem.Id;
            }
        }
    }
}
public class ComboBoxItem
{
    public int Id { get; set; }
    public string DisplayText { get; set; }

    public override string ToString()
    {
        return DisplayText;
    }
}