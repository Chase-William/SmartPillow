using SmartPillowLib.Models;
namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class SnoozeSettingsVM : NotifyClass
    {
        private readonly SnoozeProperties snoozeProps;
        private const byte RADIO_OPTION_1_IN_MINUTES = 5;
        private const byte RADIO_OPTION_2_IN_MINUTES = 10;
        private const byte RADIO_OPTION_3_IN_MINUTES = 20;
        private const byte RADIO_OPTION_4_IN_MINUTES = 30;

        public bool IsEnabled
        {
            get => snoozeProps.IsEnabled;
            set
            {
                if (IsEnabled == value) return;

                snoozeProps.IsEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public bool IntervalRadioBtnOption1
        {
            get => snoozeProps.Interval == RADIO_OPTION_1_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption1 == value) return;

                snoozeProps.Interval = RADIO_OPTION_1_IN_MINUTES;
                NotifyPropertyChanged();
            }
        }

        public bool IntervalRadioBtnOption2
        {
            get => snoozeProps.Interval == RADIO_OPTION_2_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption2 == value) return;

                snoozeProps.Interval = RADIO_OPTION_2_IN_MINUTES;
                NotifyPropertyChanged();
            }
        }

        public bool IntervalRadioBtnOption3
        {
            get => snoozeProps.Interval == RADIO_OPTION_3_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption3 == value) return;

                snoozeProps.Interval = RADIO_OPTION_3_IN_MINUTES;
                NotifyPropertyChanged();
            }
        }

        public bool IntervalRadioBtnOption4
        {
            get => snoozeProps.Interval == RADIO_OPTION_4_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption4 == value) return;

                snoozeProps.Interval = RADIO_OPTION_4_IN_MINUTES;
                NotifyPropertyChanged();
            }
        }

        public SnoozeSettingsVM(SnoozeProperties _snoozeProps)
        {
            snoozeProps = _snoozeProps;

            NotifyPropertiesChanged(nameof(IsEnabled),
                                    nameof(IntervalRadioBtnOption1),
                                    nameof(IntervalRadioBtnOption2),
                                    nameof(IntervalRadioBtnOption3),
                                    nameof(IntervalRadioBtnOption4));
        }
    }
}
