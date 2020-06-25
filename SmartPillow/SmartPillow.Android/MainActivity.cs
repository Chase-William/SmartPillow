using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.IO;

namespace SmartPillow.Droid
{
    [Activity(Label = "Smart Pillow", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

         
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);    // Getting the folder path that already exist on the device and will be used to map a location to our database.   
            string completedPath = Path.Combine(folderPath, App.DatabaseKeys.DATABASE_NAME);                    // Combining the two paths to create a completed path

            LoadApplication(new App(completedPath));      
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            // Invoking the backbtn pressed func
            App.OnHWBackBtnPressed?.Invoke();
            base.OnBackPressed();
        }
    }
}