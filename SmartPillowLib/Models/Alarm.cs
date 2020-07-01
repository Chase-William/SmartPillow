using SmartPillowLib.ViewModels;
using SmartPillowLib.ViewModels.TimedAlarmVMs;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Xamarin.Forms;

namespace SmartPillowLib.Models
{
    /// <summary>
    ///     Base class for all alarm types.
    /// </summary>
    public partial class Alarm : NotifyClass
    {
        /// <summary>
        ///     Signals that the state of this alarm has changed.
        ///     This means it needs to be enabled or disabled on the native platform.
        /// </summary>
        //public static event Action<Alarm> AlarmStateChanged;

        /// <summary>
        ///     The unique identifier for all alarms.
        /// </summary>
        public int Id { get; set; }

        private string name;
        /// <summary>
        ///     Name of the alarm.
        ///     NotifyPropertyChanged is implemented here to help notify the listview's of name updates.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (Name == value) return;
                name = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        ///     Path and name of image file.
        /// </summary>
        public string ImgPath { get; set; }

        private bool isFadeEnabled;
        /// <summary>
        ///     Bool indicating whether the alarm all together should fade in when going off.
        /// </summary>
        public bool IsFadeEnabled
        {
            get => isFadeEnabled;
            set
            {
                isFadeEnabled = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     When the timer should be executed.
        /// </summary>
        public DateTimeOffset Time { get; set; }

        // Protected for access when overriding the corresponding property.
        protected bool isAlarmEnabled;
        /// <summary>
        ///     Bool indicates whether this alarm is enabled or disabled.
        ///     Overridable because for example in a listview if you want to toggle this alarm on or off.
        ///     You will need to use the dependency service to call the platform specific code to setup an alarm or cancel one.
        /// </summary>
        public virtual bool IsAlarmEnabled
        {
            get => isAlarmEnabled;
            set
            {
                if (IsAlarmEnabled == value) return;
                
                isAlarmEnabled = value;

                ///AlarmStateChanged?.Invoke(this);
                NotifyPropertyChanged();             
            }
        }
        /// <summary>
        ///     String which the user can put any notes about the alarm in. 
        /// </summary>        
        public string Note { get; set; }

        /// <summary>
        ///     Contains properties for customizing the user's desire snooze duration and interval.
        /// </summary>
        public SnoozeProperties SnoozeProps { get; set; }

        #region Device Properties
        /// <summary>
        ///     Contains the properties for this alarm related to the pillow.
        /// </summary>
        public DeviceProperties PillowProps { get; set; }
        /// <summary>
        ///     Contains the properties for this alarm relate to the phone.
        /// </summary>
        public DeviceProperties PhoneProps { get; set; }
        #endregion

    }

    /// <summary>
    ///     Class that defines the common properties for both the Smart Pillow and the Phone.
    /// </summary>
    public class DeviceProperties : NotifyClass
    {     
        private bool isVibrationEnabled;
        /// <summary>
        ///     Bool indicating whether vibration is enabled reguardless of Vibration's value.
        /// </summary>
        public bool IsVibrationEnabled
        {
            get => isVibrationEnabled;
            set
            {
                isVibrationEnabled = value;
                NotifyPropertiesChanged(nameof(IsVibrationEnabled) , nameof(GetDeviceSettingsStr));
            }
        }        
        private bool isBrightnessEnabled;
        /// <summary>
        ///     Bool indicating whether brightness is enabled regaurdless of Brightness's value.
        /// </summary>
        public bool IsBrightnessEnabled
        {
            get => isBrightnessEnabled;
            set
            {
                isBrightnessEnabled = value;
                NotifyPropertiesChanged(nameof(IsBrightnessEnabled), nameof(GetDeviceSettingsStr));
            }
        }
        private bool isEnabled;
        /// <summary>
        ///     Indicates whether this device is enabled or disabled.
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                NotifyPropertiesChanged(nameof(IsEnabled), nameof(GetDeviceSettingsStr));
            }
        }

        /// <summary>
        ///     Value for vibration 0 to 100.
        /// </summary>
        public byte Vibration { get; set; }
        /// <summary>
        ///     Value for vibration 0 to 100.
        /// </summary>
        public byte Brightness { get; set; }

        /// <summary>
        ///     Formats the device settings into a string.
        /// </summary>
        public string GetDeviceSettingsStr
        {
            get
            {
                if (!IsEnabled) return "Off";
                if (IsBrightnessEnabled && IsVibrationEnabled) return "Vibration & Lights";
                if (IsBrightnessEnabled) return "Lights Only";
                if (IsVibrationEnabled) return "Vibration Only";
                return "Enabled but Inactive";
            }            
        }
    }

    /// <summary>
    ///     Class used to represent the snooze mechanic for alarms.
    /// </summary>
    public class SnoozeProperties : NotifyClass
    {
        private byte repeat;
        public byte Repeat
        {
            get => repeat;
            set
            {
                repeat = value;
                NotifyPropertiesChanged(nameof(Repeat), nameof(GetSnoozeSettingsStr));
            }
        }
        private byte interval;
        public byte Interval
        {
            get => interval;
            set
            {
                interval = value;
                NotifyPropertiesChanged(nameof(Interval), nameof(GetSnoozeSettingsStr));
            }
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                NotifyPropertiesChanged(nameof(IsEnabled), nameof(GetSnoozeSettingsStr));
            }
        }

        /// <summary>
        ///     Returns a formatted representation of the snooze settings.
        /// </summary>
        public string GetSnoozeSettingsStr => string.Format(IsEnabled ? (Interval + " minutes, Repeat " + (Repeat == byte.MaxValue ? "Continuously" : Repeat.ToString() + " times")) : "Off");
    }
}
