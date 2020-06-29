using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Widget;
using Java.Lang;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Droid.Locals.Notifications;
using Xamarin.Forms;

// https://www.google.com/search?client=firefox-b-1-d&q=BroadcastReceiver
[assembly: Dependency(typeof(SmartPillow.Droid.Locals.SmartPillowAlarm.AndroidSmartPillowAlarm))]
namespace SmartPillow.Droid.Locals.SmartPillowAlarm
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {        
        /// <summary>
        ///     Called when an alarm is triggered.<br/>
        ///     @param - context, sender<br/>
        ///     @param - intent, info specifically about what was received itself
        /// </summary>
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra(AndroidSmartPillowAlarm.MSG_EXTRA);
            var title = intent.GetStringExtra(AndroidSmartPillowAlarm.TITLE_EXTRA);

            AndroidNotificationManager notificationManager = new AndroidNotificationManager();

            notificationManager.ScheduleNotification(title, message);
        }
    }

    /// <summary>
    ///     Custom platform abstraction for our shared PCL to interact with for setting alarms n stuff.
    /// </summary>
    public class AndroidSmartPillowAlarm : ISmartPillowAlarmManager
    {
        public const string MSG_EXTRA = "message";
        public const string TITLE_EXTRA = "title";

        // Should be initialized from inside MainActivity Init
        internal static AlarmManager AlarmManager { get; private set; }
        internal static MainActivity MainActivity { get; private set; }

        /// <summary>
        ///     Stores our current on going pendingIntents.<br/>
        ///     @type - int, Alarm Id which acts as the key<br/>
        ///     @type - PendingIntent, the pending intent
        /// </summary>
        private static readonly Dictionary<int, PendingIntent> pendingIntents = new Dictionary<int, PendingIntent>();

        /// <summary>
        ///     Initializes the LocalSmartPillowAlarm's static fields.
        /// </summary>
        internal static void Init(MainActivity mainActivity)
        {
            MainActivity = mainActivity;
            AlarmManager = (AlarmManager)mainActivity.GetSystemService(Context.AlarmService);
        }

        public void CancelAlarm(int alarmId)
        {
            PendingIntent alarmIntent = pendingIntents[alarmId];

            // Cancel here
            AlarmManager.Cancel(alarmIntent);
        }

        public void SetAlarm(DateTime time, int alarmId)
        {
            // Calendar stuff           
            Calendar calendar = Calendar.GetInstance(ULocale.Us);
            calendar.Set(CalendarField.HourOfDay, 12);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 2);

            Intent baseIntent = new Intent(MainActivity, typeof(AlarmReceiver));
            baseIntent.PutExtra(MSG_EXTRA, "Hello");
            baseIntent.PutExtra(TITLE_EXTRA, "This is a title");     

            PendingIntent alarmIntent = PendingIntent.GetBroadcast(MainActivity, 0, baseIntent, 0);
            // Adding our
            pendingIntents.Add(alarmId, alarmIntent);
                       
            AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 10000, alarmIntent);
            Toast.MakeText(MainActivity, $"Alarm set for: 10 seconds...", ToastLength.Short).Show();
        }
    }
}