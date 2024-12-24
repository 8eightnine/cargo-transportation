using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cargo_transportation.Classes;
using cargo_transportation;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace Documents
{
    public class Documents
    {
        public static void ShowDocuments(MainForm mainForm)
        {
            ExportForm exportForm = new ExportForm();
            exportForm.ShowDialog();
            ExportParams parameters = exportForm.exportParams;
        }
        
        public static void ExportFile(ExportParams exportParams)
        {
            if (exportParams.reportType == 0) // Доставленные заказы
            {
                if (exportParams.exportToWord)
                    DataProcessor.ExportDeliveredOrdersToWord(exportParams.filterValue);
                if (exportParams.exportToExcel)
                    DataProcessor.ExportDeliveredOrdersToExcel(exportParams.filterValue);
            }

            if (exportParams.reportType == 1) // Свободные водители
            {
                if (exportParams.exportToWord)
                    DataProcessor.ExportAllAvailableDriversToWord();
                if (exportParams.exportToExcel)
                    DataProcessor.ExportAllAvailableDriversToExcel();
            }

            if (exportParams.reportType == 2) // Общие заказы
            {
                if (exportParams.exportToWord)
                    DataProcessor.ExportOrdersToWord();
                if (exportParams.exportToExcel)
                    DataProcessor.ExportOrdersToExcel();
            }

            if (exportParams.reportType == 3) // Грузы
            {
                if (exportParams.exportToWord)
                    DataProcessor.ExportAllCargoToWord();
                if (exportParams.exportToExcel)
                    DataProcessor.ExportAllCargoToExcel();
            }

            if (exportParams.reportType == 4) // Ремонт машин
            {
                if (exportParams.exportToWord)
                    DataProcessor.ExportCarsRepairedBeforeToWord(exportParams.filterValue);
                if (exportParams.exportToExcel)
                    DataProcessor.ExportCarsRepairedBeforeToExcel(exportParams.filterValue);
            }
        }
    }
}
