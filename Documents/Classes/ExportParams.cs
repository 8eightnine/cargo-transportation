using System;

namespace Documents
{
    public class ExportParams
    {
        public bool exportToWord = false;
        public bool exportToExcel = false;
        public bool filterIsActive = false;
        public int reportType = -1;
        public DateTime filterValue = DateTime.Now;
    }
}
