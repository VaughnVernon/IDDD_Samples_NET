using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntryRelocated : IDomainEvent
    {
        public CalendarEntryRelocated(
            Tenant tenant,
            CalendarId calendarId,
            CalendarEntryId calendarEntryId,
            string location)
        {
            this.Tenant = tenant;
            this.CalendarId = calendarId;
            this.CalendarEntryId = calendarEntryId;
            this.Location = location;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public CalendarEntryId CalendarEntryId { get; private set; }
        public string Location { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
