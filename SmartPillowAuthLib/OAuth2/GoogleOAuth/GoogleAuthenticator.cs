using SmartPillowAuthLib.OAuth2.GoogleOAuth.Services;
using System;
using Xamarin.Auth;

namespace SmartPillowAuthLib.OAuth2.GoogleOAuth
{
    public class GoogleAuthenticator
    {
        private const string AuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";

        private OAuth2Authenticator auth;
        private IGoogleAuthenticationDelegate authenticationDelegate;

        public GoogleAuthenticator(string clientId, string scope, string redirectUrl, IGoogleAuthenticationDelegate _authenticationDelegate)
        {
            authenticationDelegate = _authenticationDelegate;

            auth = new OAuth2Authenticator(clientId,
                                            string.Empty,
                                            scope,
                                            new Uri(AuthorizeUrl),
                                            new Uri(redirectUrl),
                                            new Uri(AccessTokenUrl),
                                            null,
                                            true);

            auth.Completed += OnAuthenticationCompleted;
            auth.Error += OnAuthenticationFailed;           
        }

        /// <summary>
        ///     returns the underlying authenticator instance.
        /// </summary>
        /// <returns></returns>
        public OAuth2Authenticator GetAuthenticator()
        {
            return auth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public void OnPageLoading(Uri uri)
        {
            auth.OnPageLoading(uri);
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {            

            if (e.IsAuthenticated)
            {
                // Creates an instance of our token container
                var token = new GoogleOAuthToken
                {
                    TokenType = e.Account.Properties["token_type"],     // Getting the type of token
                    AccessToken = e.Account.Properties["access_token"]  // Getting the value of the token
                };
                authenticationDelegate.OnAuthenticationCompleted(token);      
            }
            else
            {
                authenticationDelegate.OnAuthenticationCanceled();
            }
        }

        private void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            authenticationDelegate.OnAuthenticationFailed(e.Message, e.Exception);
        }
    }
}
