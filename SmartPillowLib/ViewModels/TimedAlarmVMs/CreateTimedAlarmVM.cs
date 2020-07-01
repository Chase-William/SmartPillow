using SmartPillow.Util;
using SmartPillowLib.Data.Local;
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
        /// <summary>
        ///     Event that signals that the user wants to adjust the settings of a device.
        ///     The device is determined by a string key.
        /// </summary>
        public event Action<string> AdjustSettings;
        /// <summary>
        ///     Event that signals that the user has finished adjusting the settings.
        /// </summary>
        public event Action FinishedAdjustingSettings;
        /// <summary>
        ///     Event that signals a new alarm is ready to be saved.
        ///     Note:
        ///         We dont need to pass in an old and new alarm because the ID of the new alarm and old are the same.       
        /// </summary>
        public event Action<Alarm> SaveAlarm;

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
        ///     Default constructor for making a new alarm.
        /// </summary>
        public CreateTimedAlarmVM(ObservableCollection<AlarmListViewWrapper> alarms) 
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
                },
                IsAlarmEnabled = true,
                TimeOffset = default
            };

            // Create new alarm
            SaveAlarmCMD = new Command(() =>
            {
                // Saves alarm to the local database
                // Assigns the Id of the Alarm the id that was created by the database context

                SaveAlarm?.Invoke(NewAlarm);

                //LocalServiceContext.Provider.InsertAlarm(NewAlarm);
                // Updating collection with the alarm
                alarms.Add(new AlarmListViewWrapper(NewAlarm));
                
                FinishedAdjustingSettings?.Invoke();
            });
        }

        /// <summary>
        ///     Parameterized constructor for editing an existing alarm.
        /// </summary>
        /// <param name="alarm"></param>
        public CreateTimedAlarmVM(AlarmListViewWrapper alarmWrapper)
        {
            var alarm = LocalDataServiceContext.Provider.GetAlarm(alarmWrapper.Id);
            NewAlarm = ObjectCloner.CloneJson(alarm);            

            // Saves changes to alarm.
            SaveAlarmCMD = new Command(() =>
            {
                // Saves changes to alarm
                alarm.Name = NewAlarm.Name;
                alarm.PillowProps = NewAlarm.PillowProps;
                alarm.PhoneProps = NewAlarm.PhoneProps;
                alarm.SnoozeProps = NewAlarm.SnoozeProps;

                SaveAlarm?.Invoke(NewAlarm);
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
