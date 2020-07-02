using LiteDB;
using Microcharts;
using SkiaSharp;

namespace SmartPillowLib.Models
{
    public class Example
    {
        public PointChart EventChart { get; set; }
        public LineChart LineChart { get; set; }
        public PointChart SnoreChart { get; set; }

        static string[] LineColors = new string[] { "#7AC0DF", "#A794EE", "#D06BFC", "#92A9E7", "#BC7FF5" };

        static int count = 0;

        public static Example Converter(Example example)
        {
            example.EventChart.Margin = 0;
            foreach (var item in example.EventChart.Entries)
            {
                switch(item.Label)
                {
                    case "Baby": item.Color = SKColor.Parse("#FFC4F7"); break;
                    case "Doorbell": item.Color = SKColor.Parse("#B1FFD5"); break;
                    case "Car": item.Color = SKColor.Parse("#91BDFF"); break;
                }
            }

            foreach (var item in example.LineChart.Entries)
            {
                count = (count > 3) ? 0 : count;
                item.Color = SKColor.Parse(LineColors[count]);
                item.TextColor = SKColor.Parse("#B2B2B2");
                count++;
            }

            example.SnoreChart.Margin = 20;
            foreach (var item in example.SnoreChart.Entries)
                if (item.Value == .35f)
                    item.Color = SKColor.Parse("#00C2FF");

            return example;
        }
    }
}
