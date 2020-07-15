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
        public const string ClientId = "300644153670-fks34l4r7sot5nnk9gb8617sg5c657uj.apps.googleusercontent.com";
        public const string Scope = "email";
        public const string RedirectUrl = "com.companyname.smartpillow:/oauth2redirect";
    }

}