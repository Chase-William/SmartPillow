using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SmartPillow.CustomAbstractions.Alarms;
using SmartPillow.Droid.Locals.Notifications;
using SmartPillowLib.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

// https://www.google.com/search?client=firefox-b-1-d&q=BroadcastReceiver
[assembly: Dependency(typeof(SmartPillow.Droid.Locals.SmartPillowAlarm.AndroidSPAlarmManager))]
namespace SmartPillow.Droid.Locals.SmartPillowAlarm
{
    public readonly struct AlarmKeys
    {
        public const string MSG_EXTRA = "message";
        public const string TITLE_EXTRA = "title";
        public const string ID = "id";
    }

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
            var message = intent.GetStringExtra(AlarmKeys.MSG_EXTRA);
            var title = intent.GetStringExtra(AlarmKeys.TITLE_EXTRA);
            int alarmId = intent.GetIntExtra(AlarmKeys.ID, 0);

            // Informing our shared PCL that a specific alarm has been activated.
            //SmartPillowAlarmManager.ActivateAlarmAudio(alarmId);

            //AndroidNotificationManager notificationManager = new AndroidNotificationManager();

            //notificationManager.ScheduleNotification(title, message

            
            
            Intent alarmActiveIntent = new Intent(context ,typeof(AlarmActivity));
            alarmActiveIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.FromBackground | ActivityFlags.BroughtToFront);  // Required when starting a activity outside of a Activity Context
            alarmActiveIntent.PutExtra(AlarmKeys.ID, alarmId);
            alarmActiveIntent.AddCategory(Intent.CategoryLauncher);

            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.AcquireCausesWakeup | WakeLockFlags.ScreenDim, "Smart Pillow Alarm");

            
            context.StartActivity(alarmActiveIntent);
            wakeLock.Acquire(30000); //wakelock for 30 sec```
        }
    }

    /// <summary>
    ///     Custom platform abstraction for our shared PCL to interact with for setting alarms n stuff.
    /// </summary>
    public class AndroidSPAlarmManager : ISmartPillowAlarmManager
    {
        // Should be initialized from inside MainActivity Init
        internal static Android.App.AlarmManager AlarmManager { get; private set; }
        internal static MainActivity MainActivity { get; private set; }

        /// <summary>
        ///     Stores our current on going pendingIntents.<br/>
        ///     @type - int, Alarm Id which acts as the key
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

        public static void CancelAlarm(Alarm alarm)
        {
            // If the alarmId isn't inside the pendingIntents dict abort.
            if (!activeAlarms.Contains(alarm.Id)) return;

            Intent alarmIntent = new Intent();
            alarmIntent.PutExtra(AlarmKeys.ID, activeAlarms.Single(x => x == alarm.Id));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(MainActivity, 0, alarmIntent, 0);
            activeAlarms.Remove(alarm.Id);
            AlarmManager.Cancel(pendingIntent);

            Toast.MakeText(MainActivity, "Alarm " + alarm.Name + " cancelled.", ToastLength.Short).Show();
        }

        public static void SetAlarm(TimeSpan timeOffset, Alarm alarm)
        {
            // Calendar stuff           
            Calendar calendar = Calendar.GetInstance(ULocale.Us);
            calendar.Set(CalendarField.HourOfDay, 12);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 2);

            Intent baseIntent = new Intent(MainActivity, typeof(AlarmReceiver));
            baseIntent.PutExtra(AlarmKeys.MSG_EXTRA, "Your alarm is set to go off at " + timeOffset.ToString());
            baseIntent.PutExtra(AlarmKeys.TITLE_EXTRA, "Alarm " + alarm.Name + " set.");     
            baseIntent.PutExtra(AlarmKeys.ID, alarm.Id);     

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