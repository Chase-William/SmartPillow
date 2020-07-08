using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillowLib.ViewModels.SettingsVMs
{
    public class PhoneSettingsVM : NotifyClass
    {
        public event Action<AnimationQuality> SaveAnimationQuality;

        private string animationQualitySelected;
        public string AnimationQualitySelected
        {
            get => animationQualitySelected;
            set
            {
                if (AnimationQualitySelected == value) return;

                animationQualitySelected = value;

                AnimationQuality quality;
                switch(AnimationQualitySelected)
                {
                    case "Simple":
                        quality = AnimationQuality.Simple;
                        break;
                    case "Intermediate":
                        quality = AnimationQuality.Intermediate;
                        break;
                    case "Fancy":
                        quality = AnimationQuality.Fancy;
                        break;
                    default:
                        quality = AnimationQuality.Fancy;
                        break;
                }                                             

                SaveAnimationQuality?.Invoke(quality);
                NotifyPropertyChanged();
            }
        }
    }
}
