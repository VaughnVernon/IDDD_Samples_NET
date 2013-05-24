using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarId : SaaSOvation.Common.Domain.Model.Identity
    {
        public CalendarId() { }

        public CalendarId(string id)
            : base(id)
        {
        }
    }
}
