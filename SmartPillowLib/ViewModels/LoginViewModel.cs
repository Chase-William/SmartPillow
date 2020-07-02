using Newtonsoft.Json;
using SkiaSharp;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
                SmartPillowDeviceID = "ZZ987-19C",
                DataUrl = "https://quoridge.blob.core.windows.net/bugle/markZ.json"
            };

            user.UserData = GetHistories(user.DataUrl);
            SetLightBlueAndCloseLoginPage(user);
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
                SmartPillowDeviceID = "SP123-19B",
                DataUrl = "https://quoridge.blob.core.windows.net/bugle/twitter.json"
            };

            user.UserData = GetHistories(user.DataUrl);
            SetLightBlueAndCloseLoginPage(user);
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
                SmartPillowDeviceID = "YW455-19D",
                DataUrl = "https://quoridge.blob.core.windows.net/bugle/google.json"
            };

            user.UserData = GetHistories(user.DataUrl);
            SetLightBlueAndCloseLoginPage(user);
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
                SmartPillowDeviceID = "WQW31-25X",
                DataUrl = "https://quoridge.blob.core.windows.net/bugle/facebook.json"
            };

            user.UserData = GetHistories(user.DataUrl);
            SetLightBlueAndCloseLoginPage(user);
        });

        public LoginViewModel()
        {
          
        }

        public List<History> GetHistories(string url)
        {
            var list = new List<History>();
            var client = new WebClient();
            Stream all = client.OpenRead(url);
            using (StreamReader reader = new StreamReader(all))
            {
                var page = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<History>>(page);
            }
            return list;
        }

        // Temporarly method
        public void SetLightBlueAndCloseLoginPage(User user)
        {
            // setting color to light blue for every single of entry -_-
            foreach (var item in user.UserData)
                foreach (var find in item.Weeks)
                    foreach (var cmon in find.SleepQualityChart.Entries)
                    {
                        cmon.Color = SKColor.Parse("#7AC0DF");
                        cmon.TextColor = SKColor.Parse("#7AC0DF");
                    }

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = true;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        }
    }
}
