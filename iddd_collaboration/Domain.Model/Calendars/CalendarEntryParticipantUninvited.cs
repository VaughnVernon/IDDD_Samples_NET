using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntryParticipantUninvited : IDomainEvent
    {
        public CalendarEntryParticipantUninvited(
            Tenant tenant,
            CalendarId calendarId,
            CalendarEntryId calendarEntryId,
            Participant participant)
        {
            this.Tenant = tenant;
            this.CalendarId = calendarId;
            this.CalendarEntryId = calendarEntryId;
            this.Participant = participant;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public CalendarEntryId CalendarEntryId { get; private set; }
        public Participant Participant { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
