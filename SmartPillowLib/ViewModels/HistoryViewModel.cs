using SmartPillow.Util;
using SmartPillowLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartPillowLib.ViewModels
{
    public class HistoryViewModel : NotifyClass
    {
        public List<History> Months { get; set; } = UserInformation.User.UserData;
        private bool isNoHistoryVisble = false;
        private bool isHaveHistoryVisble = false;
        public int position;
        private string brightness;

        public bool IsNoHistoryVisble
        {
            get => isNoHistoryVisble;
            set 
            { 
                isNoHistoryVisble = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsHaveHistoryVisible
        {
            get => isHaveHistoryVisble;
            set
            {
                isHaveHistoryVisble = value;
                NotifyPropertyChanged();
            }
        }

        public User User
        {
            get => UserInformation.User;
            set 
            {
                UserInformation.User = value;
                NotifyPropertyChanged();
            }
        }

        public string ProfileImage
        {
            get => User.Image;
            set 
            { 
                User.Image = value;
                NotifyPropertyChanged();
            }
        }

        public List<History> History
        {
            get => UserInformation.User.UserData;
            set
            {
                UserInformation.User.UserData = value;
                NotifyPropertyChanged();
            }
        }

        public HistoryViewModel()
        {
            if (History == null) IsNoHistoryVisble = true;
            if (History != null) IsHaveHistoryVisible = true;

            if (Months != null)
                if (Months.Count() != 0)
                    position = Months.Count() - 1;
        }

        public int Position
        {
            get 
            {
                if (Months != null)
                    if (Months.Count() != 0)
                        Weeks = History[position].Weeks;

                return position; 
            }
            set 
            { 
                position = value;
                NotifyPropertyChanged();
            }
        }

        private List<Week> weeks;

        public List<Week> Weeks
        {
            get => weeks; 
            set 
            { 
                weeks = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     HomePage's brightness will be darker if local time is in between 12AM and 6AM
        /// </summary>
        public string Brightness
        {
            get => AutoBrightness.CheckNightTime();
            set
            {
                brightness = value;
                NotifyPropertyChanged();
            }
        }

        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(History),
                                    nameof(User),
                                    nameof(ProfileImage),
                                    nameof(Position),
                                    nameof(Months),
                                    nameof(Weeks),
                                    nameof(IsNoHistoryVisble),
                                    nameof(IsHaveHistoryVisible));
        }
    }
}
