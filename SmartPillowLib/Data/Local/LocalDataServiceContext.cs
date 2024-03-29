﻿using SmartPillowLib.Models;
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
    public class LocalDataServiceContext : ILocalDataServiceContext
    {
        public static string DatabasePath;

        #region Collection Keys
        public const string TIMED_ALARM_COL_KEY = "timed_alarms";
        public const string SAFETY_ALARM_COL_KEY = "safety_alarms";
        public const string LOGIN_COL_KEY = "login_saved";
        #endregion               

        /// <summary>
        ///     Uses to create new instances of this class.
        /// </summary>
        public static LocalDataServiceContext Provider => new LocalDataServiceContext();

        private LocalDataServiceContext() { }

        #region Alarm Category
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
                if (!col.Update(alarm))
                {
                    throw new Exception("Alarm not found inside collection.");
                }
            }
        }


        public Alarm GetAlarm(int alarmId)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(TIMED_ALARM_COL_KEY);
                return col.FindAll().Single(x => x.Id == alarmId);
            }
        }

        public IEnumerable<Alarm> GetAlarms(string collection_key = TIMED_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(collection_key);

                var collection = col.FindAll().ToList();

                // Returns a IEnumerable of type Alarms
                return collection;
            }
        }

        public void DeleteAllAlarms(string collection_key = TIMED_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(collection_key);
                col.DeleteAll();
            }
        }

        public void DeleteAlarm(int alarmId, string collection_key = TIMED_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alarm>(collection_key);
                col.Delete(alarmId);
            }
        }
        #endregion

        #region Access token category
        public void InsertLoginAccessToken(AccessToken loginInfo, string collection_key = LOGIN_COL_KEY)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<AccessToken>(collection_key);
                loginInfo.Id = col.Insert(loginInfo).AsInt32;
            }
        }

        public AccessToken GetLoginAccessToken(string collection_key = LOGIN_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<AccessToken>(collection_key);
                var loginInfo = col.FindById(1);
                return loginInfo;
            }
        }

        public void DeleteLoginAccessToken(string collection_key = LOGIN_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<AccessToken>(collection_key);
                col.DeleteAll();
            }
        }
        #endregion

        #region Alert Cateogry
        public IEnumerable<Alert> GetAlerts(string collection_key = SAFETY_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alert>(collection_key);

                var collection = col.FindAll().ToList();

                return collection;
            }
        }

        public void InsertAlert(Alert alert, string collection_key = SAFETY_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alert>(collection_key);

                alert.Id = col.Insert(alert).AsInt32;
            }
        }

        public void UpdateAlert(Alert alert, string collection_key = SAFETY_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alert>(collection_key);

                // Updates a specific alert
                if (!col.Update(alert))
                {
                    //throw new Exception("Alarm not found inside collection.");
                }
            }
        }

        public void DeleteAlert(int alertId, string collection_key = SAFETY_ALARM_COL_KEY)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var col = db.GetCollection<Alert>(collection_key);
                col.Delete(alertId);
            }
        }
        #endregion
    }
}
