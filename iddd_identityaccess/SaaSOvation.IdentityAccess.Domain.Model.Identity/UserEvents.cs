namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class PersonContactInformationChanged : DomainEvent
    {
        public PersonContactInformationChanged(
                Identity<Tenant> tenantId,
                String username,
                ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public ContactInformation ContactInformation { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class PersonNameChanged : DomainEvent
    {
        public PersonNameChanged(
                Identity<Tenant> tenantId,
                String username,
                FullName name)
        {
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public FullName Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserEnablementChanged : DomainEvent
    {
        public UserEnablementChanged(
                Identity<Tenant> tenantId,
                String username,
                Enablement enablement)
        {
            this.Enablement = enablement;
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public Enablement Enablement { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserPasswordChanged : DomainEvent
    {
        public UserPasswordChanged(
                Identity<Tenant> tenantId,
                String username)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserRegistered : DomainEvent
    {
        public UserRegistered(
                Identity<Tenant> tenantId,
                String username,
                FullName name,
                EmailAddress emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public EmailAddress EmailAddress { get; private set; }

        public int EventVersion { get; set; }

        public FullName Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
