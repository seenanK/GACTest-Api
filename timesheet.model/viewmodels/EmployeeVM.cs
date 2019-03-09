using System;
using System.Collections.Generic;
using System.Text;

namespace timesheet.model.viewmodels
{
    public class EmployeeVM
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int totalweeklyeffort { get; set; }
        public double averageweeklyeffort { get; set; }
    }
}
