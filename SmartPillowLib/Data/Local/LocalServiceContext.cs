using SmartPillowLib.Models;
using LiteDB;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 
///     Local Db in use: https://www.litedb.org
/// 
/// </summary>
namespace SmartPillowLib.Data.Local
{
    public class LocalServiceContext : ILocalServiceContext
    {
        public static string DatabasePath;

        #region Collection Keys
        public const string TIMED_ALARM_COL_KEY = "timed_alarms";
        public const string SAFETY_ALARM_COL_KEY = "safety_alarms";
        #endregion               

        /// <summary>
        ///     Uses to create new instances of this class.
        /// </summary>
        public static LocalServiceContext Provider => new LocalServiceContext();

        private LocalServiceContext() { }

        public void InsertAlarm(Alarm alarm, string collection_key = TIMED_ALARM_COL_KEY)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(DatabasePath))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Alarm>(collection_key);

                // Insert new alarm document (Id will be auto-incremented)
                alarm.Id = col.Insert(alarm).AsInt32;
            }
        }

        public void UpdateAlarm(Alarm alarm, string collection_key = TIMED_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(collection_key);

                // Updates a specific alarm
                if(!col.Update(alarm))
                {
                    throw new Exception("Alarm not found inside collection.");
                }
            }
        }

        public IEnumerable<Alarm> GetAlarms(string collection_key = TIMED_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(collection_key);

                var test = col.FindAll().ToList();

                test.Add(new Alarm
                {
                    PillowProps = new DeviceProperties
                    {
                        IsBrightnessEnabled = true,
                        IsVibrationEnabled = true,
                        IsEnabled = true,
                        Brightness = 50,
                        Vibration = 50
                    },
                    PhoneProps = new DeviceProperties
                    {
                        IsBrightnessEnabled = true,
                        IsVibrationEnabled = true,
                        IsEnabled = true,
                        Brightness = 50,
                        Vibration = 50
                    },
                    SnoozeProps = new SnoozeProperties
                    {
                        IsEnabled = true,
                        Interval = 10,
                        Repeat = 3
                    },
                    Name = "My Favorite Alarm"
                });

                // Returns a IEnumerable of type Alarms
                return test;
            }
        }
    }
}
