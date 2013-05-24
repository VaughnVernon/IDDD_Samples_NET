using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain;
using SaaSOvation.Collaboration.Domain.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Calendars
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
