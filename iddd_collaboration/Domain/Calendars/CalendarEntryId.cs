using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Calendars
{
    public class CalendarEntryId : SaaSOvation.Common.Domain.Identity
    {
        public CalendarEntryId()
        {
        }

        public CalendarEntryId(string id)
            : base(id)
        {
        }
    }
}
