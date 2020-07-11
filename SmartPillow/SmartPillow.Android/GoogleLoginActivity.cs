using Android.App;
using Android.OS;
using Android.Widget;
using SmartPillowAuthLib.OAuth2.GoogleOAuth;
using SmartPillowAuthLib.OAuth2.GoogleOAuth.Services;
using System;

namespace SmartPillow.Droid
{
    public class GoogleLoginActivity : Activity, IGoogleAuthenticationDelegate
    {
        // Need to be static because we need to access it
        // in GoogleAuthInterceptor for continuation
        public static GoogleAuthenticator Auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GoogleLogin);

            Auth = new GoogleAuthenticator(
                Configuration.ClientId,
                Configuration.Scope,
                Configuration.RedirectUrl,
                this); // Completed/Error events

            var googleLoginButton =
                FindViewById<Button>(Resource.Id.googleLoginButton);

            googleLoginButton.Click += OnGoogleLoginButtonClicked;
        }

        #region Google Auth2.0 Functions
        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            // Display the activity handling the authentication
            var authenticator = Auth.GetAuthenticator();
            var intent = authenticator.GetUI(this);
            StartActivity(intent);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            // Retrieve the user's email address
            var googleService = new GoogleService();
            var email =
                await googleService.GetEmailAsync(
                    token.TokenType,
                    token.AccessToken);

            // Display it on the UI
            var googleButton =
                FindViewById<Button>(Resource.Id.googleLoginButton);

            googleButton.Text = $"Connected with {email}";
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            new AlertDialog.Builder(this)
                .SetTitle(message)
                .SetMessage(exception?.ToString())
                .Show();
        }

        public void OnAuthenticationCanceled()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}