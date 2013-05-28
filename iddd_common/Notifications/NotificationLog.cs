using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SaaSOvation.Common.Notifications
{
    public class NotificationLog
    {
        public NotificationLog(string notificationLogId, string nextNotificationLogId, string previousNotificationLogId, 
            IEnumerable<Notification> notifications, bool isArchived)
        {            
            this.notificationLogId = notificationLogId;
            this.nextNotificationLogId = nextNotificationLogId;
            this.previousNotificationLogId = previousNotificationLogId;
            this.notifications = new ReadOnlyCollection<Notification>(notifications.ToArray());
            this.isArchived = isArchived;
        }

        readonly string notificationLogId;
        readonly string nextNotificationLogId;
        readonly string previousNotificationLogId;
        readonly ReadOnlyCollection<Notification> notifications; 
        readonly bool isArchived;

        public bool IsArchived
        {
            get { return this.isArchived; }
        }

        public ReadOnlyCollection<Notification> Notifications
        {
            get { return this.notifications; }
        }

        public int TotalNotifications
        {
            get { return this.notifications.Count; }
        }

        public NotificationLogId DecodedNotificationLogId
        {
            get { return new NotificationLogId(this.notificationLogId); }
        }

        public string NotificationLogId
        {
            get { return this.notificationLogId; }
        }

        public NotificationLogId DecodedNextNotificationLogId
        {
            get { return new NotificationLogId(this.nextNotificationLogId); }
        }

        public string NextNotificationLogId
        {
            get { return this.nextNotificationLogId; }
        }

        public bool HasNextNotificationLog
        {
            get { return this.nextNotificationLogId != null; }
        }

        public NotificationLogId DecodedPreviousNotificationLogId
        {
            get { return new NotificationLogId(this.previousNotificationLogId); }
        }

        public string PreviousNotificationLogId
        {
            get { return this.previousNotificationLogId; }
        }

        public bool HasPreviousNotificationLog
        {
            get { return this.previousNotificationLogId != null; }
        }
    }
}
