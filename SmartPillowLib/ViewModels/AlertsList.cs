using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartPillowLib.ViewModels
{
    public class AlertsList
    {
        public ObservableCollection<Alert> Alerts;
        public AlertsList()
        {
            var alertList = new ObservableCollection<Alert>();

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 100, VibrationPercent = 0, LastUpdated = new DateTime(2020, 6, 14, 11, 30, 59) });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 27, VibrationPercent = 45, LastUpdated = new DateTime(2020, 5, 26, 4, 36, 43) });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 0, VibrationPercent = 18, LastUpdated = new DateTime(2020, 6, 13, 1, 3, 59, 41) });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 30, VibrationPercent = 13, LastUpdated = new DateTime(2020, 6, 13, 1, 3, 57) });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 0, VibrationPercent = 80, LastUpdated = new DateTime(2020, 6, 12, 6, 3, 59) });

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 23, VibrationPercent = 0, LastUpdated = new DateTime(2020, 4, 24, 5, 5, 20) });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 0, VibrationPercent = 26, LastUpdated = new DateTime(2020, 4, 30, 2, 3, 59) });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 78, VibrationPercent = 0, LastUpdated = new DateTime(2020, 5, 13, 1, 2, 51) });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 15, VibrationPercent = 32, LastUpdated = new DateTime(2020, 5, 12, 4, 3, 59) });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 60, VibrationPercent = 0, LastUpdated = new DateTime(2020, 7, 8, 5, 3, 59) });

            alertList.Add(new Alert() { Image = "fireIcon", SpecificAlert = "Fire", BrightnessPercent = 23, VibrationPercent = 0, LastUpdated = new DateTime(2020, 7, 18, 4, 3, 59) });
            alertList.Add(new Alert() { Image = "babyIcon", SpecificAlert = "Baby", BrightnessPercent = 0, VibrationPercent = 26, LastUpdated = new DateTime(2020, 7, 12, 6, 3, 59) });
            alertList.Add(new Alert() { Image = "CarIcon", SpecificAlert = "Car", BrightnessPercent = 78, VibrationPercent = 0, LastUpdated = new DateTime(2020, 7, 14, 5, 3, 59) });
            alertList.Add(new Alert() { Image = "weatherIcon", SpecificAlert = "Nature", BrightnessPercent = 15, VibrationPercent = 32, LastUpdated = new DateTime(2020, 7, 1, 2, 9, 59) });
            alertList.Add(new Alert() { Image = "doorbellIcon", SpecificAlert = "Doorbell", BrightnessPercent = 60, VibrationPercent = 0, LastUpdated = new DateTime(2020, 6, 4, 4, 3, 20) });

            Alerts = alertList;
        }
    }
}
