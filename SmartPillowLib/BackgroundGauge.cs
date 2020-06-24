using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib
{
    public static class BackgroundGauge
    {
        public static RadialGaugeChart Gray { get; set; }
        static BackgroundGauge()
        {
            Gray = new RadialGaugeChart()
            {
                // creating data to Entries property
                Entries = new[] {

                    new Microcharts.Entry((float)1)
                    {
                        // transparent color 
                        Color = SKColor.Parse("#0a00000c"),
                    },
                    new Microcharts.Entry((float)1)
                    {
                        // gray color
                        Color = SKColor.Parse("#707070")
                    },
                },
                BackgroundColor = SKColors.Transparent,
                StartAngle = 0,
                LineAreaAlpha = 10,
                MaxValue = 1,
            };
        }
    }
}
