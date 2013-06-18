using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Notifications;

namespace SaaSOvation.AgilePM.Application.Notifications
{
    public class NotificationApplicationService
    {
        public NotificationApplicationService(INotificationPublisher notificationPublisher)
        {
            this.notificationPublisher = notificationPublisher;
        }

        readonly INotificationPublisher notificationPublisher;

        public void PublishNotifications()
        {
            ApplicationServiceLifeCycle.Begin(false);
            try
            {
                this.notificationPublisher.PublishNotifications();
                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }
    }
}
