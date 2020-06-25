using SmartPillow.Util;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public CreateTimedAlarmVM(ObservableCollection<Alarm> alarms) 
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

                // Just a little way to make sure the Ids are different for now
                // Once a local db is implemented will use the key created there
                NewAlarm.Id = alarms.Last().Id++;
                alarms.Add(NewAlarm);

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
                alarm.Name = NewAlarm.Name;
                alarm.PillowProps = NewAlarm.PillowProps;
                alarm.PhoneProps = NewAlarm.PhoneProps;
                alarm.SnoozeProps = NewAlarm.SnoozeProps;
                // alarm = NewAlarm;
                //alarm.PillowProps.Brightness = NewAlarm.PillowProps.Brightness;
                
                // ------------------------------------- need to update local storage ----------------------------------

                FinishedAdjustingSettings?.Invoke();
            });
        }

        /// <summary>
        ///     Called when this page is appearing to make sure the current values are in sync with the UI.
        /// </summary>
        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(NewAlarm.PillowProps.IsEnabled), 
                                    nameof(NewAlarm.PhoneProps.IsEnabled), 
                                    nameof(NewAlarm.SnoozeProps.IsEnabled), 
                                    nameof(NewAlarm.IsFadeEnabled));
        }
    }
}
