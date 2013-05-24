using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntryRescheduled : IDomainEvent
    {
        public CalendarEntryRescheduled(
            Tenant tenant,
            CalendarId calendarId,
            CalendarEntryId calendarEntryId,
            DateRange timeSpan,
            Repetition repetition,
            Alarm alarm)
        {
            this.Tenant = tenant;
            this.CalendarId = calendarId;
            this.CalendarEntryId = calendarEntryId;
            this.TimeSpan = timeSpan;
            this.Repetition = repetition;
            this.Alarm = alarm;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public CalendarEntryId CalendarEntryId { get; private set; }
        public DateRange TimeSpan { get; private set; }
        public Repetition Repetition { get; private set; }
        public Alarm Alarm { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
