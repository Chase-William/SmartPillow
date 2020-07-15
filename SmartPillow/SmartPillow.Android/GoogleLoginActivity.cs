using Android.App;
using Android.OS;
using Android.Widget;
using SmartPillowAuthLib.OAuth2.GoogleOAuth;
using SmartPillowAuthLib.OAuth2.GoogleOAuth.Services;
using System;
using Android.Content.PM;
using Xamarin.Essentials;
using Android.Content;
using Xamarin.Forms;
using SmartPillowLib.Models;
using SmartPillowLib.ViewModels;

namespace SmartPillow.Droid
{
    [Activity(Label = "GoogleLoginActivity", Theme = "@style/MainTheme", NoHistory = true)]
    public class GoogleLoginActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {
        // Need to be static because we need to access it
        // in GoogleAuthInterceptor for continuation
        public static GoogleAuthenticator Auth;
        
        public static User User { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Auth = new GoogleAuthenticator(
                    Configuration.ClientId,
                    Configuration.Scope,
                    Configuration.RedirectUrl,
                    this); // Completed/Error events

            Login();
        }

        #region Google Auth2 Functions
        private void Login()
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null; // Turning off the toast
            StartActivity(intent);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {           
            // Retrieve the user's email address
            var googleService = new GoogleService();
            GoogleAccountInfo googleAccount = await googleService.GetAccountInfo(token); // ------------------------------ fix this

            User = new User
            {
                Id = googleAccount.Id,
                Image = googleAccount.Image,
                Email = googleAccount.Email
            };

            Finish();
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            new AlertDialog.Builder(this)
                           .SetTitle(message)
                           .SetMessage(exception?.ToString())
                           .Show();
        }

        public void OnAuthenticationCanceled() { }
        #endregion
    }
}