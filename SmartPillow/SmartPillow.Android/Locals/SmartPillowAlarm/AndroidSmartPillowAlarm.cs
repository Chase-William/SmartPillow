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

            // Informing our shared PCL that a specific alarm has been activated.
            SmartPillowAlarmManager.ActivateAlarmAudio(intent.GetIntExtra(AndroidSmartPillowAlarm.ID, 0));

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
        public const string ID = "id";

        // Should be initialized from inside MainActivity Init
        internal static Android.App.AlarmManager AlarmManager { get; private set; }
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
            // If the alarmId isn't inside the pendingIntents dict abort.
            if (!pendingIntents.ContainsKey(alarmId)) return;

            PendingIntent alarmIntent = pendingIntents[alarmId];
            pendingIntents.Remove(alarmId);
            AlarmManager.Cancel(alarmIntent);
        }

        public void SetAlarm(DateTime time, int alarmId)
        {
            // Calendar stuff           
            Calendar calendar = Calendar.GetInstance(ULocale.Us);
            calendar.Set(CalendarField.HourOfDay, 12);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 2);

            Intent baseIntent = new Intent(MainActivity, typeof(AlarmReceiver));
            baseIntent.PutExtra(MSG_EXTRA, "Alarm");
            baseIntent.PutExtra(TITLE_EXTRA, "This is a title");     
            baseIntent.PutExtra(ID, alarmId);     

            PendingIntent alarmIntent = PendingIntent.GetBroadcast(MainActivity, 0, baseIntent, 0);
            // Adding our
            pendingIntents.Add(alarmId, alarmIntent);

            AlarmManager.SetRepeating(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 5000, 5000, alarmIntent);
            //AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 5000, alarmIntent);
            Toast.MakeText(MainActivity, $"Alarm set for: 5 seconds...", ToastLength.Short).Show();
        }
    }
}