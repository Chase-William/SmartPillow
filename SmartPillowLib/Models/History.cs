using Microcharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartPillowLib.Models
{
    public class History
    {
        private string month;
        private string year;
        public DateTime Date { get; set; }

        public string Month
        {
            get => Date.ToString("MMMM");
            set => month = value; 
        }

        public string Year
        {
            get => year = Date.Year.ToString();
            set => year = value;
        }
        public List<Week> Weeks { get; set; }
    }

    public class Week
    {
        public string DateRange { get; set; }
        public List<Day> Days { get; set; }
        public PointChart SleepQualityChart { get; set; }
    }

    public class Day
    {
        public string SpecificDay { get; set; }
    }
}
