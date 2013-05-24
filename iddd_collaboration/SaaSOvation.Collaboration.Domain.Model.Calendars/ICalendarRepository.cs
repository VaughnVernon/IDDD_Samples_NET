using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public interface ICalendarRepository
    {
        Calendar Get(Tenants.Tenant tenant, CalendarId calendarId);
        CalendarId GetNextIdentity();
        void Save(Calendar calendar);
    }
}
