using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    /// <summary>
    ///     This class provides common functionalites for both pillow and phone device types.
    /// </summary>
    public class DeviceSettingsBase : NotifyClass
    {        
        public DeviceProperties DeviceProps { get; set; }

        protected DeviceSettingsBase(DeviceProperties _deviceProps)
        {
            DeviceProps = _deviceProps;

            // Making sure all properties and their bound UI components are aware of any starting values.
            NotifyPropertiesChanged(nameof(DeviceProps.IsEnabled),
                                    nameof(DeviceProps.IsVibrationEnabled),
                                    nameof(DeviceProps.IsBrightnessEnabled),
                                    nameof(DeviceProps.Brightness),
                                    nameof(DeviceProps.Vibration));
        }
    }
}
