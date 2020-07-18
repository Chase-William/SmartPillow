using SmartPillow.Util;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
            var data = new AlertsList();
            var list = data.Alerts;

            Alerts.Clear();

            if (!string.IsNullOrEmpty(Keyword))
            {
                var newList = (list.Where(x => x.SpecificAlert.ToLower().Contains(Keyword.ToLower())));

                var filtered = newList.ToList();
                foreach (var item in filtered)
                    Alerts.Add(item);
            }
            else
                Alerts = list;

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
            var data = new AlertsList();
            Alerts = data.Alerts;
        }
    }
}
