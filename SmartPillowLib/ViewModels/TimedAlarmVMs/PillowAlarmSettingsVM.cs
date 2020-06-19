using SmartPillowLib.Models;

namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class PillowAlarmSettingsVM : NotifyClass
    {
        /// <summary>
        ///     Reference that is scoped to the alarm's Pillow Properties only.
        /// </summary>
        private DeviceProps pillowProps;

        #region Intermediate Binding Properties
        public byte Vibration
        {
            get => pillowProps.Vibration;
            set  
            {
                if (Vibration == value) return;

                pillowProps.Vibration = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsVibrationEnabled
        {
            get => pillowProps.IsVibrationEnabled;
            set
            {
                if (IsVibrationEnabled == value) return;

                pillowProps.IsVibrationEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public byte Brightness
        {
            get => pillowProps.Brightness;
            set
            {
                if (Brightness == value) return;

                pillowProps.Brightness = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsBrightnessEnabled
        {
            get => pillowProps.IsBrightnessEnabled;
            set
            {
                if (IsBrightnessEnabled == value) return;

                pillowProps.IsBrightnessEnabled = value;
                NotifyPropertyChanged();
            }
        }        
        public bool IsEnabled
        {
            get => pillowProps.IsEnabled;
            set
            {
                if (IsEnabled == value) return;

                pillowProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public PillowAlarmSettingsVM(DeviceProps _pillowProps)
        {
            pillowProps = _pillowProps;

            // Making sure all properties and their bound UI components are aware of any starting values.
            NotifyPropertiesChanged(nameof(IsEnabled),
                                    nameof(IsBrightnessEnabled),
                                    nameof(IsVibrationEnabled),
                                    nameof(Brightness),
                                    nameof(Vibration));
        }
    }
}
