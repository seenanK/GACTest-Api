using System;
using System.Collections.Generic;
using System.Text;

namespace timesheet.model.viewmodels
{
    public class TimeSheetOutputVM
    {
        public string task { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
    }
}
