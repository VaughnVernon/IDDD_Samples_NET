using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Collaboration.Application.Calendars.Data;

using SaaSOvation.Common.Port.Adapters.Persistence;

namespace SaaSOvation.Collaboration.Application.Calendars
{
    public class CalendarEntryQueryService : AbstractQueryService
    {
        public CalendarEntryQueryService(string providerName, string connectionString)
            : base(connectionString, providerName)
        {
        }

        public CalendarEntryData GetCalendarEntryDataById(string tenantId, string calendarEntryId)
        {
            return QueryObject<CalendarEntryData>(
                    "select "
                    + "entry.calendar_entry_id, entry.alarm_alarm_units, entry.alarm_alarm_units_type, "
                    + "entry.calendar_id, entry.description, entry.location, "
                    + "entry.owner_email_address, entry.owner_identity, entry.owner_name, "
                    + "entry.repetition_ends, entry.repetition_type, entry.tenant_id, "
                    + "entry.time_span_begins, entry.time_span_ends, "
                    + "invitee.calendar_entry_id as o_invitees_calendar_entry_id, "
                    + "invitee.participant_email_address as o_invitees_participant_email_address, "
                    + "invitee.participant_identity as o_invitees_participant_identity, "
                    + "invitee.participant_name as o_invitees_participant_name, "
                    + "invitee.tenant_id as o_invitees_tenant_id "
                    + "from tbl_vw_calendar_entry as entry left outer join tbl_vw_calendar_entry_invitee as invitee "
                    + " on entry.calendar_entry_id = invitee.calendar_entry_id "
                    + "where entry.tenant_id = ? and entry.calendar_entry_id = ?",
                    new JoinOn("calendar_entry_id", "o_invitees_calendar_entry_id"),
                    tenantId,
                    calendarEntryId);
        }

        public IList<CalendarEntryData> GetCalendarEntryDataByCalendarId(string tenantId, string calendarId)
        {
            return QueryObjects<CalendarEntryData>(
                    "select "
                    + "entry.calendar_entry_id, entry.alarm_alarm_units, entry.alarm_alarm_units_type, "
                    + "entry.calendar_id, entry.description, entry.location, "
                    + "entry.owner_email_address, entry.owner_identity, entry.owner_name, "
                    + "entry.repetition_ends, entry.repetition_type, entry.tenant_id, "
                    + "entry.time_span_begins, entry.time_span_ends, "
                    + "invitee.calendar_entry_id as o_invitees_calendar_entry_id, "
                    + "invitee.participant_email_address as o_invitees_participant_email_address, "
                    + "invitee.participant_identity as o_invitees_participant_identity, "
                    + "invitee.participant_name as o_invitees_participant_name, "
                    + "invitee.tenant_id as o_invitees_tenant_id "
                    + "from tbl_vw_calendar_entry as entry left outer join tbl_vw_calendar_entry_invitee as invitee "
                    + " on entry.calendar_entry_id = invitee.calendar_entry_id "
                    + "where entry.tenant_id = ? and entry.calendar_id = ?",
                    new JoinOn("calendar_entry_id", "o_invitees_calendar_entry_id"),
                    tenantId,
                    calendarId);
        }

        public IList<CalendarEntryData> GetCalendarEntriesSpanning(string tenantId, string calendarId, DateTime beginsOn, DateTime endsOn)
        {
            return QueryObjects<CalendarEntryData>(
                    "select "
                    + "entry.calendar_entry_id, entry.alarm_alarm_units, entry.alarm_alarm_units_type, "
                    + "entry.calendar_id, entry.description, entry.location, "
                    + "entry.owner_email_address, entry.owner_identity, entry.owner_name, "
                    + "entry.repetition_ends, entry.repetition_type, entry.tenant_id, "
                    + "entry.time_span_begins, entry.time_span_ends, "
                    + "invitee.calendar_entry_id as o_invitees_calendar_entry_id, "
                    + "invitee.participant_email_address as o_invitees_participant_email_address, "
                    + "invitee.participant_identity as o_invitees_participant_identity, "
                    + "invitee.participant_name as o_invitees_participant_name, "
                    + "invitee.tenant_id as o_invitees_tenant_id "
                    + "from tbl_vw_calendar_entry as entry left outer join tbl_vw_calendar_entry_invitee as invitee "
                    + " on entry.calendar_entry_id = invitee.calendar_entry_id "
                    + "where entry.tenant_id = ? and entry.calendar_id = ? and "
                    + "((entry.time_span_begins between ? and ?) or "
                    + " (entry.repetition_ends between ? and ?))",
                    new JoinOn("calendar_entry_id", "o_invitees_calendar_entry_id"),
                    tenantId,
                    calendarId,
                    beginsOn,
                    endsOn,
                    beginsOn,
                    endsOn);
        }
    }
}
