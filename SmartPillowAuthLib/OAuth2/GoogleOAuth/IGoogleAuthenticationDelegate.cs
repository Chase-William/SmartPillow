using SmartPillowAuthLib.OAuth2.GoogleOAuth.Services;
using System;

namespace SmartPillowAuthLib.OAuth2.GoogleOAuth
{
    public interface IGoogleAuthenticationDelegate
    {
        void OnAuthenticationCompleted(GoogleOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}
