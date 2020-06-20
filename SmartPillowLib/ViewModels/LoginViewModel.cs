using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class LoginViewModel : NotifyClass
    {
        public event Action PopAsyncPage;

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set {
                isBusy = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsDisappearing { get; set; }

        public ICommand LoginCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function
            IsBusy = false;

            // For testing purpose
            var user = new User()
            {
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Image = "Zack.png",
                Email = "Email@gmail.com",
                PhoneNumber = "585-585-5858"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;

            PopAsyncPage?.Invoke();

            IsBusy = true;
        });

        public ICommand TwitterCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function
            IsBusy = false;

            // For testing purpose
            var user = new User()
            {
                FirstName = "Twitter",
                LastName = "",
                Image = "Twitter.png",
                Email = "Twitter@gmail.com",
                PhoneNumber = "111-111-1111"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;

            PopAsyncPage?.Invoke();

            IsBusy = true;
        });

        public ICommand GoogleCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function
            IsBusy = false;

            // For testing purpose
            var user = new User()
            {
                FirstName = "Google",
                LastName = "",
                Image = "Google.png",
                Email = "Google@gmail.com",
                PhoneNumber = "222-222-2222"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;

            PopAsyncPage?.Invoke();

            IsBusy = true;
        });

        public ICommand FacebookCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function
            IsBusy = false;

            // For testing purpose
            var user = new User()
            {
                FirstName = "Facebook Inc",
                LastName = "",
                Image = "Facebook.png",
                Email = "Facebook@gmail.com",
                PhoneNumber = "333-333-3333"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;

            PopAsyncPage?.Invoke();

            IsBusy = true;
        });

        public LoginViewModel()
        {
            IsBusy = true;            
        }
    }
}
