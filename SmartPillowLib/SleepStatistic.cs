using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using SmartPillowLib.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace SmartPillowLib
{
    public static class SleepStatistic
    {
        public static Stopwatch sw = new Stopwatch();

        // User's goal to sleep for 8 hours minimum
        public static TimeSpan GoalSleep = new TimeSpan(8, 0, 0);

        // User's total sleep duration
        public static TimeSpan TotalSleep { get; set; }
        public static double Quality { get; set; }
        public static RadialGaugeChart QualityGauge { get; set; }

        public static TimeSpan AwakeDuration { get; set; }
        public static double AwakePercentage { get; set; }
        public static RadialGaugeChart AwakeGauge { get; set; }

        public static TimeSpan RemDuration { get; set; }
        public static double RemPercentage { get; set; }
        public static RadialGaugeChart RemGauge { get; set; }

        public static TimeSpan SleepDuration { get; set; }
        public static double SleepPercentage { get; set; }
        public static RadialGaugeChart SleepGauge { get; set; }

        public static TimeSpan DeepDuration { get; set; }
        public static double DeepPercentage { get; set; }
        public static RadialGaugeChart DeepGauge { get; set; }

        // Charts with example values
        public static PointChart EventChart { get; set; }
        public static LineChart LineChart { get; set; }
        public static PointChart SnoreChart { get; set; }

        static SleepStatistic()
        {
            /// <summary>
            ///     Calculating percentage for Quality, Awake, REM, Sleep, and Deep with example values
            /// </summary>

            // Example values
            TotalSleep = new TimeSpan(7, 12, 12);
            AwakeDuration = new TimeSpan(4, 2, 12);
            RemDuration = new TimeSpan(1, 32, 12);
            SleepDuration = new TimeSpan(1, 12, 12);
            DeepDuration = new TimeSpan(2, 45, 12);

            SetExampleValues();

            // Calculating for all percentages
            Quality = Math.Round((TotalSleep.TotalSeconds / GoalSleep.TotalSeconds * 100), 2);
            AwakePercentage = Math.Round((AwakeDuration.TotalSeconds / TotalSleep.TotalSeconds * 100), 2);
            RemPercentage = Math.Round((RemDuration.TotalSeconds / TotalSleep.TotalSeconds * 100), 2);
            SleepPercentage = Math.Round((SleepDuration.TotalSeconds / TotalSleep.TotalSeconds * 100), 2);
            DeepPercentage = Math.Round((DeepDuration.TotalSeconds / TotalSleep.TotalSeconds * 100), 2);

            // Setting radial gauge charts up
            QualityGauge = SetChartUp(Quality);
            AwakeGauge = SetChartUp(AwakePercentage);
            RemGauge = SetChartUp(RemPercentage);
            SleepGauge = SetChartUp(SleepPercentage);
            DeepGauge = SetChartUp(DeepPercentage);
        }

        /// <summary>
        ///     When user presses "Start Sleep"
        /// </summary>
        public static void StartSleep()
        {
            sw.Start();
        }

        /// <summary>
        ///     When user presses "End Sleep" <- Still have to figure how I should implement the function for UI
        /// </summary>
        public static void EndSleep()
        {
            sw.Stop();
            if (sw.Elapsed != null)
                TotalSleep = sw.Elapsed;

            sw.Reset();
        }

        static RadialGaugeChart SetChartUp(double percentage)
        {
            var chart = new RadialGaugeChart()
            {
                Entries = new[]
                {
                    // data for radial gauge chart
                    new Microcharts.Entry((float)1)
                    {
                        // transparent color 
                        Color = SKColor.Parse("#0a00000c"),
                    },
                    new Microcharts.Entry((float)percentage / 100)
                    {
                        // light blue color 
                        Color = SKColor.Parse("#7AC0DF")
                    },
                },
                BackgroundColor = SKColors.Transparent,
                StartAngle = 0,
                LineAreaAlpha = 15,
                MaxValue = 1
            };
            return chart;
        }

        static void SetExampleValues()
        {
            var list = new Example();
            var client = new WebClient();
            Stream all = client.OpenRead("https://people.rit.edu/qpn6238/wuw/exampleValues.json");
            using (StreamReader reader = new StreamReader(all))
            {
                var page = reader.ReadToEnd();

                // All SKColor values are returned as 0 when deserializing <Frustration>
                // I have to use Example.Converter method to get actual color values
                list = JsonConvert.DeserializeObject<Example>(page);
            }

            // Getting actual SKColor values
            var coloredList = Example.Converter(list);

            EventChart = coloredList.EventChart;
            LineChart = coloredList.LineChart;
            SnoreChart = coloredList.SnoreChart;
        }

        // ------ Methods below aren't fully functioned since we don't have a smart pillow to connect ------ //
        // ------ and signal connection between the smart pillow and mobile hasn't been invested ------ //

        static Stopwatch awakeWatch;
        static Stopwatch remWatch;
        static Stopwatch sleepWatch;
        static Stopwatch deepWatch;

        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's awake been appeared
        /// </summary>
        static void AwakeAppeared() => StartWatch(awakeWatch);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's awake becomes faded away fully
        /// </summary>
        static void AwakeFaded() => EndWatch(awakeWatch, AwakeDuration);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's rem has been appeared 
        /// </summary>
        static void RemAppeared() => StartWatch(remWatch);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's rem becomes faded away fully
        /// </summary>
        static void RemFaded() => EndWatch(remWatch, RemDuration);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's sleep has been appeared 
        /// </summary>
        static void SleepAppeared() => StartWatch(sleepWatch);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's sleep becomes faded away fully
        /// </summary>
        static void SleepFaded() => EndWatch(sleepWatch, SleepDuration);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's deep has been appeared 
        /// </summary>
        static void DeepAppeared() => StartWatch(deepWatch);


        /// <summary>
        ///     Invokes this method when smart pillow or 
        ///     phone device detects user's deep becomes faded away fully
        /// </summary>
        static void DeepFaded() => EndWatch(deepWatch, DeepDuration);


        /// <summary>
        ///     Start watching user's specific stage when the stage appears
        ///     during user's sleep time
        /// </summary>
        /// <param name="watch"></param>
        static void StartWatch(Stopwatch watch)
        {
            if (watch == null)
                watch = new Stopwatch();

            watch.Start();
        }

        /// <summary>
        ///     Stop watching user's specific stage when the stage disappears
        ///     during user's sleep time
        /// </summary>
        /// <param name="watch"></param>
        /// <param name="stage"></param>
        static void EndWatch(Stopwatch watch, TimeSpan stage)
        {
            watch.Stop();

            stage += watch.Elapsed;
            watch.Reset();
        }
    }
}
