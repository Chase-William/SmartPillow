using SmartPillow.Util;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartPillowLib.ViewModels
{
    public class AlertsViewModel : NotifyClass
    {
        private string brightness;
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
            var alertList = new ObservableCollection<Alert>();

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 100, VibrationPercent = 0 });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 27, VibrationPercent = 45 });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 0, VibrationPercent = 18 });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 30, VibrationPercent = 13 });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 0, VibrationPercent = 80 });

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 23, VibrationPercent = 0 });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 0, VibrationPercent = 26 });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 78, VibrationPercent = 0 });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 15, VibrationPercent = 32 });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 60, VibrationPercent = 0 });

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 23, VibrationPercent = 0 });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 0, VibrationPercent = 26 });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 78, VibrationPercent = 0 });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 15, VibrationPercent = 32 });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 60, VibrationPercent = 0 });

            Alerts = alertList;
        }
    }
}
