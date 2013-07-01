using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Collaboration.Domain.Model.Calendars;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;
using SaaSOvation.Collaboration.Domain.Model.Tenants;

using SaaSOvation.Collaboration.Application.Calendars.Data;

namespace SaaSOvation.Collaboration.Application.Calendars
{
    public class CalendarApplicationService
    {
        public CalendarApplicationService(ICalendarRepository calendarRepository, ICalendarEntryRepository calendarEntryRepository, CalendarIdentityService calendarIdentityService, ICollaboratorService collaboratorService)
        {
            this.calendarRepository = calendarRepository;
            this.calendarEntryRepository = calendarEntryRepository;
            this.calendarIdentityService = calendarIdentityService;
            this.collaboratorService = collaboratorService;
        }

        readonly ICalendarRepository calendarRepository;
        readonly ICalendarEntryRepository calendarEntryRepository;
        readonly CalendarIdentityService calendarIdentityService;
        readonly ICollaboratorService collaboratorService;

        public void ChangeCalendarDescription(string tenantId, string calendarId, string description)
        {
            var calendar = this.calendarRepository.Get(new Tenant(tenantId), new CalendarId(calendarId));

            calendar.ChangeDescription(description);

            this.calendarRepository.Save(calendar);
        }

        public void CreateCalendar(string tenantId, string name, string description, string ownerId, ISet<string> participantsToShareWith, ICalendarCommandResult calendarCommandResult)
        {
            var tenant = new Tenant(tenantId);
            var owner = this.collaboratorService.GetOwnerFrom(tenant, ownerId);
            var sharers = GetSharersFrom(tenant, participantsToShareWith);

            var calendar = new Calendar(tenant, this.calendarRepository.GetNextIdentity(), name, description, owner, sharers);

            this.calendarRepository.Save(calendar);

            calendarCommandResult.SetResultingCalendarId(calendar.CalendarId.Id);
        }

        public void RenameCalendar(string tenantId, string calendarId, string name)
        {
            var calendar = this.calendarRepository.Get(new Tenant(tenantId), new CalendarId(calendarId));

            calendar.Rename(name);

            this.calendarRepository.Save(calendar);
        }

        public void ScheduleCalendarEntry(string tenantId, string calendarId, string description, string location, string ownerId, DateTime timeSpanBegins, DateTime timeSpanEnds,
            string repeatType, DateTime repeatEndsOn, string alarmType, int alarmUnits, ISet<string> participantsToInvite, ICalendarCommandResult calendarCommandResult)
        {
            var tenant = new Tenant(tenantId);

            var calendar = this.calendarRepository.Get(tenant, new CalendarId(calendarId));

            var calendarEntry = calendar.ScheduleCalendarEntry(
                this.calendarIdentityService,
                description,
                location,
                this.collaboratorService.GetOwnerFrom(tenant, ownerId),
                new DateRange(timeSpanBegins, timeSpanEnds),
                new Repetition((RepeatType)Enum.Parse(typeof(RepeatType), repeatType), repeatEndsOn),
                new Alarm((AlarmUnitsType)Enum.Parse(typeof(AlarmUnitsType), alarmType), alarmUnits),
                GetInviteesFrom(tenant, participantsToInvite));

            this.calendarEntryRepository.Save(calendarEntry);

            calendarCommandResult.SetResultingCalendarId(calendar.CalendarId.Id);
            calendarCommandResult.SetResultingCalendarEntryId(calendarEntry.CalendarEntryId.Id);
        }

        public void ShareCalendarWith(string tenantId, string calendarId, ISet<string> participantsToShareWith)
        {
            var tenant = new Tenant(tenantId);
            var calendar = this.calendarRepository.Get(tenant, new CalendarId(calendarId));

            foreach (var sharer in GetSharersFrom(tenant, participantsToShareWith))
            {
                calendar.ShareCalendarWith(sharer);
            }

            this.calendarRepository.Save(calendar);
        }

        public void UnshareCalendarWith(string tenantId, string calendarId, ISet<string> participantsToShareWith)
        {
            var tenant = new Tenant(tenantId);
            var calendar = this.calendarRepository.Get(tenant, new CalendarId(calendarId));

            foreach (var sharer in GetSharersFrom(tenant, participantsToShareWith))
            {
                calendar.UnshareCalendarWith(sharer);
            }

            this.calendarRepository.Save(calendar);
        }

        ISet<Participant> GetInviteesFrom(Tenant tenant, ISet<string> participantsToInvite)
        {
            var invitees = new HashSet<Participant>();
            foreach (string participatnId in participantsToInvite)
            {
                var participant = this.collaboratorService.GetParticipantFrom(tenant, participatnId);
                invitees.Add(participant);
            }
            return invitees;
        }

        ISet<CalendarSharer> GetSharersFrom(Tenant tenant, ISet<string> participantsToShareWith)
        {
            var sharers = new HashSet<CalendarSharer>();
            foreach (var participatnId in participantsToShareWith)
            {
                var participant = this.collaboratorService.GetParticipantFrom(tenant, participatnId);
                sharers.Add(new CalendarSharer(participant));
            }
            return sharers;
        }
    }
}
