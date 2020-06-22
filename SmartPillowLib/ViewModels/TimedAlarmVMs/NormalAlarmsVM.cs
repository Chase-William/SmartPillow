using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class NormalAlarmsVM : NotifyClass
    {
        public event Action CreateNewAlarm;
        public event Action<Alarm> AlarmSelected;

        public List<Alarm> Alarms => TESTNormalAlarms.Alarms;

        private Alarm selectedRobot;
        public Alarm SelectedItem
        {
            get => selectedRobot;
            set
            {
                selectedRobot = value;
                if (selectedRobot != null)
                    AlarmSelected?.Invoke(value);
            }
        }

        public ICommand CreateNewAlarmCMD => new Command(() =>
        {
            CreateNewAlarm?.Invoke();
        });

        public NormalAlarmsVM() => NotifyPropertyChanged(nameof(Alarms));
    }
}
