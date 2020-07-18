using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmartPillowLib.Models
{
    public class Alert
    {
        public string Image { get; set; }
        public string SpecificAlert { get; set; }
        public int VibrationPercent { get; set; }
        public int BrightnessPercent { get; set; }
        public DateTime LastUpdated { get; set; }

        /// <summary>
        ///     determine IsVisible is either true or false based on 
        ///     a specific alert has either both or only one setting (vibration/brightness)
        /// </summary>
        #region

        // the bool property is true if a specific alert has both vibration and brightness are settled by an user
        private bool hasBothVibandBri;
        public bool HasBothVibandBri 
        {
            get => hasBothVibandBri = (VibrationPercent == 0 || BrightnessPercent == 0) ? false : true;
            set => hasBothVibandBri = value;
        }

        private bool hasOnlyOneSetting;
        public bool HasOnlyOneSetting
        {
            get => hasOnlyOneSetting = !HasBothVibandBri;
            set => hasOnlyOneSetting = value;
        }

        private int font;

        public int Font
        {
            get => font = (Percent == 100) ? 32 : 45;
            set => font = value;
        }

        // either brightness or vibration if the specific alert has only one setting (doesn't have both vibration and brightness)
        private string specificSetting;
        public string SpecificSetting
        {
            get => specificSetting = (VibrationPercent == 0) ? "Brightness" : "Vibration";
            set => specificSetting = value;
        }

        // if HasBothVibandBri is false, applys either brightnessPercent or VibrationPercent that is settled to Percent
        private int percent;
        public int Percent
        {
            get => percent = (VibrationPercent == 0) ? BrightnessPercent : VibrationPercent;
            set => percent = value;
        }
        #endregion

        /// <summary>
        ///     add "%" to the string to display it if a specific alert has both settings (vibration and brightness)  
        /// </summary>
        #region

        private string brightnessPercentString;
        public string BrightnessPercentString
        {
            get => brightnessPercentString =
                (HasBothVibandBri) ? BrightnessPercent.ToString() + "%" : BrightnessPercent.ToString();
            set => brightnessPercentString = value;
        }


        private string vibrationPercentString;
        public string VibrationPercentString
        {
            get => vibrationPercentString =
                (HasBothVibandBri) ? VibrationPercent.ToString() + "%" : VibrationPercent.ToString();
            set => vibrationPercentString = value;
        }
        #endregion
        // -------------------------- //

        // We will work on those more properties below later
        public Color Color { get; set; }
    }
}
