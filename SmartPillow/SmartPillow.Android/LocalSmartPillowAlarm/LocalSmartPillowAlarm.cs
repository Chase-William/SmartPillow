using System;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Widget;
using Java.Lang;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Droid.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(SmartPillow.Droid.LocalSmartPillowAlarm.LocalSmartPillowAlarm))]
namespace SmartPillow.Droid.LocalSmartPillowAlarm
{
    /// <summary>
    ///     Custom platform abstraction for our shared PCL to interact with for setting alarms n stuff.
    /// </summary>
    public class LocalSmartPillowAlarm : ISmartPillowAlarmManager
    {
        // Should be initialized from inside MainActivity Init
        internal static AlarmManager AlarmManager { get; private set; }
        internal static MainActivity MainActivity { get; private set; }

        /// <summary>
        ///     Initializes the LocalSmartPillowAlarm's static fields.
        /// </summary>
        internal static void Init(MainActivity mainActivity)
        {
            MainActivity = mainActivity;
            AlarmManager = (AlarmManager)mainActivity.GetSystemService(Context.AlarmService);
        }

        public void SetAlarm()
        {
            // Calendar stuff           
            Calendar calendar = Calendar.GetInstance(ULocale.Us);
            calendar.Set(CalendarField.HourOfDay, 12);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 2);

            // Creating a pendingIntent
            PendingIntent alarmIntent;
            Intent baseIntent = new Intent(MainActivity, typeof(DataBroadcastReceiver));
            alarmIntent = PendingIntent.GetBroadcast(MainActivity, 0, baseIntent, 0);

            AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 20000, alarmIntent);
            Toast.MakeText(MainActivity, $"Alarm set for: 20 seconds...", ToastLength.Short).Show();
        }
    }
}