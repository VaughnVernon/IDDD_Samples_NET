using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Common.Notifications
{
    public class NotificationLogInfo
    {
        public NotificationLogInfo(NotificationLogId notificationLogId, long totalLogged)
        {
            this.notificationLogId = notificationLogId;
            this.totalLogged = totalLogged;
        }

        readonly NotificationLogId notificationLogId;

        public NotificationLogId NotificationLogId
        {
            get { return notificationLogId; }
        }

        readonly long totalLogged;

        public long TotalLogged
        {
            get { return totalLogged; }
        } 

    }
}
