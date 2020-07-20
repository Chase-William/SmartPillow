using System;
using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Timers;
using Xamarin.Essentials;
using SmartPillowLib.Models;
using SmartPillowLib.Data.Local;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Droid.Locals.SmartPillowAlarm;

namespace SmartPillow.Droid
{
    [Activity(Label = "AlarmActivity", Theme = "@style/MainTheme", NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTask)]
    public class AlarmActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        TextView timeTextView;

        Timer timeUpdater;

        Alarm alarm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AlarmActivityLayout);

            // add flags to turn screen on and appear over lock screen
            //Window.AddFlags(WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.DismissKeyguard | WindowManagerFlags.KeepScreenOn | WindowManagerFlags.TurnScreenOn);
            //Window.AddFlags(WindowManagerFlags.DismissKeyguard);
            //Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            //Window.AddFlags(WindowManagerFlags.TurnScreenOn);

            Window.AddFlags(WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.DismissKeyguard | WindowManagerFlags.KeepScreenOn | WindowManagerFlags.TurnScreenOn);            

            // For higher api levels this turns off the key gaurd
            KeyguardManager keyguardManager = (KeyguardManager)GetSystemService(Context.KeyguardService);
            if (keyguardManager != null)
            {
                keyguardManager.RequestDismissKeyguard(this, null);
            }

            // If no extras attached return
            if (this.Intent.Extras == null) return;                        
            alarm = LocalDataServiceContext.Provider.GetAlarm(Intent.Extras.GetInt("id"));

            timeTextView = FindViewById<TextView>(Resource.Id.timeTextView);

            // Dismiss
            FindViewById<TextView>(Resource.Id.dismissBtn).Click += delegate
            {
                Finish();
            };

            var snoozeBtn = FindViewById<TextView>(Resource.Id.snoozeBtn);
            // Snooze is enabled so setup snooze btn
            if (alarm.SnoozeProps.IsEnabled)
            {
                // Snooze
                FindViewById<TextView>(Resource.Id.snoozeBtn).Click += delegate
                {

                    Finish();
                };
            }
            // If snooze is disabled then remove it from the ui but not the visual tree completely.
            else
            {
                snoozeBtn.Visibility = ViewStates.Invisible;
            }
            

            UpdateClockOnScreen();

            // Every Second get the most updated time
            timeUpdater = new Timer(1000);
            timeUpdater.Elapsed += TimeUpdater_Elapsed;
            timeUpdater.Start();            
        }

        private void TimeUpdater_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateClockOnScreen();
        }

        /// <summary>
        ///     Update ui with current time
        /// </summary>
        void UpdateClockOnScreen()
        {
            MainThread.BeginInvokeOnMainThread(() => timeTextView.Text = (DateTime.Now.Hour < 13 ? DateTime.Now.Hour : (DateTime.Now.Hour - 12)) + $":{DateTime.Now.Minute}:{DateTime.Now.Second:00}");
        }

        /// <summary>
        ///     When resuming update the time to reflect the current time
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            UpdateClockOnScreen();
            timeUpdater.Start();
        }

        /// <summary>
        ///     Stop the timer when the app is going into the background
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            timeUpdater.Stop();
        }
    }
}