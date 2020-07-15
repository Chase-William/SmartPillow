using Xamarin.Forms;
using SmartPillow.Pages.Nav;
using System.Threading.Tasks;
using System;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillowLib.Data.Local;
using SmartPillowAuthLib.OAuth2.FacebookOAuth;
using SmartPillowLib;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;
using SmartPillowAuthLib.OAuth1.TwitterOAuth;
using Newtonsoft.Json;
using Xamarin.Auth;
using System.Collections.Generic;

/// <summary>
///     We use Custom fonts for our app theme
/// </summary
[assembly: ExportFont("Ubuntu-Medium.ttf", Alias = "Ubuntu")]
[assembly: ExportFont("Bahnschrift.ttf", Alias = "Bahnschrift")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto")]
namespace SmartPillow
{
    public partial class App : Application
    {
        /// <summary>
        ///     Handler for a backbtn being pressed.
        ///     This event Func is invoked from the native xamarin proj.
        /// </summary>
        public static Func<bool> OnHWBackBtnPressed;

        // Check if user is logged - setting to false for testing
        public static bool IsUserLogged = false;
        public App(string dbPath)
        {
            InitializeComponent();

            // Passing this path to our LocalService Provider to make our calls in the application simplier since we will only have one local db right now.
            SmartPillowLib.Data.Local.LocalDataServiceContext.DatabasePath = dbPath;

            // Initializing our MasterDetailPage which contains our drawer and action bar.
            MainPage = new MainMasterPage();
        }

        protected override void OnStart()
        {
            var loginInfo = LocalDataServiceContext.Provider.GetLoginAccessToken();

            if (loginInfo != null)
            {
                var vm = new LoginViewModel();
                switch (loginInfo.LoginWith)
                {
                    case "facebook":
                        var fb = new FacebookAuth();
                        fb.FacebookUserProfileAsync(loginInfo.AccessTokenValue);
                        fb.SendProfileInfo += (object[] info, FacebookProfile profile) => vm.UpdateFbUser(profile);
                        break;
                    case "twitter":
                        var twitter = new TwitterAuth();
                        var account = JsonConvert.DeserializeObject<Xamarin.Auth.Account>(loginInfo.Auth1Account);
                        var d = new Dictionary<string, string>();
                        var request = new OAuth1Request("GET", new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                        d, account, false);
                        twitter.UserProfileAsync(request);
                        twitter.SendProfileInfo += (object[] info, TwitterProfile profile) => vm.UpdateTwitterUser(profile);
                        break;
                    case "google":
                        break;
                    case "native":
                        vm.LoginWithMarkZ();
                        break;
                }
                UserInformation.IsUserLogged = true;
            }
            else
                UserInformation.IsUserLogged = false;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        ///     Contains keys for App.xaml resourceDictionary.
        /// </summary>
        public readonly struct ResourceKeys
        {
            public const string GRADIENT_BLUE = "GradientBlue";
            public const string GRADIENT_PURP = "GradientPurp";
        }

        /// <summary>
        ///     Contains keys and important information related to the local database.
        /// </summary>
        public readonly struct DatabaseKeys
        {
            public const string DATABASE_NAME = "alarms";
            public readonly string Database_Path;

            public DatabaseKeys(string dbPAth)
            {
                Database_Path = dbPAth;
            }
        }

        /// <summary>
        ///     Keys for animations to use.<br/>
        ///     These key's values can be accessed by a get request to Xamarin.Essentials Preferences.
        /// </summary>
        public readonly struct AnimationKeys
        {
            /// <summary>
            ///     Animation quality for custom animations.
            /// </summary>
            public const string ANIMATION_QUALITY = "animation_quality";
        }
    }
}
