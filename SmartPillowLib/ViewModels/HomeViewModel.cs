using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SmartPillow;
using SmartPillowLib.Models;

namespace SmartPillow.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private string profileImage;
        private User user;
        private bool isUserLogged;

        public INavigation Navigation { get; set; }

        //public bool IsUserLogged
        //{
        //    get { return isUserLogged; }
        //    set { isUserLogged = value; }
        //}

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
            //testing to see if image is displayed sucessfully
            ProfileImage = "Zack.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
