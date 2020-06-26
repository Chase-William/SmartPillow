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
        public static event Action CheckStatus;

        public ICommand LoginCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function

            // For testing purpose
            var user = new User()
            {
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Image = "Zack.png",
                Email = "Email@gmail.com",
                PhoneNumber = "585-585-5858",
                SmartPillowDeviceID = "ZZ987-19C"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = false;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        });

        public ICommand TwitterCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function

            // For testing purpose
            var user = new User()
            {
                FirstName = "Twitter",
                LastName = "",
                Image = "Twitter.png",
                Email = "Twitter@gmail.com",
                PhoneNumber = "111-111-1111",
                SmartPillowDeviceID = "SP123-19B"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = true;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        });

        public ICommand GoogleCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function

            // For testing purpose
            var user = new User()
            {
                FirstName = "Google",
                LastName = "",
                Image = "Google.png",
                Email = "Google@gmail.com",
                PhoneNumber = "222-222-2222",
                SmartPillowDeviceID = "YW455-19D"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = false;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        });

        public ICommand FacebookCommand => new Command(() =>
        {
            // !!! I need to code this deeper for login function

            // For testing purpose
            var user = new User()
            {
                FirstName = "Facebook Inc",
                LastName = "",
                Image = "Facebook.png",
                Email = "Facebook@gmail.com",
                PhoneNumber = "333-333-3333",
                SmartPillowDeviceID = "WQW31-25X"
            };

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = true;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        });

        public LoginViewModel()
        {
          
        }
    }
}
