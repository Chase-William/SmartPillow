using SmartPillowLib.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.Models
{
    public class Alert : NotifyClass
    {
        #region Fields
        //private int id;
        private string image;
        private string smallIcon;
        private string color;
        private string specificAlert;
        private int vibrationPercent;
        private int brightnessPercent;
        private DateTime lastUpdated;
        private string description;
        private string brightnessPercentString;
        private string vibrationPercentString;
        private string lastUpdatedString;
        private bool hasBothVibandBri;
        private bool hasOnlyOneSetting;
        private int font;
        private string specificSetting;
        private int percent;
        #endregion

        #region Basic Properties
        public int Id { get; set; }
        public string Image
        {
            get => image;
            set { image = value; NotifyPropertyChanged(); }
        }

        public string SmallIcon
        {
            get => smallIcon;
            set { smallIcon = value; NotifyPropertyChanged(); }
        }

        public string Color 
        {
            get => color;
            set { color = value; NotifyPropertyChanged(); }
        }

        public string SpecificAlert
        {
            get => specificAlert;
            set { specificAlert = value; NotifyPropertyChanged(); }
        }

        public int VibrationPercent
        {
            get => vibrationPercent;
            set { vibrationPercent = value; NotifyPropertyChanged(); }
        }

        public int BrightnessPercent
        {
            get => brightnessPercent;
            set { brightnessPercent = value; NotifyPropertyChanged(); }
        }

        public DateTime LastUpdated
        {
            get => lastUpdated;
            set { lastUpdated = value; NotifyPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; NotifyPropertyChanged(); }
        }

        public string LastUpdatedString
        {
            get => "Last Updated " + LastUpdated.ToString("d");
            set 
            { 
                lastUpdatedString = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        ///     determine IsVisible is either true or false based on 
        ///     a specific alert has either both or only one setting (vibration/brightness)
        /// </summary>
        #region

        // the bool property is true if a specific alert has both vibration and brightness are settled by an user
        public bool HasBothVibandBri 
        {
            get => hasBothVibandBri = (VibrationPercent == 0 || BrightnessPercent == 0) ? false : true;
            set { hasBothVibandBri = value; NotifyPropertyChanged(); }
        }

        public bool HasOnlyOneSetting
        {
            get => hasOnlyOneSetting = !HasBothVibandBri;
            set { hasOnlyOneSetting = value; NotifyPropertyChanged(); }
        }

        public int Font
        {
            get => font = (Percent == 100) ? 32 : 45;
            set { font = value; NotifyPropertyChanged(); }
        }

        // either brightness or vibration if the specific alert has only one setting (doesn't have both vibration and brightness)
        public string SpecificSetting
        {
            get => specificSetting = (VibrationPercent == 0) ? "Brightness" : "Vibration";
            set { specificSetting = value;  NotifyPropertyChanged(); }
        }

        // if HasBothVibandBri is false, applys either brightnessPercent or VibrationPercent that is settled to Percent
        public int Percent
        {
            get => percent = (VibrationPercent == 0) ? BrightnessPercent : VibrationPercent;
            set { percent = value; NotifyPropertyChanged(); }
        }
        #endregion

        /// <summary>
        ///     add "%" to the string to display it if a specific alert has both settings (vibration and brightness)  
        /// </summary>
        #region

        public string BrightnessPercentString
        {
            get => brightnessPercentString =
                (HasBothVibandBri) ? BrightnessPercent.ToString() + "%" : BrightnessPercent.ToString();
            set { brightnessPercentString = value; NotifyPropertyChanged(); }
        }

        public string VibrationPercentString
        {
            get => vibrationPercentString =
                (HasBothVibandBri) ? VibrationPercent.ToString() + "%" : VibrationPercent.ToString();
            set { vibrationPercentString = value; NotifyPropertyChanged(); }
        }
        #endregion

        // -------------------------- //

        // default constructor
        public Alert()
        {
            Color = "#FFFFFF";
            BrightnessPercent = 0;
            VibrationPercent = 0;
            Image = "babyIcon";
        }
    }
}
