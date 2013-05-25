using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Notifications;
using SaaSOvation.Common.Events;

namespace SaaSOvation.IdentityAccess.Application
{
    public class NotificationApplicationService
    {
        public NotificationApplicationService(IEventStore eventStore, INotificationPublisher notificationPublisher)
        {
            this.eventStore = eventStore;
            this.notificationPublisher = notificationPublisher;
        }

        readonly IEventStore eventStore;
        readonly INotificationPublisher notificationPublisher;

        public NotificationLog GetCurrentNotificationLog()
        {
            return new NotificationLogFactory(this.eventStore)
                .CreateCurrentNotificationLog();
        }

        public NotificationLog GetNotificationLog(string notificationLogId)
        {
            return new NotificationLogFactory(this.eventStore)
                .CreateNotificationLog(new NotificationLogId(notificationLogId));
        }

        public void PublishNotifications()
        {
            this.notificationPublisher.PublishNotifications();
        }
    }
}
