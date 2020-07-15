using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPillowAuthLib.OAuth2.GoogleOAuth.Services;

namespace SmartPillow.Droid
{
    [Activity(Label = "GoogleAuthIntercepter")]
    [IntentFilter(actions: new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataSchemes = new[]
              {
                  // First part of the redirect url (Package name)
                  "com.companyname.smartpillow"
              },
              DataPaths = new[]
              {
                  // Second part of the redirect url (Path)
                  "/oauth2redirect"
              })]
    public class GoogleAuthIntercepter : Activity
    {      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Net.Uri uri_android = Intent.Data;

            // Convert Android Url to C#/netxf/BCL System.Uri
            var uri_netfx = new Uri(uri_android.ToString());

            // Send the URI to the Authenticator for continuation
            GoogleLoginActivity.Auth?.OnPageLoading(uri_netfx);

            Finish();
        }
    }
}