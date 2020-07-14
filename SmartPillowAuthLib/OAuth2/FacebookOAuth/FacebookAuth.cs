using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Auth;

namespace SmartPillowAuthLib.OAuth2.FacebookOAuth
{
    public class FacebookAuth
    {
        public event Action<FacebookProfile> SendProfileInfo;

        public FacebookProfile Profile { get; set; }

        public string ClientID = "561614271198499";
        public string FacebookScope = "email";
        public string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public string FacebookRedirectedUrl = "https://www.facebook.com/connect/login_success.html";
        public string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

        public OAuth2Authenticator GetAuth()
        {
            var auth = new OAuth2Authenticator(
                ClientID,
                FacebookScope,
                new Uri(FacebookAuthorizeUrl),
                new Uri(FacebookRedirectedUrl),
                null,
                false);

            auth.Completed += Authenticator_Completed;
            //auth.Error += Authenticator_Failed;

            return auth;
        }

        public void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                string accesstoken = e.Account.Properties["access_token"];
                FacebookUserProfileAsync(accesstoken);
            }
        }

        public async void FacebookUserProfileAsync(string accesstoken)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(
                "https://graph.facebook.com/me?fields=email,id,name,picture.width(1500).height(1500)&access_token=" +
                accesstoken);

            Profile = JsonConvert.DeserializeObject<FacebookProfile>(response);

            SendProfileInfo?.Invoke(Profile);
        }

        //public void Authenticator_Failed(object sender, AuthenticatorErrorEventArgs e)
        //{

        //}
    }
}
