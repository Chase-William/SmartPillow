using SmartPillowLib.Models;
namespace SmartPillowLib.ViewModels.TimedAlarmVMs
{
    public class SnoozeSettingsVM : NotifyClass
    {
        public SnoozeProperties SnoozeProps { get; set; }

        #region Interval Const
        public const byte RADIO_OPTION_1_IN_MINUTES = 5;
        public const byte RADIO_OPTION_2_IN_MINUTES = 10;
        public const byte RADIO_OPTION_3_IN_MINUTES = 20;
        public const byte RADIO_OPTION_4_IN_MINUTES = 30;
        #endregion

        #region Repeat Const
        public const byte RADIO_OPTION_1_REPEAT = 3;
        public const byte RADIO_OPTION_2_REPEAT = 5;
        public const byte RADIO_OPTION_3_REPEAT = byte.MaxValue;
        #endregion

        /// <summary>
        /// 
        ///     Note:
        ///     
        ///     All Intermediate properties could be removed if the SnoozeProperties class was couples to the UI.
        ///     I did not want to do that so instead they remain seperated like this.
        /// 
        /// </summary>

        #region Interval Intermediate Props
       
        public bool IntervalRadioBtnOption1
        {
            get => SnoozeProps.Interval == RADIO_OPTION_1_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption1 == value) return;

                SnoozeProps.Interval = RADIO_OPTION_1_IN_MINUTES;
            }
        }
        public bool IntervalRadioBtnOption2
        {
            get => SnoozeProps.Interval == RADIO_OPTION_2_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption2 == value) return;

                SnoozeProps.Interval = RADIO_OPTION_2_IN_MINUTES;
            }
        }
        public bool IntervalRadioBtnOption3
        {
            get => SnoozeProps.Interval == RADIO_OPTION_3_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption3 == value) return;

                SnoozeProps.Interval = RADIO_OPTION_3_IN_MINUTES;
            }
        }
        public bool IntervalRadioBtnOption4
        {
            get => SnoozeProps.Interval == RADIO_OPTION_4_IN_MINUTES;
            set
            {
                if (IntervalRadioBtnOption4 == value) return;

                SnoozeProps.Interval = RADIO_OPTION_4_IN_MINUTES;
            }
        }
        #endregion

        #region Repeat Intermediate Props
        public bool RepeatRadioBtnOption1
        {
            get => SnoozeProps.Repeat == RADIO_OPTION_1_REPEAT;
            set
            {
                if (RepeatRadioBtnOption1 == value) return;

                SnoozeProps.Repeat = RADIO_OPTION_1_REPEAT;
            }
        }
        public bool RepeatRadioBtnOption2
        {
            get => SnoozeProps.Repeat == RADIO_OPTION_2_REPEAT;
            set
            {
                if (RepeatRadioBtnOption2 == value) return;

                SnoozeProps.Repeat = RADIO_OPTION_2_REPEAT;
            }
        }
        public bool RepeatRadioBtnOption3
        {
            get => SnoozeProps.Repeat == RADIO_OPTION_3_REPEAT;
            set
            {
                if (RepeatRadioBtnOption3 == value) return;

                SnoozeProps.Repeat = RADIO_OPTION_3_REPEAT;
            }
        }
        #endregion

        public SnoozeSettingsVM(SnoozeProperties _snoozeProps)
        {
            SnoozeProps = _snoozeProps;

            NotifyPropertiesChanged(nameof(SnoozeProps.IsEnabled),
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
