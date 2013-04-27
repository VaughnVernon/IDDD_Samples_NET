namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;

    public class Enablement
    {
        public static Enablement IndefiniteEnablement()
        {
            return new Enablement(true, DateTime.MinValue, DateTime.MinValue);
        }

        public Enablement(bool enabled, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new InvalidOperationException("Enablement start and/or end date is invalid.");
            }

            this.Enabled = enabled;
            this.EndDate = endDate;
            this.StartDate = startDate;
        }

        public bool Enabled { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime StartDate { get; private set; }

        public bool IsEnablementEnabled()
        {
            bool enabled = false;

            if (this.Enabled)
            {
                if (!this.IsTimeExpired())
                {
                    enabled = true;
                }
            }

            return enabled;
        }

        public bool IsTimeExpired()
        {
            bool timeExpired = false;

            if (this.StartDate != DateTime.MinValue && this.EndDate != DateTime.MinValue)
            {
                DateTime now = DateTime.Now;
                if (now < this.StartDate || now > this.EndDate)
                {
                    timeExpired = true;
                }
            }

            return timeExpired;
        }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                Enablement typedObject = (Enablement)anotherObject;
                equalObjects =
                    this.Enabled == typedObject.Enabled &&
                    this.StartDate == typedObject.StartDate &&
                    this.EndDate == typedObject.EndDate;
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (19563 * 181)
                + (this.Enabled ? 1:0)
                + (this.StartDate == null ? 0:this.StartDate.GetHashCode())
                + (this.EndDate == null ? 0:this.EndDate.GetHashCode());

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "Enablement [enabled=" + Enabled + ", endDate=" + EndDate + ", startDate=" + StartDate + "]";
        }
    }
}
