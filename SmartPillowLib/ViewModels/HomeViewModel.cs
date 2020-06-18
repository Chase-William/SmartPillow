using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SmartPillowLib.Models;
using SmartPillowLib;

namespace SmartPillow.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private string profileImage;
        private User user;

        public INavigation Navigation { get; set; }

        public bool IsUserLogged
        {
            get { return UserInformation.IsUserLogged; }
            set { UserInformation.IsUserLogged = value; }
        }

        public User User
        {
            get { return user; }
            set 
            { 
                user = value;
                NotifyPropertyChanged();
            }
        }


        public string ProfileImage
        {
            get { return profileImage; }
            set { 
                profileImage = value;
                NotifyPropertyChanged();
            }
        }

        //public ICommand UserCommand => new Command(async () =>
        //{
        //    if (IsUserLogged == true)
        //        await Navigation.PushAsync(new ProfilePage());
        //    else
        //        await Navigation.PushAsync(new LoginPage());

        //});

        public HomeViewModel()
        {
            IsUserLogged = false;

            //testing to see if image is displayed sucessfully
            if (IsUserLogged == false)
                ProfileImage = "Guest.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
