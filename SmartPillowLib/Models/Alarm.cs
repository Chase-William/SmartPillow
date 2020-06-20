using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.Models
{
    /// <summary>
    ///     Base class for all alarm types.
    /// </summary>
    public class Alarm
    {
        /// <summary>
        ///     Name of the alarm.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Path and name of image file.
        /// </summary>
        public string ImgPath { get; set; }              
        /// <summary>
        ///     Bool indicating whether the alarm all together should fade in when going off.
        /// </summary>
        public bool IsFadeEnabled { get; set; }
        /// <summary>
        ///     Bool indicates whether this alarm is enabled.
        /// </summary>
        public bool IsAlarmEnabled { get; set; }
        /// <summary>
        ///     String which the user can put any notes about the alarm in. 
        /// </summary>        
        public string Note { get; set; }

        /// <summary>
        ///     Contains properties for customizing the user's desire snooze duration and interval.
        /// </summary>
        public Snooze SnoozeProps { get; set; }

        #region Device Properties
        /// <summary>
        ///     Contains the properties for this alarm related to the pillow.
        /// </summary>
        public DeviceProps PillowProps { get; set; }
        /// <summary>
        ///     Contains the properties for this alarm relate to the phone.
        /// </summary>
        public DeviceProps PhoneProps { get; set; }
        #endregion       
    }

    /// <summary>
    ///     Class that defines the common properties for both the Smart Pillow and the Phone.
    /// </summary>
    public class DeviceProps
    {
        /// <summary>
        ///     Value for vibration 0 to 100.
        /// </summary>
        public byte Vibration { get; set; }
        /// <summary>
        ///     Bool indicating whether vibration is enabled reguardless of Vibration's value.
        /// </summary>
        public bool IsVibrationEnabled { get; set; }
        /// <summary>
        ///     Value for vibration 0 to 100.
        /// </summary>
        public byte Brightness { get; set; }
        /// <summary>
        ///     Bool indicating whether brightness is enabled regaurdless of Brightness's value.
        /// </summary>
        public bool IsBrightnessEnabled { get; set; }

        /// <summary>
        ///     Indicates whether this device is enabled or disabled.
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    ///     Class used to represent the snooze mechanic for alarms.
    /// </summary>
    public class Snooze
    {
        public byte Minutes { get; set; }
        public byte Interval { get; set; }
        public bool IsEnabled { get; set; }
    }
}
