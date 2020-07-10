using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartPillow.Droid.Renderers;
using SmartPillow.Pages;
using SmartPillowLib.Models;
using SmartPillowLib;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Auth;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(ProviderPage), typeof(ProviderRenderer))]
namespace SmartPillow.Droid.Renderers
{
    public class ProviderRenderer : PageRenderer
    {
        public ProviderRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            //Get and Assign ProviderName from ProviderLoginPage
            var loginPage = Element as ProviderPage;
            string providername = loginPage.ProviderName;
            var page = new LoginPage();

            var activity = this.Context as Activity;

            var d = new Dictionary<string, string>();
            d.Add("profile_image_url_https", "profile_image_url_https");
                if (providername == "Twitter")
                {
                    var auth = LoginWithTwitter();
                    // After Twitter  login completed 
                    auth.Completed += async (sender, eventArgs) =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            UserInformation.User = new User()
                            {
                                Id = eventArgs.Account.Properties["user_id"],
                                FirstName = eventArgs.Account.Properties["screen_name"],
                            };

                            var request = new OAuth1Request("GET", 
                                new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                                d, eventArgs.Account, false);

                            var response = await request.GetResponseAsync();

                            var json = response.GetResponseText();

                            var twitterUser = JsonConvert.DeserializeObject<Twitter>(json);

                            // "remove _normal" to get an original size of the image
                            var bigImage = twitterUser.profile_image_url_https.Replace("_normal", "");
                            UserInformation.User.Image = bigImage;
                            UserInformation.IsUserLogged = true;
                            loginPage.SuccessfulLoginAction?.Invoke();
                        }
                        else
                        {
                            // The user cancelled
                            loginPage.SuccessfulLoginAction?.Invoke();
                        }
                    };
                    activity.StartActivity(auth.GetUI(activity));
                }
        }
        public OAuth1Authenticator LoginWithTwitter()
        {
            OAuth1Authenticator Twitterauth = null;
            try
            {
                Twitterauth = new OAuth1Authenticator(
                           consumerKey: "RdghlGeE9yKzXmqfKclCxqeWR", 
                           consumerSecret: "UblALjSOXJTRAz0DvqHhtJcDm9qpIpzYd0CHE8FyhNVO99JBnP",
                           requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"), // These values do not need changing
                           authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"), // These values do not need changing
                           accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token/"), // These values do not need changing
                           callbackUrl: new Uri("http://mobile.twitter.com")    // Set this property to the location the user will be redirected too after successfully authenticating
                       );
            }
            catch (Exception ex)
            {

            }
            return Twitterauth;
        }
    }
}