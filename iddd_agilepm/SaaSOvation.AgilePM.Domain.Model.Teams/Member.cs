namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class Member
    {
        protected Member(
            Identity<Tenant> tenantId,
            string username,
            string firstName,
            string lastName,
            string emailAddress,
            DateTime initializedOn)
        {
            this.EmailAddress = emailAddress;
            this.Enabled = true;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ChangeTracker = new MemberChangeTracker(initializedOn, initializedOn, initializedOn);
        }

        public string EmailAddress { get; private set; }

        public bool Enabled { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }

        public string Username { get; private set; }

        protected MemberChangeTracker ChangeTracker { get; private set; }

    }
}
