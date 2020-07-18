using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartPillow.Droid
{
    [Activity(Label = "AlarmActivity", Theme = "@style/MainTheme", NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTask)]
    public class AlarmActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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

            if (this.Intent.Extras == null) return;
            
            int alarmId = Intent.Extras.GetInt("id");           
        }       
    }
}