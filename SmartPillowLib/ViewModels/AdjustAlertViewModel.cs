using SmartPillowLib.Models;
using SmartPillowLib.Data.Local;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class AdjustAlertViewModel : NotifyClass
    {
        #region Fields
        private string profile;
        private Alert alert;
        private Alert selectedIcon;
        private List<Alert> options;
        private string specificAlert;
        private string color;
        private string image;
        private double brightnessPercent;
        private double vibrationPercent;
        private bool isVisible = false;
        private string description;
        #endregion

        #region Properties

        public string Profile
        {
            get => UserInformation.User.Image;
            set { profile = value; }
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

        public AdjustAlertViewModel()
        {
            var alertList = new AlertsList();
            Options = alertList.OptionalAlerts;
        }

        public void UpdateDatabase()
        {
            if (!AlertsViewModel.IsNewAlert)
            {
                Alert.LastUpdated = DateTime.Now;
                LocalDataServiceContext.Provider.UpdateAlert(Alert);
            }
        }
    }
}
