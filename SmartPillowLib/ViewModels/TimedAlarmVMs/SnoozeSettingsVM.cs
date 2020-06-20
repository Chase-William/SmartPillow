using SmartPillowLib.Models;
namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class SnoozeSettingsVM : NotifyClass
    {
        private readonly SnoozeProperties snoozeProps;

        #region Interval Const
        private const byte RADIO_OPTION_1_IN_MINUTES = 5;
        private const byte RADIO_OPTION_2_IN_MINUTES = 10;
        private const byte RADIO_OPTION_3_IN_MINUTES = 20;
        private const byte RADIO_OPTION_4_IN_MINUTES = 30;
        #endregion

        #region Repeat Const
        private const byte RADIO_OPTION_1_REPEAT = 3;
        private const byte RADIO_OPTION_2_REPEAT = 5;
        private const byte RADIO_OPTION_3_REPEAT = byte.MaxValue;
        #endregion

        #region Interval Intermediate Props
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
        #endregion

        #region Repeat Intermediate Props
        public bool RepeatRadioBtnOption1
        {
            get => snoozeProps.Repeat == RADIO_OPTION_1_REPEAT;
            set
            {
                if (RepeatRadioBtnOption1 == value) return;

                snoozeProps.Repeat = RADIO_OPTION_1_REPEAT;
                NotifyPropertyChanged();
            }
        }
        public bool RepeatRadioBtnOption2
        {
            get => snoozeProps.Repeat == RADIO_OPTION_2_REPEAT;
            set
            {
                if (RepeatRadioBtnOption2 == value) return;

                snoozeProps.Repeat = RADIO_OPTION_2_REPEAT;
                NotifyPropertyChanged();
            }
        }
        public bool RepeatRadioBtnOption3
        {
            get => snoozeProps.Repeat == RADIO_OPTION_3_REPEAT;
            set
            {
                if (RepeatRadioBtnOption3 == value) return;

                snoozeProps.Repeat = RADIO_OPTION_3_REPEAT;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public SnoozeSettingsVM(SnoozeProperties _snoozeProps)
        {
            snoozeProps = _snoozeProps;

            NotifyPropertiesChanged(nameof(IsEnabled),
                                    nameof(IntervalRadioBtnOption1),
                                    nameof(IntervalRadioBtnOption2),
                                    nameof(IntervalRadioBtnOption3),
                                    nameof(IntervalRadioBtnOption4),
                                    nameof(RepeatRadioBtnOption1),
                                    nameof(RepeatRadioBtnOption2),
                                    nameof(RepeatRadioBtnOption3));
        }
    }
}
