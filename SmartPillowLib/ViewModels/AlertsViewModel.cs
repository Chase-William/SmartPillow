using SmartPillow.Util;
using SmartPillowLib.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartPillowLib.ViewModels
{
    public class AlertsViewModel : NotifyClass
    {
        private string brightness;

        private string keyword;
        public string Keyword
        {
            get => keyword;
            set
            {
                keyword = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SearchCommand => new Command(() =>
        {
            var list = GetActualAlerts();

            Alerts.Clear();

            if (!string.IsNullOrEmpty(Keyword))
            {
                var newList = (list.Where(x => x.SpecificAlert.ToLower().Contains(Keyword.ToLower())));

                var filtered = newList.ToList();
                filtered.ForEach(x => Alerts.Add(x));
            }
            else
                Alerts = list;
        });

        public ICommand ByNameCommand => new Command(() =>
        {
            var list = GetActualAlerts();

            Alerts.Clear();

            var newList = list.OrderBy(x => x.SpecificAlert).ToList();
            newList.ForEach(x => Alerts.Add(x));
        });

        public ICommand BrightnessEnabledCommand => new Command(() =>
        {
            var list = GetActualAlerts();

            Alerts.Clear();

            list.ForEach(x => { if (x.BrightnessPercent != 0) { Alerts.Add(x); } });
        });

        public ICommand VibrationEnabledCommand => new Command(() =>
        {
            var list = GetActualAlerts();

            Alerts.Clear();

            list.ForEach(x => { if (x.VibrationPercent != 0) { Alerts.Add(x); } });
        });

        public ICommand LastUpdatedCommand => new Command(() =>
        {
            var list = GetActualAlerts();

            Alerts.Clear();

            var newList = list.OrderByDescending(x => x.LastUpdated).ToList();
            newList.ForEach(x => Alerts.Add(x));
        });

        public string Brightness
        {
            get => AutoBrightness.CheckNightTime();
            set
            {
                brightness = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Alert> alerts;

        public ObservableCollection<Alert> Alerts
        {
            get => alerts;
            set 
            {
                alerts = value;
                NotifyPropertyChanged();
            }
        }

        public AlertsViewModel()
        {
            Alerts = GetActualAlerts();
        }

        public ObservableCollection<Alert> GetActualAlerts()
        {
            var data = new AlertsList();
            return data.Alerts;
        }
    }
}
