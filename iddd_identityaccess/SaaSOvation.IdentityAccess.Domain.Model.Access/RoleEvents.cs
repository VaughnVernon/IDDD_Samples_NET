namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class GroupAssignedToRole : DomainEvent
    {
        public GroupAssignedToRole(Identity<Identity.Tenant> tenantId, string roleName, string groupName)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = new DateTime();
            this.RoleName = roleName;
            this.TenantId = tenantId;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public Identity<Tenant> TenantId;
    }

    public class GroupUnassignedFromRole : DomainEvent
    {
        public GroupUnassignedFromRole(Identity<Identity.Tenant> tenantId, string roleName, string groupName)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = new DateTime();
            this.RoleName = roleName;
            this.TenantId = tenantId;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public Identity<Tenant> TenantId;
    }

    public class RoleProvisioned : DomainEvent
    {
        public RoleProvisioned(Identity<Tenant> tenantId, string name)
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

    public class UserAssignedToRole : DomainEvent
    {
        public UserAssignedToRole(
            Identity<Identity.Tenant> tenantId,
            string roleName,
            string username,
            string firstName,
            string lastName,
            string emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.EventVersion = 1;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.OccurredOn = new DateTime();
            this.RoleName = roleName;
            this.TenantId = tenantId;
            this.Username = username;
        }

        public string EmailAddress { get; private set; }

        public int EventVersion { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public Identity<Tenant> TenantId;

        public string Username { get; private set; }
    }

    public class UserUnassignedFromRole : DomainEvent
    {
        public UserUnassignedFromRole(Identity<Identity.Tenant> tenantId, string roleName, string username)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.RoleName = roleName;
            this.TenantId = tenantId;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public string Username { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public Identity<Tenant> TenantId;
    }
}
