using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    /// <summary>
    ///     This class provides common functionalites for both pillow and phone device types.
    /// </summary>
    public class DeviceSettingsBase : NotifyClass
    {        
        private DeviceProps deviceProps;

        #region Intermediate Binding Properties
        public byte Vibration
        {
            get => deviceProps.Vibration;
            set
            {
                if (Vibration == value) return;

                deviceProps.Vibration = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsVibrationEnabled
        {
            get => deviceProps.IsVibrationEnabled;
            set
            {
                if (IsVibrationEnabled == value) return;

                deviceProps.IsVibrationEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public byte Brightness
        {
            get => deviceProps.Brightness;
            set
            {
                if (Brightness == value) return;

                deviceProps.Brightness = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsBrightnessEnabled
        {
            get => deviceProps.IsBrightnessEnabled;
            set
            {
                if (IsBrightnessEnabled == value) return;

                deviceProps.IsBrightnessEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsEnabled
        {
            get => deviceProps.IsEnabled;
            set
            {
                if (IsEnabled == value) return;

                deviceProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        protected DeviceSettingsBase(DeviceProps _deviceProps)
        {
            deviceProps = _deviceProps;

            // Making sure all properties and their bound UI components are aware of any starting values.
            NotifyPropertiesChanged(nameof(IsEnabled),
                                    nameof(IsBrightnessEnabled),
                                    nameof(IsVibrationEnabled),
                                    nameof(Brightness),
                                    nameof(Vibration));
        }
    }
}
