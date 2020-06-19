using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using SmartPillowLib.Models;
using System.Windows.Input;

namespace SmartPillowLib.ViewModels
{
    public class HomeViewModel : NotifyClass
    {
        public event Action OpenLoginPage;

        public event Action OpenProfilePage;

        public bool IsUserLogged
        {
            get { return UserInformation.IsUserLogged; }
            set 
            { 
                UserInformation.IsUserLogged = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand UserCommand => new Command(() =>
        {
            if (IsUserLogged == false)
                OpenLoginPage?.Invoke();

            else
                OpenProfilePage?.Invoke();
        });

        public HomeViewModel()
        {
            //testing to see if image is displayed sucessfully

            var guest = new User() { Image = "Guest.png" };
            var mark = new User() { Image = "Zack.png" };

            if (IsUserLogged == false)
                User = guest;

            else
                User = mark;
        }

        public User User
        {
            get { return UserInformation.User; }
            set
            {
                UserInformation.User = value;
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
