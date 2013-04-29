namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class Role : AssertionConcern
    {
        public Role(TenantId tenantId, string name, string description, bool supportsNesting)
        {
            this.Description = description;
            this.Name = name;
            this.SupportsNesting = supportsNesting;
            this.TenantId = tenantId;

            this.CreateInternalGroup();
        }

        public string Description { get; private set; }

        public string Name { get; private set; }

        public bool SupportsNesting { get; private set; }

        public TenantId TenantId { get; private set; }

        private Group Group { get; set; }

        public void AssignGroup(Group group, GroupMemberService groupMemberService)
        {
            AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

            this.Group.AddGroup(group, groupMemberService);

            DomainEventPublisher
                .Instance
                .Publish(new GroupAssignedToRole(
                        this.TenantId,
                        this.Name,
                        group.Name));
        }

        public void AssignUser(User user)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this user.");

            this.Group.AddUser(user);

            DomainEventPublisher
                .Instance
                .Publish(new UserAssignedToRole(
                        this.TenantId,
                        this.Name,
                        user.Username,
                        user.Person.Name.FirstName,
                        user.Person.Name.LastName,
                        user.Person.EmailAddress.Address));
        }

        public bool IsInRole(User user, GroupMemberService groupMemberService)
        {
            return this.Group.IsMember(user, groupMemberService);
        }

        public void UnassignGroup(Group group)
        {
            AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

            this.Group.RemoveGroup(group);

            DomainEventPublisher
                .Instance
                .Publish(new GroupUnassignedFromRole(
                        this.TenantId,
                        this.Name,
                        group.Name));
        }

        public void UnassignUser(User user)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this user.");

            this.Group.RemoveUser(user);

            DomainEventPublisher
                .Instance
                .Publish(new UserUnassignedFromRole(
                        this.TenantId,
                        this.Name,
                        user.Username));
        }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                Role typedObject = (Role)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.Name.Equals(typedObject.Name);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (18723 * 233)
                + this.TenantId.GetHashCode()
                + this.Name.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "Role [tenantId=" + TenantId + ", name=" + Name
                    + ", description=" + Description
                    + ", supportsNesting=" + SupportsNesting
                    + ", group=" + Group + "]";
        }

        private void CreateInternalGroup()
        {
            String groupName = Group.ROLE_GROUP_PREFIX + Guid.NewGuid().ToString();

            this.Group = new Group(this.TenantId, groupName, "Role backing group for: " + this.Name);
        }
    }
}
