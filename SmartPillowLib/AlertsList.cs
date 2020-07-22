using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartPillowLib
{
    public class AlertsList
    {
        public ObservableCollection<Alert> Alerts;
        public List<Alert> OptionalAlerts;
        public AlertsList()
        {
            var alertList = new ObservableCollection<Alert>();

            alertList.Add(new Alert() { Image = "fireIcon", Color = "#FFAD65", SpecificAlert = "Fire", BrightnessPercent = 100, VibrationPercent = 0, LastUpdated = new DateTime(2020, 6, 14, 11, 30, 59), Description = "A smoke detector is a device that senses smoke, typically as an indicator of fire." });
            alertList.Add(new Alert() { Image = "babyIcon", Color = "#FFC4F7", SpecificAlert = "Baby", BrightnessPercent = 27, VibrationPercent = 45, LastUpdated = new DateTime(2020, 5, 26, 4, 36, 43), Description = "Baby alarm is a radio system used to detect to sounds made by an infant. It alerts when the sound is detected as crying sound." });
            alertList.Add(new Alert() { Image = "CarIcon", Color = "#91FFFB", SpecificAlert = "Car", BrightnessPercent = 0, VibrationPercent = 18, LastUpdated = new DateTime(2020, 6, 13, 1, 3, 59, 41) });
            alertList.Add(new Alert() { Image = "weatherIcon", Color = "#91BDFF", SpecificAlert = "Nature", BrightnessPercent = 30, VibrationPercent = 13, LastUpdated = new DateTime(2020, 6, 13, 1, 3, 57) });
            alertList.Add(new Alert() { Image = "doorbellIcon", Color = "#B1FFD5", SpecificAlert = "Doorbell", BrightnessPercent = 0, VibrationPercent = 80, LastUpdated = new DateTime(2020, 6, 12, 6, 3, 59) });

            Alerts = alertList;


            var optionalList = new List<Alert>();

            optionalList.Add(new Alert() { Image = "fireIcon", SmallIcon = "fireAlert", Description = "A smoke detector is a device that senses smoke, typically as an indicator of fire." });
            optionalList.Add(new Alert() { Image = "babyIcon", SmallIcon = "babyAlert", Description = "Baby alarm is a radio system used to detect to sounds made by an infant. It alerts when the sound is detected as crying sound." });
            optionalList.Add(new Alert() { Image = "CarIcon", SmallIcon = "CarAlert", Description = "carr" });
            optionalList.Add(new Alert() { Image = "doorbellIcon", SmallIcon = "doorbellAlert", Description = "doorbell" });
            optionalList.Add(new Alert() { Image = "weatherIcon", SmallIcon = "weatherAlert", Description = "weather" });
            optionalList.Add(new Alert() { Image = "smokeAlert", SmallIcon = "smokeAlert", Description = "smoke" });
            optionalList.Add(new Alert() { Image = "breakAlert", SmallIcon = "breakAlert", Description = "break" });

            OptionalAlerts = optionalList;
        }
    }
}
