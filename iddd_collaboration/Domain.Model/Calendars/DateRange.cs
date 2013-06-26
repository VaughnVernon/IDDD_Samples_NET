using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class DateRange : ValueObject
    {
        public DateRange(DateTime begins, DateTime ends)
        {
            if (begins > ends)
                throw new ArgumentException("Time span must not end before it begins.");

            this.begins = begins;
            this.ends = ends;
        }

        readonly DateTime begins;
        readonly DateTime ends;

        public DateTime Begins
        {
            get { return this.begins; }
        }

        public DateTime Ends
        {
            get { return this.ends; }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.begins;
            yield return this.ends;
        }
    }
}
