using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using SmartPillowLib.Models;

namespace SmartPillowLib.ViewModels
{
    public class HomeViewModel : NotifyClass
    {
        private string profileImage;
        private User user;

        public INavigation Navigation { get; set; }

        public bool IsUserLogged
        {
            get { return UserInformation.IsUserLogged; }
            set 
            { 
                UserInformation.IsUserLogged = value;
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

            //var userData = new UserInformation();
            //var guest = userData.User;

            var guest = new User() { Image = "Guest.png" };
            //testing to see if image is displayed sucessfully
            if (IsUserLogged == false)
                User = guest;
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
            get { return User.Image; }
            set
            {
                User.Image = value;
                NotifyPropertyChanged();
            }
        }
    }
}
