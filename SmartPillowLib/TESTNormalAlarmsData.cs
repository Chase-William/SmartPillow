using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib
{
    /// <summary>
    ///     Testing class to hold alarms
    /// </summary>
    public static class TESTNormalAlarms
    {
        public static List<Alarm> Alarms = new List<Alarm>();

        static TESTNormalAlarms()
        {
            AddAlarm(15);
        }

        static void AddAlarm(byte times)
        {
            for (int i = 0; i < times; i++)
            {
                Alarms.Add(new Alarm
                {
                    PillowProps = new DeviceProperties
                    {
                        IsBrightnessEnabled = true,
                        IsVibrationEnabled = true,
                        IsEnabled = true,
                        Brightness = 50,
                        Vibration = 50
                    },
                    PhoneProps = new DeviceProperties
                    {
                        IsBrightnessEnabled = true,
                        IsVibrationEnabled = true,
                        IsEnabled = true,
                        Brightness = 50,
                        Vibration = 50
                    },
                    SnoozeProps = new SnoozeProperties
                    {
                        IsEnabled = true,
                        Interval = 10,
                        Repeat = 3
                    },
                    Id = i,
                    Name = i.ToString() + " My Favorite Alarm"
                });
            }        
        }
    }
}
