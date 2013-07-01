using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntry : EventSourcedRootEntity
    {
        public CalendarEntry(
            Tenant tenant,
            CalendarId calendarId,
            CalendarEntryId calendarEntryId,
            string description,
            string location,
            Owner owner,
            DateRange timeSpan,
            Repetition repetition,
            Alarm alarm,
            IEnumerable<Participant> invitees = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenant, "The tenant must be provided.");
            AssertionConcern.AssertArgumentNotNull(calendarId, "The calendar id must be provided.");
            AssertionConcern.AssertArgumentNotNull(calendarEntryId, "The calendar entry id must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(description, "The description must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(location, "The location must be provided.");
            AssertionConcern.AssertArgumentNotNull(owner, "The owner must be provided.");
            AssertionConcern.AssertArgumentNotNull(timeSpan, "The time span must be provided.");
            AssertionConcern.AssertArgumentNotNull(repetition, "The repetition must be provided.");
            AssertionConcern.AssertArgumentNotNull(alarm, "The alarm must be provided.");

            if (repetition.Repeats == RepeatType.DoesNotRepeat)
                repetition = Repetition.DoesNotRepeat(timeSpan.Ends);

            AssertTimeSpans(repetition, timeSpan);

            Apply(new CalendarEntryScheduled(tenant, calendarId, calendarEntryId, description, location, owner, timeSpan, repetition, alarm, invitees));
        }        

        Tenant tenant;
        CalendarId calendarId;
        CalendarEntryId calendarEntryId;
        string description;
        string location;
        Owner owner;
        DateRange timeSpan;
        Repetition repetition;
        Alarm alarm;
        HashSet<Participant> invitees;

        public CalendarEntryId CalendarEntryId
        {
            get { return this.CalendarEntryId; }
        }

        void AssertTimeSpans(Repetition repetition, DateRange timeSpan)
        {
            if (repetition.Repeats == RepeatType.DoesNotRepeat)
            {
                AssertionConcern.AssertArgumentEquals(repetition.Ends, timeSpan.Ends, "Non-repeating entry must end with time span end.");
            }
            else
            {
                AssertionConcern.AssertArgumentFalse(timeSpan.Ends > repetition.Ends, "Time span must end when or before repetition ends.");
            }
        }

        void When(CalendarEntryScheduled e)
        {
            this.tenant = e.Tenant;
            this.calendarId = e.CalendarId;
            this.calendarEntryId = e.CalendarEntryId;
            this.description = e.Description;
            this.location = e.Location;
            this.owner = e.Owner;
            this.timeSpan = e.TimeSpan;
            this.repetition = e.Repetition;
            this.alarm = e.Alarm;
            this.invitees = new HashSet<Participant>(e.Invitees ?? Enumerable.Empty<Participant>());
        }


        public void ChangeDescription(string description)
        {
            if (description == null)
            {
                // TODO: consider
            }

            description = description.Trim();

            if (!string.IsNullOrEmpty(description) && !this.description.Equals(description))
            {
                Apply(new CalendarEntryDescriptionChanged(this.tenant, this.calendarId, this.calendarEntryId, description));
            }
        }

        void When(CalendarEntryDescriptionChanged e)
        {
            this.description = e.Description;
        }
        

        public void Invite(Participant participant)
        {
            AssertionConcern.AssertArgumentNotNull(participant, "The participant must be provided.");
            if (!this.invitees.Contains(participant))
            {
                Apply(new CalendarEntryParticipantInvited(this.tenant, this.calendarId, this.calendarEntryId, participant));
            }
        }

        void When(CalendarEntryParticipantInvited e)
        {
            this.invitees.Add(e.Participant);
        }


        public void Relocate(string location)
        {
            if (location == null)
            {
                // TODO: consider
            }

            location = location.Trim();
            if (!string.IsNullOrEmpty(location) && !this.location.Equals(location))
            {
                Apply(new CalendarEntryRelocated(this.tenant, this.calendarId, this.calendarEntryId, location));
            }
        }

        void When(CalendarEntryRelocated e)
        {
            this.location = e.Location;
        }


        public void Reschedule(string description, string location, DateRange timeSpan, Repetition repetition, Alarm alarm)
        {
            AssertionConcern.AssertArgumentNotNull(timeSpan, "The time span must be provided.");
            AssertionConcern.AssertArgumentNotNull(repetition, "The repetition must be provided.");
            AssertionConcern.AssertArgumentNotNull(alarm, "The alarm must be provided.");

            if (repetition.Repeats == RepeatType.DoesNotRepeat)
                repetition = Repetition.DoesNotRepeat(timeSpan.Ends);

            AssertTimeSpans(repetition, timeSpan);

            ChangeDescription(description);
            Relocate(location);

            Apply(new CalendarEntryRescheduled(this.tenant, this.calendarId, this.calendarEntryId, timeSpan, repetition, alarm));
        }

        void When(CalendarEntryRescheduled e)
        {
            this.timeSpan = e.TimeSpan;
            this.repetition = e.Repetition;
            this.alarm = e.Alarm;
        }


        public void Uninvite(Participant participant)
        {
            AssertionConcern.AssertArgumentNotNull(participant, "The participant must be provided.");

            if (this.invitees.Contains(participant))
            {
                Apply(new CalendarEntryParticipantUninvited(this.tenant, this.calendarId, this.calendarEntryId, participant));
            }
        }

        void When(CalendarEntryParticipantUninvited e)
        {
            this.invitees.Remove(e.Participant);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.tenant;
            yield return this.calendarId;
            yield return this.calendarEntryId;
        }
    }
}
