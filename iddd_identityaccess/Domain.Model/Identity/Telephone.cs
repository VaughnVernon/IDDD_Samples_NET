using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    public class Telephone : ValueObject
    {
        public Telephone(string number)
        {
            this.Number = number;
        }

        public Telephone(Telephone telephone)
            : this(telephone.Number)
        {
        }

        protected Telephone() { }

        string number;

        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "Telephone number is required.");
                AssertionConcern.AssertArgumentLength(value, 5, 20, "Telephone number may not be more than 20 characters.");
                AssertionConcern.AssertArgumentMatches(
                        "((\\(\\d{3}\\))|(\\d{3}-))\\d{3}-\\d{4}",
                        value,
                        "Telephone number or its format is invalid.");

                this.number = value;
            }
        }

        public override string ToString()
        {
            return "Telephone [number=" + Number + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Number;
        }
    }
}
