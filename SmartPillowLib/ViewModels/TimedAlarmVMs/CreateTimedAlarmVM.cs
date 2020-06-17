using SmartPillowLib.Models;
using System.Linq.Expressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class CreateTimedAlarmVM
    {
        public Alarm NewAlarm { get; set; } = new Alarm();

        public bool Test { get; set; }

        public ICommand SaveAlarmCMD => new Command(() =>
        {
            
        });

        public CreateTimedAlarmVM() { }
    }
}
