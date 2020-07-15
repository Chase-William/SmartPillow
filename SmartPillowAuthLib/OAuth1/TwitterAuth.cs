using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Auth;

namespace SmartPillowAuthLib.OAuth1.TwitterOAuth
{
    public class TwitterAuth
    {
        public event Action<object[], TwitterProfile> SendProfileInfo;

        public object[] LoginInfo = new object[3];
        public TwitterProfile Profile { get; set; }

        public string ConsumerKey = "RdghlGeE9yKzXmqfKclCxqeWR";
        public string ConsumerSecret = "UblALjSOXJTRAz0DvqHhtJcDm9qpIpzYd0CHE8FyhNVO99JBnP";
        public string TwitterAuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        public string TwitterAccessTokenUrl = "https://api.twitter.com/oauth/access_token/";
        public string TwitterRequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        public string CallBackUrl = "http://mobile.twitter.com";

        public OAuth1Authenticator GetAuth()
        {
            var auth = new OAuth1Authenticator(
                ConsumerKey,
                ConsumerSecret,
                new Uri(TwitterRequestTokenUrl),
                new Uri(TwitterAuthorizeUrl),
                new Uri(TwitterAccessTokenUrl),
                new Uri(CallBackUrl),
                null,
                false);

            auth.Completed += Authenticator_Completed;
            auth.Error += Authenticator_Failed;

            return auth;
        }

        public void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Profile = new TwitterProfile()
                {
                    Id = e.Account.Properties["user_id"],
                    Name = e.Account.Properties["screen_name"],
                };

                var d = new Dictionary<string, string>();
                var request = new OAuth1Request("GET",
                    new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                    d, e.Account, false);
                var account = JsonConvert.SerializeObject(e.Account);

                //       [0] = id,         [1] = logged with         [2] = accountString
                LoginInfo[0] = 0; LoginInfo[1] = "twitter"; LoginInfo[2] = account;

                UserProfileAsync(request);
            }
        }

        public async void UserProfileAsync(OAuth1Request request)
        {
            var response = await request.GetResponseAsync();

            var json = response.GetResponseText();

            var twitterUser = JsonConvert.DeserializeObject<TwitterProfile>(json);

            //remove "_normal" to get an original size of the image
            var bigImage = twitterUser.Profile_image_url_https.Replace("_normal", "");

            if (Profile == null)
                Profile = twitterUser;

            Profile.Profile_image_url_https = bigImage;
            SendProfileInfo?.Invoke(LoginInfo, Profile);
        }

        public void Authenticator_Failed(object sender, AuthenticatorErrorEventArgs e)
        {

        }
    }
}
