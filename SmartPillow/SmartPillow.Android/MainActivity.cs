﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using System.IO;
using Android.Content;
using SmartPillow.LocalNotifications.Droid;
using SmartPillow.CustomAbstractions.LocationNotification;

namespace SmartPillow.Droid
{
    [Activity(Label = "Smart Pillow", 
              Icon = "@mipmap/icon", 
              Theme = "@style/MainTheme", 
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
              LaunchMode = LaunchMode.SingleTop)] // The SingleTop mode prevents multiple instances of an Activity from being started while the application is in the foreground 
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

            CreateNotificationFromIntent(Intent);

            // Initing our LocalSmartPillowAlarm
            LocalSmartPillowAlarm.LocalSmartPillowAlarm.Init(this);

            /// Assigning our shared code callbacks for when we want to run background task/jobs/work whatever.

            // @Param1 - Subscriber
            // @Param2 - Message
            // @Param3 - Callback func
            //MessagingCenter.Subscribe<StartLongRunningTaskMsg>(this, nameof(StartLongRunningTaskMsg), message =>
            //{
            //    var intent = new Intent(this, typeof(LongRunningTaskService));
            //    StartService(intent);
            //});

            //MessagingCenter.Subscribe<StopLongRunningTaskMsg>(this, nameof(StopLongRunningTaskMsg), message =>
            //{
            //    var intent = new Intent(this, typeof(LongRunningTaskService));
            //    StopService(intent);
            //});            
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        /// <summary>
        ///     Creates a native notification.
        /// </summary>
        /// <param name="intent"></param>
        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.Extras.GetString(AndroidNotificationManager.TitleKey);
                string message = intent.Extras.GetString(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
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