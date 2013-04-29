namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class TenantAdministratorRegistered : DomainEvent
    {
        public TenantAdministratorRegistered(
            TenantId tenantId,
            string name,
            FullName administorName,
            EmailAddress emailAddress,
            string username,
            string temporaryPassword)
        {
            this.AdministorName = administorName;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TemporaryPassword = temporaryPassword;
            this.TenantId = tenantId.Id;
        }

        public FullName AdministorName { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TemporaryPassword { get; private set; }

        public string TenantId { get; private set; }
    }

    public class GroupProvisioned : DomainEvent
    {
        public GroupProvisioned(TenantId tenantId, string name)
        {
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantActivated : DomainEvent
    {
        public TenantActivated(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public  DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantDeactivated : DomainEvent
    {
        public TenantDeactivated(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantProvisioned : DomainEvent
    {
        public TenantProvisioned(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }
}
