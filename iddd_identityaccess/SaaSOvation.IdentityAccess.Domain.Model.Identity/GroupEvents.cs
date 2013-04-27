namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class GroupGroupAdded : DomainEvent
    {
        public GroupGroupAdded(Identity<Tenant> tenantId, string groupName, string nestedGroupName)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.NestedGroupName = nestedGroupName;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public string NestedGroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class GroupGroupRemoved : DomainEvent
    {
        public GroupGroupRemoved(Identity<Tenant> tenantId, string groupName, string nestedGroupName)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.NestedGroupName = nestedGroupName;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public string NestedGroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class GroupUserAdded : DomainEvent
    {
        public GroupUserAdded(Identity<Tenant> tenantId, string groupName, string username)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class GroupUserRemoved : DomainEvent
    {
        public GroupUserRemoved(Identity<Tenant> tenantId, string groupName, string username)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
