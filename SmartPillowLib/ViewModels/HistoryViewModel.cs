using SmartPillow.Util;
using SmartPillowLib.Models;
using System.Collections.Generic;
using System.Linq; 

namespace SmartPillowLib.ViewModels
{
    public class HistoryViewModel : NotifyClass
    {
        #region Fields
        public List<History> Months { get; set; } = UserInformation.User.UserData;
        private List<Week> weeks;
        private bool isNoHistoryVisble = false;
        private bool isHaveHistoryVisble = false;
        private int position;
        private string brightness;
        #endregion

        #region Properties
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

        public int Position
        {
            get 
            {
                // make sure if a specific month has any week instance before getting an index 
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

        public List<Week> Weeks
        {
            get => weeks; 
            set 
            { 
                weeks = value;
                NotifyPropertyChanged();
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

        public HistoryViewModel()
        {
            // displays "no history" icon if an user doesn't have any history
            if (History == null) IsNoHistoryVisble = true;

            //  otherwise, it displays user's monthly charts
            else IsHaveHistoryVisible = true;

            // automatically moves to user's recorded latest month when opening HistoryPage
            if (Months != null)
                if (Months.Count() != 0)
                    position = Months.Count() - 1;
        }
    }
}
