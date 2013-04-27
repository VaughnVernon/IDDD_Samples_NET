namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using System;

    public class MemberChangeTracker
    {
        internal MemberChangeTracker(DateTime enablingOn, DateTime nameChangedOn, DateTime emailAddressChangedOn)
        {
            this.EmailAddressChangedOnDate = emailAddressChangedOn;
            this.EnablingOnDate = enablingOn;
            this.NameChangedOnDate = nameChangedOn;
        }

        private DateTime EnablingOnDate { get; set; }

        private DateTime NameChangedOnDate { get; set; }

        private DateTime EmailAddressChangedOnDate { get; set; }

        public bool CanChangeEmailAddress(DateTime asOfDateTime)
        {
            return this.EmailAddressChangedOnDate < asOfDateTime;
        }

        public bool CanChangeName(DateTime asOfDateTime)
        {
            return this.NameChangedOnDate < asOfDateTime;
        }

        public bool CanToggleEnabling(DateTime asOfDateTime)
        {
            return this.EnablingOnDate < asOfDateTime;
        }

        public MemberChangeTracker EmailAddressChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.EnablingOnDate, this.NameChangedOnDate, asOfDateTime);
        }

        public MemberChangeTracker EnablingOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(asOfDateTime, this.NameChangedOnDate, this.EmailAddressChangedOnDate);
        }

        public MemberChangeTracker NameChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.EnablingOnDate, asOfDateTime, this.EmailAddressChangedOnDate);
        }
    }
}
