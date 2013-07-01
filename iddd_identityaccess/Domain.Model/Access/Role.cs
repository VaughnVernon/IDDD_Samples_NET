// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class Role : EntityWithCompositeId
    {
        public Role(TenantId tenantId, string name, string description, bool supportsNesting)
        {
            this.Description = description;
            this.Name = name;
            this.SupportsNesting = supportsNesting;
            this.TenantId = tenantId;
            this.group = CreateInternalGroup();
        }

        protected Role() { }

        readonly Group group;

        Group CreateInternalGroup()
        {
            var groupName = Group.ROLE_GROUP_PREFIX + Guid.NewGuid().ToString();
            return new Group(this.TenantId, groupName, "Role backing group for: " + this.Name);
        }

        public int Id { get; set; }

        public string Description { get; private set; }

        public string Name { get; private set; }

        public bool SupportsNesting { get; private set; }

        public TenantId TenantId { get; private set; }
        
        public void AssignGroup(Group group, GroupMemberService groupMemberService)
        {
            AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

            this.group.AddGroup(group, groupMemberService);

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

            this.group.AddUser(user);

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
            return this.group.IsMember(user, groupMemberService);
        }

        public void UnassignGroup(Group group)
        {
            AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

            this.group.RemoveGroup(group);

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

            this.group.RemoveUser(user);

            DomainEventPublisher
                .Instance
                .Publish(new UserUnassignedFromRole(
                        this.TenantId,
                        this.Name,
                        user.Username));
        }

        protected override System.Collections.Generic.IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.Name;
        }

        public override string ToString()
        {
            return "Role [tenantId=" + TenantId + ", name=" + Name
                    + ", description=" + Description
                    + ", supportsNesting=" + SupportsNesting
                    + ", group=" + group + "]";
        }        
    }
}
