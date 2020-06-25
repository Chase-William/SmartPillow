﻿using SmartPillowLib.Models;
using System.Collections.Generic;

namespace SmartPillowLib.Data.Local
{
    interface ILocalServiceContext
    {
        /// <summary>
        ///     Inserts an alarm.
        /// </summary>
        /// <param name="alarm"> The alarm to be added </param>
        /// <param name="collection_key"> The key for a specific collection </param>
        void InsertAlarm(Alarm alarm, string collection_key = LocalServiceContext.TIMED_ALARM_COL_KEY);

        /// <summary>
        ///     Updates an existing alarm.
        /// </summary>
        /// <param name="alarm"> Alarm to be updated </param>
        /// <param name="collection_key"> The key for a specific collection </param>
        void UpdateAlarm(Alarm alarm, string collection_key = LocalServiceContext.TIMED_ALARM_COL_KEY);

        /// <summary>
        ///     Gets all the alarms in the collection that matches the collection_key param.
        /// </summary>
        /// <param name="collection_key"> The key for a specific collection </param>
        /// <returns> returns a alarm collection </returns>
        IEnumerable<Alarm> GetAlarms(string collection_key = LocalServiceContext.TIMED_ALARM_COL_KEY);
    }
}