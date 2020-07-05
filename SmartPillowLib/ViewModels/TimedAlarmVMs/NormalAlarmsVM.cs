using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartPillowLib.Data.Local;
using System.Runtime.InteropServices;
using Xamarin.Forms.Internals;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class NormalAlarmsVM : NotifyClass
    {
        /// <summary>
        ///     Signals that the user wants to create a new alarm and this should be handled.
        /// </summary>
        public event Action CreateNewAlarm;
        /// <summary>
        ///     Signals that an alarm has been selected and it should be handled.
        /// </summary>
        public event Action<AlarmListViewWrapper> AlarmSelected;

        // Collection Source (readonly)
        public readonly ObservableCollection<AlarmListViewWrapper> Alarms;

        /// <summary>
        ///     Query results
        /// </summary>
        public ObservableCollection<AlarmListViewWrapper> QueryResults => !string.IsNullOrWhiteSpace(queryText)
                    ? new ObservableCollection<AlarmListViewWrapper>(Alarms.Where(x => x.Name.Contains(queryText)).ToList())
                    : Alarms;

        private string queryText;
        /// <summary>
        ///     Searchbar's query text
        /// </summary>
        public string QueryText
        {
            get => queryText;
            set
            {
                if (QueryText == value) return;

                queryText = value;
                // Updating the collection
                NotifyPropertyChanged(nameof(QueryResults));
            }
        }

        private AlarmListViewWrapper selectedAlarm;
        /// <summary>
        ///     ListView item is selected prop.
        /// </summary>
        public AlarmListViewWrapper SelectedItem
        {
            get => selectedAlarm;
            set
            {
                selectedAlarm = value;
                if (selectedAlarm == null) return;
                if (IsDeleteModeActive)
                    SelectedItem.ToBeDeleted = !SelectedItem.ToBeDeleted;
                else
                    AlarmSelected?.Invoke(selectedAlarm);
            }
        }

        private bool isDeleteModeActive = false;
        /// <summary>
        ///     Informs the listview to display the deletion buttons
        /// </summary>
        public bool IsDeleteModeActive
        {
            get => isDeleteModeActive;
            set
            {
                if (IsDeleteModeActive == value) return;
                isDeleteModeActive = value;
                NotifyPropertyChanged();

                // When delete mode is off, reset the delete status on all alarms.
                if (isDeleteModeActive == false)
                    Alarms.ForEach(alarm => alarm.ToBeDeleted = false);             
            }
        }

        public ICommand CreateNewAlarmCMD => new Command(() =>
        {
            CreateNewAlarm?.Invoke();
        });

        public ICommand InvokeDeleteModeCMD => new Command(() =>
        {
            IsDeleteModeActive = !IsDeleteModeActive;
        });

        public ICommand DeleteAlarmsCMD => new Command(() =>
        {

        });

        
        public ICommand ToggleAllAlarmsCMD => new Command((SelectAllIsChecked) =>
        {
            // Updates all alarms toBeDeleted Status because the master button has been pressed. 
            Alarms.ForEach(alarm => alarm.ToBeDeleted = (bool)SelectAllIsChecked);
        });

        public NormalAlarmsVM()
        {
            //LocalDataServiceContext.Provider.DeleteAllAlarms();

            NotifyPropertyChanged(nameof(IsDeleteModeActive));

            // Getting our local alarms
            var alarms = LocalDataServiceContext.Provider.GetAlarms().ToList();
            // Converting out local alarms into AlarmListViewWrappers for our own functionality.
            List<AlarmListViewWrapper> alarmWrappers = alarms.ConvertAll(x => new AlarmListViewWrapper(x.Id, x.Name, x.IsAlarmEnabled, x.TimeOffset));
            
            Alarms = new ObservableCollection<AlarmListViewWrapper>(alarmWrappers);
            //Alarms = new ObservableCollection<Alarm>(alarms);
            Alarms.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                    NotifyPropertyChanged(nameof(QueryResults));
            };
        }
    }
}
