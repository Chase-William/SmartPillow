using Microcharts;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPillowLib.ViewModels
{
    public class HistoryViewModel : NotifyClass
    {
        public static List<History> Months { get; set; } = UserInformation.User.UserData;
        private bool isNoHistoryVisble = false;
        private bool isHaveHistoryVisble = false;

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
        }

        private int position;

        public int Position
        {
            get 
            {
                if (Months != null )
                {
                    if (Months.Count() != 0)
                        position = Months.Count() - 1;
                }
                else
                    position = 0;
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
            get 
            {
                if (Months != null)
                    weeks = Months[position].Weeks;

                return weeks; 
            }
            set { weeks = value; }
        }

        public void OnAppearing()
        {
            NotifyPropertiesChanged(nameof(History),
                                    nameof(User),
                                    nameof(ProfileImage),
                                    nameof(IsNoHistoryVisble),
                                    nameof(IsHaveHistoryVisible));
        }
    }
}
