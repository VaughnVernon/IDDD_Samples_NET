using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Application.Calendars.Data
{
    public class CalendarData
    {
        public string CalendarId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string OwnerEmailAddress { get; set; }
        public string OwnerIdentity { get; set; }
        public string OwnerName { get; set; }
        public ISet<CalendarSharerData> Sharers { get; set; }
        public string TenantId { get; set; }
    }
}
