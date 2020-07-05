using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillow.Util
{
    public class AutoBrightness
    {
        /// <summary>
        ///     HomePage's brightness will be darker if local time is in between 12AM and 6AM
        /// </summary>
        public static string CheckNightTime()
        {
            // starts at midnight
            var nightStart = DateTime.Now.Date.AddDays(0);

            // ends at 6 am
            var nightEnd = nightStart.Date.AddHours(6);

            // Black background with Opacity is 60%, otherwise 0%
            return (nightStart < DateTime.Now && DateTime.Now < nightEnd) ? "#99000000" : "#00000000";
        }
    }
}
