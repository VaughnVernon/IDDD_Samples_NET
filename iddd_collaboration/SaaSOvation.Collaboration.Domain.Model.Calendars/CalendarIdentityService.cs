using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarIdentityService
    {
        public CalendarIdentityService(ICalendarRepository calendarRepository, ICalendarEntryRepository calendarEntryRepository)
        {
            this.calendarRepository = calendarRepository;
            this.calendarEntryRepository = calendarEntryRepository;
        }

        readonly ICalendarRepository calendarRepository;
        readonly ICalendarEntryRepository calendarEntryRepository;

        public CalendarId GetNextCalendarId()
        {
            return this.calendarRepository.GetNextIdentity();
        }

        public CalendarEntryId GetNextCalendarEntryId()
        {
            return this.calendarEntryRepository.GetNextIdentity();
        }
    }
}
