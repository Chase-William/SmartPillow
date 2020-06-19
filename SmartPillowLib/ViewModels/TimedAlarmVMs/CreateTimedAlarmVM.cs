using SmartPillowLib.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{    
    public class CreateTimedAlarmVM
    {        
        public event Action AdjustPillowSettings;

        /// <summary>
        ///     New alarm instance.
        /// </summary>
        public Alarm NewAlarm { get; set; } = new Alarm
        {
            PillowProps = new DeviceProps
            {
                IsBrightnessEnabled = true,
                IsVibrationEnabled = true,
                IsEnabled = true,
                Brightness = 50,
                Vibration = 50
            }
        };

        /// <summary>
        ///     Saves an alarm to the device after validiation.
        /// </summary>
        public ICommand SaveAlarmCMD => new Command(() =>
        {
            
        });

        public ICommand AdjustPillowSettingsCMD => new Command(() =>
        {
            AdjustPillowSettings?.Invoke();
        });

        public CreateTimedAlarmVM() { }
    }
}
