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

namespace SmartPillow.Droid
{
    public static class Configuration
    {
        public const string ClientId = "300644153670-d7enm5rerpojto6gcb4hiibmch34stip.apps.googleusercontent.com";
        public const string Scope = "email";
        public const string RedirectUrl = "com.companyname.smartpillow:/oauth2redirect";
    }

}