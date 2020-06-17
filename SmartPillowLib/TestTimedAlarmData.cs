using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib
{
    /// <summary>
    ///     Testing class to hold alarms
    /// </summary>
    public static class TestTimedAlarmData
    {
        static List<Alarm> Alarms = new List<Alarm>();

        static TestTimedAlarmData()
        {
            Alarms.Add(new Alarm
            {
                Name = "Test Alarm"
            });
        }
    }
}
