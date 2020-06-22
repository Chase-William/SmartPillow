using SmartPillow.Util;
using SmartPillowLib.Models;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{    
    public class CreateTimedAlarmVM : NotifyClass
    {        
        public event Action<string> AdjustSettings;
        public event Action FinishedAdjustingSettings;

        /// <summary>
        ///     New alarm instance.
        /// </summary>
        public Alarm NewAlarm { get; set; }

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
        public ICommand SaveAlarmCMD { get; set; }

        public ICommand CancelCMD => new Command(() =>
        {
            FinishedAdjustingSettings?.Invoke();
        });

        public ICommand AdjustSettingsCMD => new Command((object discriminator) =>
        {
            AdjustSettings?.Invoke((string)discriminator);
        });

        /// <summary>
        ///     Default contructor for making a new alarm.
        /// </summary>
        public CreateTimedAlarmVM() 
        {
            NewAlarm = new Alarm
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

            // Create new alarm
            SaveAlarmCMD = new Command(() =>
            {
                // Save to device

                FinishedAdjustingSettings?.Invoke();
            });
        }

        /// <summary>
        ///     Parameterized constructor for editing an existing alarm.
        /// </summary>
        /// <param name="alarm"></param>
        public CreateTimedAlarmVM(Alarm alarm)
        {            
            // Creates a new copy of our object in memory for editing purposes
            NewAlarm = ObjectCloner.CloneJson(alarm);            

            // Saves changes to alarm.
            SaveAlarmCMD = new Command(() =>
            {
                // Saves changes to alarm
                alarm = NewAlarm;
                
                // ------------------------------------- need to update local storage ----------------------------------

                FinishedAdjustingSettings?.Invoke();
            });
        }

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
