using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class Repetition : ValueObject
    {
        public static Repetition DoesNotRepeat(DateTime ends)
        {
            return new Repetition(RepeatType.DoesNotRepeat, ends);
        }

        public static Repetition RepeatsIndefinitely(RepeatType repeatType)
        {
            return new Repetition(repeatType, DateTime.MaxValue);
        }

        public Repetition(RepeatType repeats, DateTime ends)
        {
            this.Repeats = repeats;
            this.Ends = ends;
        }

        public RepeatType Repeats { get; private set; }

        public DateTime Ends { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Repeats;
            yield return this.Ends;
        }
    }
}
