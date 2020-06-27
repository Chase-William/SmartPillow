using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPillow.CustomAbstractions.LocationNotification
{
    /// <summary>
    ///     This interface is an abstraction that will have different implementations according to the platform.
    ///     This is used to send and receive notifications.
    /// </summary>
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;

        void Initialize();

        int ScheduleNotification(string title, string message);

        void ReceiveNotification(string title, string message);
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
