using SmartPillowLib.Models;
using SmartPillowLib.Data.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.ViewModels
{
    public class AdjustAlertViewModel : NotifyClass
    {
        #region Fields
        private string profile;
        private Alert alert;
        private string specificAlert;
        private string color;
        private string image;
        private double brightnessPercent;
        private double vibrationPercent;
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
        #endregion

        public AdjustAlertViewModel()
        {

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
