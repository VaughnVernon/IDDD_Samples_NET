using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarUnshared : IDomainEvent
    {
        public CalendarUnshared(Tenant tenant, CalendarId calendarId, string name, CalendarSharer unsharedWith)
        {
            this.Tenant = tenant;
            this.CalendarId = calendarId;
            this.Name = name;
            this.UnsharedWith = unsharedWith;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public string Name { get; private set; }
        public CalendarSharer UnsharedWith { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
