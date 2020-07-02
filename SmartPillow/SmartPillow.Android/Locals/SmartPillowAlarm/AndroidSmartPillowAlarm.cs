using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Widget;
using Java.Lang;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Droid.Locals.Notifications;
using SmartPillowLib.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
        private static readonly List<int> activeAlarms = new List<int>();

        /// <summary>
        ///     Initializes the LocalSmartPillowAlarm's static fields.
        /// </summary>
        internal static void Init(MainActivity mainActivity)
        {
            MainActivity = mainActivity;
            AlarmManager = (AlarmManager)mainActivity.GetSystemService(Context.AlarmService);

            // Adding all active alarms to the activeAlarm collection
            activeAlarms.AddRange(SmartPillowLib.Data.Local.LocalDataServiceContext.Provider.GetAlarms().Where(x => x.IsAlarmEnabled).Select(x => x.Id));
        }

        public void CancelAlarm(Alarm alarm)
        {
            // If the alarmId isn't inside the pendingIntents dict abort.
            if (!activeAlarms.Contains(alarm.Id)) return;

            Intent alarmIntent = new Intent();
            alarmIntent.PutExtra(ID, activeAlarms.Single(x => x == alarm.Id));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(MainActivity, 0, alarmIntent, 0);
            activeAlarms.Remove(alarm.Id);
            AlarmManager.Cancel(pendingIntent);
        }

        public void SetAlarm(TimeSpan timeOffset, Alarm alarm)
        {
            // Calendar stuff           
            Calendar calendar = Calendar.GetInstance(ULocale.Us);
            calendar.Set(CalendarField.HourOfDay, 12);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 2);

            Intent baseIntent = new Intent(MainActivity, typeof(AlarmReceiver));
            baseIntent.PutExtra(MSG_EXTRA, "Your alarm is set to go off at " + timeOffset.ToString());
            baseIntent.PutExtra(TITLE_EXTRA, "Alarm " + alarm.Name + " set.");     
            baseIntent.PutExtra(ID, alarm.Id);     

            PendingIntent alarmIntent = PendingIntent.GetBroadcast(MainActivity, alarm.Id, baseIntent, 0);
            // Adding our alarm to the active list
            activeAlarms.Add(alarm.Id);

            //AlarmManager.SetRepeating(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 5000, 5000, alarmIntent);
            //AlarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + 5000, alarmIntent);            

            // Getting the remaining time from the current time and the alarm's time and the difference between them.
            var diff = timeOffset.Subtract(DateTime.Now.ToLocalTime().TimeOfDay);
            var diffInMili = diff.TotalMilliseconds;

            // If our alarm's time to fire has already passed add a day to it so it doesn't go off when turned on.
            // Its important we don't change the original timeOffset to this new time.
            if (DateTime.Now.ToLocalTime().TimeOfDay > timeOffset)
            {
                diffInMili = diff.TotalMilliseconds + TimeSpan.FromDays(1).TotalMilliseconds;
            }

            AlarmManager.SetExact(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + (long)diffInMili, alarmIntent);

            //var diff = TimeSpan.FromMilliseconds(diffInMili);

            Toast.MakeText(MainActivity, "Alarm " + alarm.Name + " is active.", ToastLength.Short).Show();

        }
    }
}