using System;
using System.Collections.Generic;


namespace MBran.DayOfWeekPicker
{
    public class DaysOfWeek
    {
        public IEnumerable<string> Names { get; set; }
        public IEnumerable<string> AbbreviatedNames { get; set; }
        public IEnumerable<DayOfWeek> DayOfWeek { get; set; }
    }
}