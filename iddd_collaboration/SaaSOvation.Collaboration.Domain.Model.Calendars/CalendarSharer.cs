using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarSharer : ValueObject, IComparable<CalendarSharer>
    {
        public CalendarSharer(Participant participant)
        {
            AssertionConcern.AssertArgumentNotNull(participant, "Participant must be provided.");
            this.participant = participant;
        }

        readonly Participant participant;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.participant;
        }

        public int CompareTo(CalendarSharer other)
        {
            return this.participant.CompareTo(other.participant);
        }
    }
}
