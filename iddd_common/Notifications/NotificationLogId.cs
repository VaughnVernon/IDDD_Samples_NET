using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Common.Notifications
{
    public class NotificationLogId : Domain.Model.ValueObject
    {
        public static string GetEncoded(NotificationLogId notificationLogId)
        {
            if (notificationLogId != null) return notificationLogId.Encoded;
            else return null;
        }

        public static NotificationLogId First(int notificationsPerLog)
        {
            return new NotificationLogId(0, 0).Next(notificationsPerLog);
        }

        public NotificationLogId(long lowId, long highId)
        {
            this.Low = lowId;
            this.High = highId;
        }

        public NotificationLogId(string notificationlogId)
        {
            var pts = notificationlogId.Split(',');
            this.Low = long.Parse(pts[0]);
            this.High = long.Parse(pts[1]);
        }

        public long Low { get; private set; }
        public long High { get; private set; }

        public string Encoded
        {
            get { return this.Low + "," + this.High; }
        }

        public NotificationLogId Next(int notificationsPerLog)
        {
            var nextLow = this.High + 1;
            var nextHigh = nextLow + notificationsPerLog;
            var next = new NotificationLogId(nextLow, nextHigh);
            if (Equals(next))
                next = null;
            return next;
        }

        public NotificationLogId Previous(int notificationsPerLog)
        {
            var previousLow = Math.Max(this.Low - notificationsPerLog, 1);
            var previousHigh = previousLow + notificationsPerLog - 1;
            var previous = new NotificationLogId(previousLow, previousHigh);
            if (Equals(previous))
                previous = null;
            return previous;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Low;
            yield return this.High;
        }
    }
}
