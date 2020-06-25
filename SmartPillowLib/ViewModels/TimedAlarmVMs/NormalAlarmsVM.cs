using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartPillowLib.Data.Local;

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
        public event Action<Alarm> AlarmSelected;

        // Collection Source (readonly)
        public readonly ObservableCollection<Alarm> Alarms;

        /// <summary>
        ///     Query results
        /// </summary>
        public ObservableCollection<Alarm> QueryResults => !string.IsNullOrWhiteSpace(queryText)
                    ? new ObservableCollection<Alarm>(Alarms.Where(x => x.Name.Contains(queryText)).ToList())
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

        private Alarm selectedAlarm;
        /// <summary>
        ///     ListView item is selected prop.
        /// </summary>
        public Alarm SelectedItem
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
            Alarms = new ObservableCollection<Alarm>(LocalServiceContext.Provider.GetAlarms());
            Alarms.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                    NotifyPropertyChanged(nameof(QueryResults));
            };
        }
    }
}
