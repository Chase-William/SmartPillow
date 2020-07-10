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
using Xamarin.Auth;
using Xamarin.Forms;

namespace SmartPillowLib.ViewModels
{
    public class LoginViewModel : NotifyClass
    {
        public event Action PopAsyncPage;
        public event Action OpenTwitterPage;
        public event Action FBCanceled;
        public static event Action CheckStatus;
        public User User { get; set; }
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
                    DataUrl = "markZ"
                };
                user.UserData = GetHistories(user.DataUrl);
                SetLightBlueAndCloseLoginPage(user);
                UserInformation.User = user;
                UserInformation.IsUserLogged = true;
                UserInformation.IsConnected = true;
                CheckStatus?.Invoke();
                PopAsyncPage?.Invoke();
                IsVisible = false;
            });
        });

        public ICommand TwitterCommand => new Command(() =>
        {
            OpenTwitterPage?.Invoke();

            //await Task.Run(() =>
            //{
            //    // For testing purpose
            //    var user = new User()
            //    {
            //        FirstName = "Twitter",
            //        LastName = "",
            //        Image = "Twitter.png",
            //        Email = "Twitter@gmail.com",
            //        PhoneNumber = "111-111-1111",
            //        SmartPillowDeviceID = "SP123-19B",
            //        DataUrl = "twitter"
            //    };
            //    user.UserData = GetHistories(user.DataUrl);
            //    SetLightBlueAndCloseLoginPage(user);
            //    UserInformation.User = user;
            //    UserInformation.IsUserLogged = true;
            //    UserInformation.IsConnected = true;
            //    CheckStatus?.Invoke();
            //    PopAsyncPage?.Invoke();
            //    IsVisible = false;
            //});
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
                    DataUrl = "google"
                };
                user.UserData = GetHistories(user.DataUrl);
                SetLightBlueAndCloseLoginPage(user);
                UserInformation.User = user;
                UserInformation.IsUserLogged = true;
                UserInformation.IsConnected = true;
                CheckStatus?.Invoke();
                PopAsyncPage?.Invoke();
                IsVisible = false;
            });
        });

        public ICommand FacebookCommand => new Command(async () =>
        {
            IsVisible = true;
            await LoginFacebookAsync();
        });

        async Task LoginFacebookAsync()
        {
            try
            {
                if (_facebookService.IsLoggedIn)
                    _facebookService.Logout();

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
                                Id = facebookProfile.Id,
                            };
                            User = user;

                            if (User.Id != null)
                            {
                                var existed = CheckUrlStatus(
                                    "https://quoridge.blob.core.windows.net/bugle/" + User.Id + ".json");

                                if(existed)
                                {
                                    User.UserData = GetHistories(User.Id);
                                    if (User != null)
                                        SetLightBlueAndCloseLoginPage(User);
                                }
                            }
                            UserInformation.User = User;
                            UserInformation.IsUserLogged = true;
                            UserInformation.IsConnected = true;
                            CheckStatus?.Invoke();
                            PopAsyncPage?.Invoke();
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

        /// <summary>
        ///     Read User's data in json file and deserialize it to User's History
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<History> GetHistories(string id)
        {
            var list = new List<History>();
            var client = new WebClient();
            Stream all = client.OpenRead("https://quoridge.blob.core.windows.net/bugle/" + id + ".json");
            using (StreamReader reader = new StreamReader(all))
            {
                var page = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<History>>(page);
            }
            return list;
        }

        /// <summary>
        ///     Setting non-colors to actual colors after deserializing json for every single of entry -_-
        /// </summary>
        /// <param name="user"></param>
        public void SetLightBlueAndCloseLoginPage(User user)
        {
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
        }

        /// <summary>
        ///     Check the url if it is existed or not
        /// </summary>
        /// <param name="Website"></param>
        /// <returns></returns>
        protected bool CheckUrlStatus(string Website)
        {
            try
            {
                var request = WebRequest.Create(Website) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                    return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
