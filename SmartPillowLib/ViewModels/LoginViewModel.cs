using Newtonsoft.Json;
using Plugin.FacebookClient;
using SkiaSharp;
using SmartPillowLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class LoginViewModel : NotifyClass
    {
        public event Action PopAsyncPage;
        public event Action FBCanceled;
        public static event Action CheckStatus;
        public static string[] LineColors = new string[] { "#7AC0DF", "#A794EE", "#D06BFC", "#92A9E7", "#BC7FF5" };

        IFacebookClient _facebookService = CrossFacebookClient.Current;

        private bool isVisible;

        public bool IsVisible
        {
            get => isVisible;
            set 
            { 
                isVisible = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand LoginCommand => new Command(async () =>
        {
            // !!! I need to code this deeper for login function
            IsVisible = true;

            /// <summary>
            ///     uses task.run to allow activity indicator to be displayed 
            ///     while reading a json file and stores data into UserInformation
            /// </summary>
            await Task.Run(() =>
            {
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
                IsVisible = false;
            });
        });

        public ICommand TwitterCommand => new Command(async () =>
        {
            // !!! I need to code this deeper for login function
            IsVisible = true;

            await Task.Run(() =>
            {
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
                IsVisible = false;
            });
        });

        public ICommand GoogleCommand => new Command(async () =>
        {
            // !!! I need to code this deeper for login function
            IsVisible = true;

            await Task.Run(() =>
            {
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
                IsVisible = false;
            });
        });

        public ICommand FacebookCommand => new Command(async () =>
        {
            // !!! I need to code this deeper for login function
            //IsVisible = true;
            await LoginFacebookAsync();
            /// <summary>
            ///     uses task.run to allow activity indicator to be displayed 
            ///     while reading a json file and stores data into UserInformation
            /// </summary>
            //await Task.Run(() =>
            //{
            //    // For testing purpose
            //    var user = new User()
            //    {
            //        FirstName = "Facebook Inc",
            //        LastName = "",
            //        Image = "Facebook.png",
            //        Email = "Facebook@gmail.com",
            //        PhoneNumber = "333-333-3333",
            //        SmartPillowDeviceID = "WQW31-25X",
            //        DataUrl = "https://quoridge.blob.core.windows.net/bugle/facebook.json"
            //    };
            //    user.UserData = GetHistories(user.DataUrl);
            //    SetLightBlueAndCloseLoginPage(user);
            //    IsVisible = false;
            //});
        });
        async Task LoginFacebookAsync()
        {
            try
            {
                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null) return;

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            var user = new User
                            {
                                Email = facebookProfile.Email,
                                FirstName = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                                Image = facebookProfile.Picture.Data.Url,
                            };
                            UserInformation.User = user;
                            UserInformation.IsUserLogged = true;
                            UserInformation.IsConnected = true;
                            PopAsyncPage?.Invoke();
                            CheckStatus?.Invoke();
                            break;
                        case FacebookActionStatus.Canceled:
                            FBCanceled?.Invoke();
                            break;
                        case FacebookActionStatus.Unauthorized:
                            FBCanceled?.Invoke();
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name", "picture.width(1500).height(1500)" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public LoginViewModel()
        {
            // hides activity indicator when LoginPage gets opened
            IsVisible = false;
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
            // setting non-colors to actual colors after deserializing json for every single of entry -_-
            foreach (var item in user.UserData)
                foreach (var find in item.Weeks)
                {
                    foreach (var cmon in find.SleepQualityChart.Entries)
                    {
                        cmon.Color = SKColor.Parse("#7AC0DF");
                        cmon.TextColor = SKColor.Parse("#7AC0DF");
                    }

                    foreach (var day in find.Days)
                    {
                        day.EventChart.BackgroundColor = SKColors.Transparent;
                        foreach (var events in day.EventChart.Entries)
                        {
                            switch (events.Label)
                            {
                                case "Car": events.Color = SKColor.Parse("#91BDFF"); break;
                                case "Doorbell": events.Color = SKColor.Parse("#B1FFD5"); break;
                                case "Fire": events.Color = SKColor.Parse("#FF8585"); break;
                                case "Baby": events.Color = SKColor.Parse("#FFC4F7"); break;
                                case "Nature": events.Color = SKColor.Parse("#FFD685"); break;
                                case "Breakin": events.Color = SKColor.Parse("#BE6DEC"); break;
                                case "Smoke": events.Color = SKColor.Parse("#BBBBBB"); break;
                            }
                        }

                        day.LineChart.BackgroundColor = SKColors.Transparent;
                        int lineColorCount = 0;
                        foreach (var lines in day.LineChart.Entries)
                        {
                            lines.TextColor = SKColor.Parse("#B2B2B2");
                            lines.Color = SKColor.Parse(LineColors[lineColorCount]);

                            if (lineColorCount == 4)
                                lineColorCount = 0;
                            else
                                lineColorCount++;
                        }

                        day.SnoreChart.BackgroundColor = SKColors.Transparent;
                        foreach (var snores in day.SnoreChart.Entries)
                        {
                            snores.Color = (snores.Value == 0) ? SKColor.Parse("#0a00000c") 
                                : SKColor.Parse("#00C2FF");
                        }

                        foreach (var alert in day.Alerts)
                        {
                            switch (alert.Name)
                            {
                                case "Car": alert.Color = SKColor.Parse("#91BDFF"); break;
                                case "Doorbell": alert.Color = SKColor.Parse("#B1FFD5"); break;
                                case "Fire": alert.Color = SKColor.Parse("#FF8585"); break;
                                case "Baby": alert.Color = SKColor.Parse("#FFC4F7"); break;
                                case "Nature": alert.Color = SKColor.Parse("#FFD685"); break;
                                case "Breakin": alert.Color = SKColor.Parse("#BE6DEC"); break;
                                case "Smoke": alert.Color = SKColor.Parse("#BBBBBB"); break;
                            }
                        }

                        switch (day.TotalSleep.Hours)
                        {
                            case 9: day.Margin = new Thickness(40, 310, 50, 21); day.SnoreMargin = new Thickness(40, 289, 0, 17); day.Width = 290; break;
                            case 8: day.Margin = new Thickness(51, 310, 50, 21); day.SnoreMargin = new Thickness(62, 289, 0, 17); day.Width = 259; break;
                            case 7: day.Margin = new Thickness(51, 310, 43, 21); day.SnoreMargin = new Thickness(62, 289, 0, 17); day.Width = 257; break;
                            case 6: day.Margin = new Thickness(55, 310, 48, 21); day.SnoreMargin = new Thickness(67, 289, 0, 17); day.Width = 250; break;
                            case 5: day.Margin = new Thickness(60, 310, 50, 21); day.SnoreMargin = new Thickness(67, 289, 0, 17); day.Width = 246; break;
                            case 4: day.Margin = new Thickness(60, 310, 50, 21); day.SnoreMargin = new Thickness(70, 289, 0, 17); day.Width = 238; break;
                        }
                    }
                }

            UserInformation.User = user;
            UserInformation.IsUserLogged = true;
            UserInformation.IsConnected = true;
            CheckStatus?.Invoke();
            PopAsyncPage?.Invoke();
        }
    }
}
