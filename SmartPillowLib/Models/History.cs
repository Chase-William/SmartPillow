using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

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
        public string Month { get; set; }
        public string Year { get; set; }
        public string DateRange { get; set; }
        public ObservableCollection<Day> Days { get; set; }
        public PointChart SleepQualityChart { get; set; }
    }

    public class Day
    {
        //public string Month { get; set; }
        //public string SpecificDay { get; set; }
        public string CurrentDate { get; set; }
        //public int Start { get; set; }
        public string StartTime { get; set; }
        //public int End { get; set; }
        public string EndTime { get; set; }
        //public string Year { get; set; }
        public TimeSpan TotalSleep { get; set; }
        //public TimeSpan AwakeDuration { get; set; }
        public string AwakePercentage { get; set; }
        public string AwakeTime { get; set; }
        //public TimeSpan RemDuration { get; set; }
        public string RemPercentage { get; set; }
        public string RemTime { get; set; }
        //public TimeSpan SleepDuration { get; set; }
        public string SleepPercentage { get; set; }
        public string SleepTime { get; set; }
        //public TimeSpan DeepDuration { get; set; }
        public string DeepPercentage { get; set; }
        public string DeepTime { get; set; }
        public PointChart EventChart { get; set; }
        public List<AlertEvent> Alerts { get; set; }
        public LineChart LineChart { get; set; }
        public PointChart SnoreChart { get; set; }
        public int Snores { get; set; }
        public Thickness Margin { get; set; }
        public Thickness SnoreMargin { get; set; }
        public double Width { get; set; }
        public bool IsVisible { get; set; }
        public double Rotation { get; set; }
        //public RadialGaugeChart Background { get; set; }
        //public RadialGaugeChart Quality { get; set; }
        //public RadialGaugeChart Awake { get; set; }
        //public RadialGaugeChart Rem { get; set; }
        //public RadialGaugeChart Sleep { get; set; }
        //public RadialGaugeChart Deep { get; set; }
        //public string Mood { get; set; }
    }

    public class AlertEvent
    {
        public string Name { get; set; }
        //public TimeSpan Time { get; set; }
        public SKColor Color { get; set; }
        public string AtTime { get; set; }
    }
}
