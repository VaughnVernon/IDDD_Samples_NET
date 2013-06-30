using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class Calendar : EventSourcedRootEntity
    {
        public Calendar(Tenant tenant, CalendarId calendarId, string name, string description, Owner owner, IEnumerable<CalendarSharer> sharedWith = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenant, "The tenant must be provided.");
            AssertionConcern.AssertArgumentNotNull(calendarId, "The calendar id must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(name, "The name must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(description, "The description must be provided.");
            AssertionConcern.AssertArgumentNotNull(owner, "The owner must be provided.");
            Apply(new CalendarCreated(tenant, calendarId, name, description, owner, sharedWith));
        }

        void When(CalendarCreated e)
        {
            this.tenant = e.Tenant;
            this.calendarId = e.CalendarId;
            this.name = e.Name;
            this.description = e.Description;
            this.sharedWith = new HashSet<CalendarSharer>(e.SharedWith ?? Enumerable.Empty<CalendarSharer>());
        }

        public Calendar(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : base(eventStream, streamVersion)
        {
        }

        Tenant tenant;
        CalendarId calendarId;
        string name;
        string description;
        HashSet<CalendarSharer> sharedWith;

        public CalendarId CalendarId
        {
            get { return this.calendarId; }
        }

        public ReadOnlyCollection<CalendarSharer> AllSharedWith
        {
            get { return new ReadOnlyCollection<CalendarSharer>(this.sharedWith.ToArray()); }
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentNotEmpty(description, "The description must be provided.");
            Apply(new CalendarDescriptionChanged(this.tenant, this.calendarId, this.name, description));
        }

        void When(CalendarDescriptionChanged e)
        {
            this.description = e.Description;
        }

        public void Rename(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, "The name must be provided.");
            Apply(new CalendarRenamed(this.tenant, this.calendarId, name, this.description));
        }

        void When(CalendarRenamed e)
        {
            this.name = e.Name;
        }


        public CalendarEntry ScheduleCalendarEntry(
            CalendarIdentityService calendarIdService,
            string description,
            string location,
            Owner owner,
            DateRange timeSpan,
            Repetition repetition,
            Alarm alarm,
            IEnumerable<Participant> invitees = null)
        {
            return new CalendarEntry(
                this.tenant,
                this.calendarId,
                calendarIdService.GetNextCalendarEntryId(),
                description,
                location,
                owner,
                timeSpan,
                repetition,
                alarm,
                invitees);
        }

        public void ShareCalendarWith(CalendarSharer calendarSharer)
        {
            AssertionConcern.AssertArgumentNotNull(calendarSharer, "The calendar sharer must be provided.");
            if (!this.sharedWith.Contains(calendarSharer))
            {
                Apply(new CalendarShared(this.tenant, this.calendarId, this.name, calendarSharer));
            }
        }

        void When(CalendarShared e)
        {
            this.sharedWith.Add(e.SharedWith);
        }


        public void UnshareCalendarWith(CalendarSharer calendarSharer)
        {
            AssertionConcern.AssertArgumentNotNull(calendarSharer, "The calendar sharer must be provided.");
            if (this.sharedWith.Contains(calendarSharer))
            {
                Apply(new CalendarUnshared(this.tenant, this.calendarId, this.name, calendarSharer));
            }
        }

        void When(CalendarUnshared e)
        {
            this.sharedWith.Remove(e.UnsharedWith);
        }


        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.tenant;
            yield return this.calendarId;
        }
    }
}
