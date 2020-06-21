using SmartPillowLib.Models;
using System.Collections.Generic;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class NormalAlarmsPageVM : NotifyClass
    {
        public List<Alarm> Alarms => TESTNormalAlarms.Alarms;

        public NormalAlarmsPageVM() => NotifyPropertyChanged(nameof(Alarms));
    }
}
