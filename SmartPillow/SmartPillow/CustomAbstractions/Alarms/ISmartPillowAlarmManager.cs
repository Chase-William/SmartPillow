using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillow.CustomAbstractions.Alarms
{
    /// <summary>
    ///     Abstraction interface for setting alarms on the respective platform.
    /// </summary>
    public interface ISmartPillowAlarmManager
    {
        /// <summary>
        ///     Sets a alarm at a specific time.<br/>
        ///     @param - time, the alarm will trigger at<br/>
        ///     @param - alarmId, id of the alarm which this call is for
        /// </summary>
        void SetAlarm(DateTime time, int alarmId);
        /// <summary>
        ///     Cancels a set alarm.<br/>
        ///     @param - alarmId, id of alarm to cancel 
        /// </summary>
        void CancelAlarm(int alarmId);
    }
}
