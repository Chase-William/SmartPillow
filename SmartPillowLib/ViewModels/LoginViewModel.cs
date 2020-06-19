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

        public ICommand LoginCommand => new Command(() =>
        {
            IsBusy = false;
            // !!! I need to code this deeper for login function

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

        public LoginViewModel()
        {
            IsBusy = true;
        }
    }
}
