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
    public class CalendarEntryApplicationService
    {
        public CalendarEntryApplicationService(ICalendarEntryRepository calendarEntryRepository, ICollaboratorService collaboratorService)
        {
            this.calendarEntryRepository = calendarEntryRepository;
            this.collaboratorService = collaboratorService;
        }

        readonly ICalendarEntryRepository calendarEntryRepository;
        readonly ICollaboratorService collaboratorService;

        public void ChangeCalendarEntryDescription(string tenantId, string calendarEntryId, string description)
        {
            var calendarEntry = this.calendarEntryRepository.Get(new Tenant(tenantId), new CalendarEntryId(calendarEntryId));

            calendarEntry.ChangeDescription(description);

            this.calendarEntryRepository.Save(calendarEntry);
        }

        public void InviteCalendarEntryParticipant(string tenantId, string calendarEntryId, ISet<string> participantsToInvite)
        {
            var tenant = new Tenant(tenantId);
            var calendarEntry = this.calendarEntryRepository.Get(tenant, new CalendarEntryId(calendarEntryId));

            foreach (var participant in GetInviteesFrom(tenant, participantsToInvite))
            {
                calendarEntry.Invite(participant);
            }

            this.calendarEntryRepository.Save(calendarEntry);
        }

        public void RelocateCalendarEntry(string tenantId, string calendarEntryId, string location)
        {
            var calendarEntry = this.calendarEntryRepository.Get(new Tenant(tenantId), new CalendarEntryId(calendarEntryId));

            calendarEntry.Relocate(location);

            this.calendarEntryRepository.Save(calendarEntry);
        }

        public void RescheduleCalendarEntry(string tenantId, string calendarEntryId, string description, string location, DateTime timeSpanBegins, DateTime timeSpanEnds,
            string repeatType, DateTime repeatEndsOn, string alarmType, int alarmUnits)
        {
            var calendarEntry = this.calendarEntryRepository.Get(new Tenant(tenantId), new CalendarEntryId(calendarEntryId));

            calendarEntry.Reschedule(
                description, 
                location, 
                new DateRange(timeSpanBegins, timeSpanEnds), 
                new Repetition((RepeatType)Enum.Parse(typeof(RepeatType), repeatType), repeatEndsOn),
                new Alarm((AlarmUnitsType)Enum.Parse(typeof(AlarmUnitsType), alarmType), alarmUnits));

            this.calendarEntryRepository.Save(calendarEntry);
        }

        public void UninviteCalendarEntryParticipant(string tenantId, string calendarEntryId, ISet<string> participantsToUninvite)
        {
            var tenant = new Tenant(tenantId);
            var calendarEntry = this.calendarEntryRepository.Get(tenant, new CalendarEntryId(calendarEntryId));

            foreach (var participant in GetInviteesFrom(tenant, participantsToUninvite))
            {
                calendarEntry.Uninvite(participant);
            }

            this.calendarEntryRepository.Save(calendarEntry);
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
    }
}
