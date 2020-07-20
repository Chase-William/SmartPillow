using Newtonsoft.Json.Bson;
using SmartPillow.Util;
using SmartPillowLib.Data.Local;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartPillowLib.ViewModels
{
    public class AlertsViewModel : NotifyClass
    {
        public static Alert SelectedAlert { get; set; }
        public static bool IsNewAlert;
        public event Action PushAdjustAlertPage;

        #region Const keys
        private const string BY_NAME_KEY = "ByName";
        private const string BRIG_ENABLED_KEY = "BriEnabled";
        private const string VIBR_ENABLED_KEY = "VibrEnabled";
        private const string RECENTLY_KEY = "RecentUpdated";
        #endregion

        #region Fields
        private string profile;
        private string filterKey = "";
        private ObservableCollection<Alert> alerts = new ObservableCollection<Alert>();
        private string brightness;
        private string keyword;
        private bool isRefreshing;
        private Alert selectedItem;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; 
                NotifyPropertyChanged(); }
        }

        public string Profile
        {
            get => UserInformation.User.Image;
            set => profile = value;
        }

        public ObservableCollection<Alert> Alerts
        {
            get => alerts;
            set
            {
                alerts = value;
                NotifyPropertyChanged();
            }
        }

        public Alert SelectedItem
        {
            get => selectedItem;
            set 
            {
                selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public string Keyword
        {
            get => keyword;
            set
            {
                keyword = value;
                NotifyPropertyChanged();
            }
        }

        public string Brightness
        {
            get => AutoBrightness.CheckNightTime();
            set
            {
                brightness = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Filter Commands
        public ICommand SearchCommand => new Command(() =>
        {
            var list = GetAlertsFromLocal();
            Alerts = new ObservableCollection<Alert>();

            Search(list);
        });

        public ICommand ByNameCommand => new Command(() =>
        {
            FilterByName();
            filterKey = BY_NAME_KEY;
        });

        public ICommand BrightnessEnabledCommand => new Command(() =>
        {
            FilterBrightnessEnabled();
            filterKey = BRIG_ENABLED_KEY;
        });

        public ICommand VibrationEnabledCommand => new Command(() =>
        {
            FilterVibrationEnbaled();
            filterKey = VIBR_ENABLED_KEY;
        });

        public ICommand LastUpdatedCommand => new Command(() =>
        {
            FilterRecentlyUpdated();
            filterKey = RECENTLY_KEY;
        });
        #endregion

        #region General Commands
        public ICommand RefreshCommand => new Command(async() =>
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;
            await Task.Run(() =>
            {
                switch(filterKey)
                {
                    case BY_NAME_KEY: FilterByName(); break;
                    case BRIG_ENABLED_KEY: FilterBrightnessEnabled(); break;
                    case VIBR_ENABLED_KEY: FilterVibrationEnbaled(); break;
                    case RECENTLY_KEY: FilterRecentlyUpdated(); break;
                    default: GetAlertsFromLocal(); break;
                }

                IsRefreshing = false;
            });
        });

        public ICommand SelectCommand => new Command(() =>
        {
            IsNewAlert = false;
            SelectedAlert = SelectedItem;
            PushAdjustAlertPage?.Invoke();
        });

        public ICommand AddAlertCommand => new Command(() =>
        {
            IsNewAlert = true;
            SelectedAlert = new Alert();
            PushAdjustAlertPage?.Invoke();
        });
        #endregion

        #region Methods
        public ObservableCollection<Alert> GetExampleAlerts()
        {
            var data = new AlertsList();
            return data.Alerts;
        }

        public void Search(ObservableCollection<Alert> list)
        {
            if (!string.IsNullOrEmpty(Keyword))
            {
                var newList = (list.Where(x => x.SpecificAlert.ToLower().Contains(Keyword.ToLower())));

                var filtered = newList.ToList();
                filtered.ForEach(x => Alerts.Add(x));
            }
            else
                Alerts = list;
        }

        public void FilterByName()
        {
            var list = GetAlertsFromLocal();
            Alerts = new ObservableCollection<Alert>();
            var newList = list.OrderBy(x => x.SpecificAlert).ToList();
            newList.ForEach(x => Alerts.Add(x));
        }

        public void FilterBrightnessEnabled()
        {
            var list = GetAlertsFromLocal();
            Alerts = new ObservableCollection<Alert>();
            list.ForEach(x => { if (x.BrightnessPercent != 0) { Alerts.Add(x); } });
        }

        public void FilterVibrationEnbaled()
        {
            var list = GetAlertsFromLocal();
            Alerts = new ObservableCollection<Alert>();
            list.ForEach(x => { if (x.VibrationPercent != 0) { Alerts.Add(x); } });
        }

        public void FilterRecentlyUpdated()
        {
            var list = GetAlertsFromLocal();
            Alerts = new ObservableCollection<Alert>();
            var newList = list.OrderByDescending(x => x.LastUpdated).ToList();
            newList.ForEach(x => Alerts.Add(x));
        }

        public ObservableCollection<Alert> GetAlertsFromLocal()
        {
            var data = LocalDataServiceContext.Provider.GetAlerts();
            var list = new ObservableCollection<Alert>();
            data.ForEach(x => list.Add(x));

            if (list.Count == 0)
            {
                var exampleData = GetExampleAlerts();
                foreach (var item in exampleData)
                    LocalDataServiceContext.Provider.InsertAlert(item);
            }

            Alerts = list;
            return list;
        }
        #endregion

        public AlertsViewModel()
        {
            GetAlertsFromLocal();
        }
    }
}
