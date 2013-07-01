using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Collaboration.Application.Calendars.Data;

using SaaSOvation.Common.Port.Adapters.Persistence;

namespace SaaSOvation.Collaboration.Application.Calendars
{
    public class CalendarQueryService : AbstractQueryService
    {
        public CalendarQueryService(string providerName, string connectionString)
            : base(connectionString, providerName)
        {
        }

        public IList<CalendarData> GetAllCalendarsDataByTenant(string tenantId)
        {
            return QueryObjects<CalendarData>(
                    "select "
                    + "cal.calendar_id, cal.description, cal.name, cal.owner_email_address, "
                    + "cal.owner_identity, cal.owner_name, cal.tenant_id, "
                    + "sharer.calendar_id as o_sharers_calendar_id, "
                    + "sharer.participant_email_address as o_sharers_participant_email_address, "
                    + "sharer.participant_identity as o_sharers_participant_identity, "
                    + "sharer.participant_name as o_sharers_participant_name, "
                    + "sharer.tenant_id as o_sharers_tenant_id "
                    + "from tbl_vw_calendar as cal left outer join tbl_vw_calendar_sharer as sharer "
                    + " on cal.calendar_id = sharer.calendar_id "
                    + "where (cal.tenant_id = ?)",
                    new JoinOn("calendar_id", "o_sharers_calendar_id"),
                    tenantId);
        }

        public CalendarData GetCalendarDataById(string tenantId, string calendarId)
        {
            return QueryObject<CalendarData>(
                    "select "
                    + "cal.calendar_id, cal.description, cal.name, cal.owner_email_address, "
                    + "cal.owner_identity, cal.owner_name, cal.tenant_id, "
                    + "sharer.calendar_id as o_sharers_calendar_id, "
                    + "sharer.participant_email_address as o_sharers_participant_email_address, "
                    + "sharer.participant_identity as o_sharers_participant_identity, "
                    + "sharer.participant_name as o_sharers_participant_name, "
                    + "sharer.tenant_id as o_sharers_tenant_id "
                    + "from tbl_vw_calendar as cal left outer join tbl_vw_calendar_sharer as sharer "
                    + " on cal.calendar_id = sharer.calendar_id "
                    + "where (cal.tenant_id = ? and cal.calendar_id = ?)",
                    new JoinOn("calendar_id", "o_sharers_calendar_id"),
                    tenantId,
                    calendarId);
        }
    }
}
