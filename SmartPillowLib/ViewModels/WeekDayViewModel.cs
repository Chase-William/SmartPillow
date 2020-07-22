using Microcharts;
using SmartPillow.Util;
using SmartPillowLib.Models;
using System.Collections.ObjectModel;

namespace SmartPillowLib.ViewModels
{
    public class WeekDayViewModel : NotifyClass
    {
        #region Fields
        private Day OldDay;
        private Week week;
        private string dayRange;
        private string brightness;
        #endregion

        #region Properties
        public string ProfileImage
        {
            get => UserInformation.User.Image;
            set
            {
                UserInformation.User.Image = value;
                NotifyPropertyChanged();
            }
        }

        public RadialGaugeChart BackgroundGray
        {
            get => BackgroundGauge.Gray;
            set => BackgroundGauge.Gray = value;
        }

        public string DayRange
        {
            get => Week.Month + " " + Week.DateRange + ", " + Week.Year;
            set => dayRange = value;
        }

        public Week Week
        {
            get => UserInformation.Week;
            set
            {
                week = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Day> Days
        {
            get => Week.Days;
            set
            {
                Week.Days = value;
                NotifyPropertiesChanged();
            }
        }

        public string Brightness
        {
            get => AutoBrightness.CheckNightTime();
            set
            {
                brightness = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        ///     ExpandableListView
        ///     https://www.youtube.com/watch?v=GG10QPrRC3w
        /// </summary>
        /// <param name="day"></param>
        public void HideOrShow(Day day)
        {
            if (OldDay == day)
            {
                // Click twice on the same item will hide it
                day.IsVisible = !day.IsVisible;
                day.Rotation = (day.Rotation == 0) ? 180 : 0;
                UpdateItems(day);
            }
            else
            {
                if (OldDay != null)
                {
                    // Hide previous selected item
                    OldDay.IsVisible = false;
                    OldDay.Rotation = 0;
                    UpdateItems(OldDay);
                }

                // Show selected item
                day.IsVisible = true;
                day.Rotation = 180;

                UpdateItems(day);
            }

            OldDay = day;
        }

        public void UpdateItems(Day day)
        {
            var index = Days.IndexOf(day);
            Days.Remove(day);
            Days.Insert(index, day);
        }
        #endregion

        public WeekDayViewModel() { }
    }
}
