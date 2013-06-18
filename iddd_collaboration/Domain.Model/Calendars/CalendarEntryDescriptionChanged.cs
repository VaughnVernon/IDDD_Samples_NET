using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntryDescriptionChanged : IDomainEvent
    {
        public CalendarEntryDescriptionChanged(
            Tenant tenant,
            CalendarId calendarId,
            CalendarEntryId calendarEntryId,
            string description)
        {
            this.Tenant = tenant;
            this.CalendarId = calendarId;
            this.CalendarEntryId = calendarEntryId;
            this.Description = description;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public CalendarEntryId CalendarEntryId { get; private set; }
        public string Description { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
