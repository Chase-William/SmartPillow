using SmartPillowLib.Models;
using SmartPillowLib.Data.Local;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class AdjustAlertViewModel : NotifyClass
    {
        public event Action OpenLoginPage;
        public event Action OpenProfilePage;
        public event Action PopPage;

        #region Fields
        private string toolbarIcon;
        private Alert alert;
        private Alert selectedIcon;
        private List<Alert> options;
        private string color;
        private bool isVisible = false;
        private string description;
        #endregion

        #region Properties
        public string ToolbarIcon
        {
            get => (AlertsViewModel.IsNewAlert) ? "neww.png" : "trash.png";
            set { toolbarIcon = value; }
        }

        public string ProfileImage
        {
            get => UserInformation.User.Image;
            set { UserInformation.User.Image = value; NotifyPropertyChanged(); }
        }

        public Alert Alert
        {
            get => AlertsViewModel.SelectedAlert;
            set
            {
                alert = value;
                NotifyPropertyChanged();
            }
        }

        public Alert SelectedIcon
        {
            get => selectedIcon;
            set 
            {
                selectedIcon = value;
                NotifyPropertyChanged();
            }
        }

        public List<Alert> Options
        {
            get { return options; }
            set { options = value; }
        }

        public string SpecificAlert
        {
            get => Alert.SpecificAlert;
            set
            {
                Alert.SpecificAlert = value;
                UpdateDatabase();
                NotifyPropertyChanged();
            }
        }

        public string Color
        {
            get => color;
            set
            {
                color = value;
                UpdateDatabase();
                NotifyPropertyChanged();
            }
        }

        public string Image
        {
            get => Alert.Image;
            set
            {
                Alert.Image = value;
                UpdateDatabase();
                NotifyPropertyChanged();
            }
        }

        public double BrightnessPercent
        {
            get => (double)Alert.BrightnessPercent;
            set
            {
                Alert.BrightnessPercent = (int)value;
                UpdateDatabase();
                NotifyPropertyChanged();
            }
        }

        public double VibrationPercent
        {
            get => (double)Alert.VibrationPercent;
            set
            {
                Alert.VibrationPercent = (int)value;
                UpdateDatabase();
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get { return Alert.Description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        public bool IsVisible
        {
            get => isVisible;
            set 
            { 
                isVisible = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new Command(() =>
        {
            if (AlertsViewModel.IsNewAlert)
            {
                AlertsViewModel.SelectedAlert.LastUpdated = DateTime.Now;
                LocalDataServiceContext.Provider.InsertAlert(AlertsViewModel.SelectedAlert);
                PopPage?.Invoke();
            }
            else
            {
                LocalDataServiceContext.Provider.DeleteAlert(AlertsViewModel.SelectedAlert.Id);
                PopPage?.Invoke();
            }
        });

        public ICommand UserCommand => new Command(() =>
        {
            if (UserInformation.IsUserLogged == false)
                OpenLoginPage?.Invoke();

            else
                OpenProfilePage?.Invoke();
        });

        public ICommand ImageCommand => new Command(() =>
        {
            IsVisible = true;
        });

        public ICommand CloseCommand => new Command(() =>
        {
            IsVisible = false;
        });

        public ICommand SelectIconCommand => new Command(() =>
        {
            Alert.Image = SelectedIcon.Image;
            Image = SelectedIcon.Image;
            Alert.Description = SelectedIcon.Description;
            Description = SelectedIcon.Description;
        });
        #endregion

        #region Methods
        public void UpdateDatabase()
        {
            if (!AlertsViewModel.IsNewAlert)
            {
                Alert.LastUpdated = DateTime.Now;
                LocalDataServiceContext.Provider.UpdateAlert(Alert);
            }
        }

        public void OnAppearing()
        {
            ProfileImage = UserInformation.User.Image;
        }
        #endregion

        public AdjustAlertViewModel()
        {
            var alertList = new AlertsList();
            Options = alertList.OptionalAlerts;
        }
    }
}
