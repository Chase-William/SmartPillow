using SmartPillowLib.Models;
using System.Collections.Generic;

namespace SmartPillowLib.Data.Local
{
    interface ILocalDataServiceContext
    {
        #region Alarm Category
        /// <summary>
        ///     Inserts an alarm.
        /// </summary>
        /// <param name="alarm"> The alarm to be added </param>
        /// <param name="collection_key"> The key for a specific collection </param>
        void InsertAlarm(Alarm alarm, string collection_key = LocalDataServiceContext.TIMED_ALARM_COL_KEY);

        /// <summary>
        ///     Updates an existing alarm.
        /// </summary>
        /// <param name="alarm"> Alarm to be updated </param>
        /// <param name="collection_key"> The key for a specific collection </param>
        void UpdateAlarm(Alarm alarm, string collection_key = LocalDataServiceContext.TIMED_ALARM_COL_KEY);

        /// <summary>
        ///     Gets all the alarms in the collection that matches the collection_key param.
        /// </summary>
        /// <param name="collection_key"> The key for a specific collection </param>
        /// <returns> returns a alarm collection </returns>
        IEnumerable<Alarm> GetAlarms(string collection_key = LocalDataServiceContext.TIMED_ALARM_COL_KEY);

        void DeleteAllAlarms(string collection_key = LocalDataServiceContext.TIMED_ALARM_COL_KEY);

        void DeleteAlarm(int alarmId, string collection_key = LocalDataServiceContext.TIMED_ALARM_COL_KEY);
        #endregion

        //-----------------------------------------------------------------------------------------------------//

        #region Access token Category
        /// <summary>
        ///     Stores user login credential when an user is logged in
        /// </summary>
        /// <param name="collection_key"> The key for a specific collection </param>
        void InsertLoginAccessToken(AccessToken loginInfo, string collection_key = LocalDataServiceContext.LOGIN_COL_KEY);

        /// <summary>
        ///     Deletes user login credential when an user is logged off
        /// </summary>
        /// <param name="collection_key"> The key for a specific collection </param>
        void DeleteLoginAccessToken(string collection_key = LocalDataServiceContext.LOGIN_COL_KEY);

        /// <summary>
        ///     Gets user login credential when the app is opened
        /// </summary>
        /// <param name="collection_key"> The key for a specific collection </param>
        AccessToken GetLoginAccessToken(string collection_key = LocalDataServiceContext.LOGIN_COL_KEY);
        #endregion

        //-----------------------------------------------------------------------------------------------------//

        #region Alert Category
        // get all alerts when alert collection page is opened
        IEnumerable<Alert> GetAlerts(string collection_key = LocalDataServiceContext.SAFETY_ALARM_COL_KEY);

        // new alert
        void InsertAlert(Alert alert, string collection_key = LocalDataServiceContext.SAFETY_ALARM_COL_KEY);

        // update alert
        void UpdateAlert(Alert alert, string collection_key = LocalDataServiceContext.SAFETY_ALARM_COL_KEY);

        // deletes a specific alert
        void DeleteAlert(int alertId, string collection_key = LocalDataServiceContext.SAFETY_ALARM_COL_KEY);
        #endregion
    }
}