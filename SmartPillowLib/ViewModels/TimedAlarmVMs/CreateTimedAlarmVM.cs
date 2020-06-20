using SmartPillowLib.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{    
    public class CreateTimedAlarmVM : NotifyClass
    {        
        public event Action<string> AdjustSettings;        

        /// <summary>
        ///     New alarm instance.
        /// </summary>
        public Alarm NewAlarm { get; set; } = new Alarm
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
            }
        };

        #region Intermediate Binding Props
        public bool IsPillowEnabled
        {
            get => NewAlarm.PillowProps.IsEnabled;
            set
            {
                if (IsPillowEnabled == value) return;

                NewAlarm.PillowProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsPhoneEnabled
        {
            get => NewAlarm.PhoneProps.IsEnabled;
            set
            {
                if (IsPhoneEnabled == value) return;

                NewAlarm.PhoneProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsSnoozeEnabled
        {
            get => NewAlarm.SnoozeProps.IsEnabled;
            set
            {
                if (IsSnoozeEnabled == value) return;

                NewAlarm.SnoozeProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsFadeEnabled
        {
            get => NewAlarm.IsFadeEnabled;
            set
            {
                if (IsFadeEnabled == value) return;

                NewAlarm.IsFadeEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        ///     Saves an alarm to the device after validiation.
        /// </summary>
        public ICommand SaveAlarmCMD => new Command(() =>
        {
            
        });

        public ICommand AdjustSettingsCMD => new Command((object discriminator) =>
        {
            AdjustSettings?.Invoke((string)discriminator);
        });

        /// <summary>
        ///     Called when this page is appearing to make sure the current values are in sync with the UI.
        /// </summary>
        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(IsPillowEnabled), 
                                    nameof(IsPhoneEnabled), 
                                    nameof(IsSnoozeEnabled), 
                                    nameof(IsFadeEnabled));
        }
    }
}
