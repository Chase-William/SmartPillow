using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class NormalAlarmsVM : NotifyClass
    {
        public event Action CreateNewAlarm;
        public event Action<Alarm> AlarmSelected;

        // Collection Source (readonly)
        public ObservableCollection<Alarm> Alarms { get; set; }

        /// <summary>
        ///     Query results
        /// </summary>
        public ObservableCollection<Alarm> QueryResults
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(queryText))
                {
                    return new ObservableCollection<Alarm>(Alarms.Where(x => x.Name.Contains(queryText)).ToList());
                }
                else
                {
                    return Alarms;
                }
            }
        }

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

        private Alarm selectedRobot;
        /// <summary>
        ///     ListView item is selected prop.
        /// </summary>
        public Alarm SelectedItem
        {
            get => selectedRobot;
            set
            {
                selectedRobot = value;
                if (selectedRobot != null)
                    foreach (var a in Alarms)
                    {
                        if (a.Id == value.Id)
                        {                            
                            AlarmSelected?.Invoke(a);
                            return;
                        }
                    }
                    //AlarmSelected?.Invoke(Alarms.Single(x => x.Id == value.Id));
            }
        }

        public ICommand CreateNewAlarmCMD => new Command(() =>
        {
            CreateNewAlarm?.Invoke();
        });

        public NormalAlarmsVM()
        {
            Alarms = new ObservableCollection<Alarm>(TESTNormalAlarms.Alarms);
            Alarms.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                    NotifyPropertyChanged();
            };

            NotifyPropertyChanged(nameof(Alarms));
        }
    }
}
