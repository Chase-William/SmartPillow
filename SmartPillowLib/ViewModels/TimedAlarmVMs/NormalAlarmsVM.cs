using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartPillowLib.Data.Local;
using System.Runtime.InteropServices;

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
                if (selectedAlarm != null)                 
                    AlarmSelected?.Invoke(selectedAlarm);
            }
        }

        public ICommand CreateNewAlarmCMD => new Command(() =>
        {
            CreateNewAlarm?.Invoke();
        });

        public NormalAlarmsVM()
        {
            //LocalDataServiceContext.Provider.DeleteAllAlarms();

            // Getting our local alarms
            var alarms = LocalDataServiceContext.Provider.GetAlarms().ToList();
            // Converting out local alarms into AlarmListViewWrappers for our own functionality.
            List<AlarmListViewWrapper> alarmWrappers = alarms.ConvertAll(x => new AlarmListViewWrapper(x.Id, x.Name, x.IsAlarmEnabled));
            
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
