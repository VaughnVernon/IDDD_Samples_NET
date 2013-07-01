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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using System.Collections.Generic;

    using SaaSOvation.Common.Domain.Model;

    public class Group : EntityWithCompositeId
    {
        public const string ROLE_GROUP_PREFIX = "ROLE-INTERNAL-GROUP: ";

        public Group(TenantId tenantId, string name, string description)
            : this()
        {
            this.Description = description;
            this.Name = name;
            this.TenantId = tenantId;
        }

        protected Group()
        {
            this.GroupMembers = new HashSet<GroupMember>();
        }

        public TenantId TenantId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ISet<GroupMember> GroupMembers { get; private set; }

        bool IsInternalGroup
        {
            get
            {
                return this.Name.StartsWith(ROLE_GROUP_PREFIX);
            }
        }

        public void AddGroup(Group group, GroupMemberService groupMemberService)
        {
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");
            AssertionConcern.AssertArgumentFalse(groupMemberService.IsMemberGroup(group, this.ToGroupMember()), "Group recurrsion.");

            if (this.GroupMembers.Add(group.ToGroupMember()) && !this.IsInternalGroup)
            {
                DomainEventPublisher
                    .Instance
                    .Publish(new GroupGroupAdded(
                            this.TenantId,
                            this.Name,
                            group.Name));
            }
        }

        public void AddUser(User user)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this group.");
            AssertionConcern.AssertArgumentTrue(user.IsEnabled, "User is not enabled.");

            if (this.GroupMembers.Add(user.ToGroupMember()) && !this.IsInternalGroup)
            {
                DomainEventPublisher
                    .Instance
                    .Publish(new GroupUserAdded(
                            this.TenantId,
                            this.Name,
                            user.Username));
            }
        }

        public bool IsMember(User user, GroupMemberService groupMemberService)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this group.");
            AssertionConcern.AssertArgumentTrue(user.IsEnabled, "User is not enabled.");

            var isMember = this.GroupMembers.Contains(user.ToGroupMember());

            if (isMember)
            {
                isMember = groupMemberService.ConfirmUser(this, user);
            }
            else
            {
                isMember = groupMemberService.IsUserInNestedGroup(this, user);
            }

            return isMember;
        }

        public void RemoveGroup(Group group)
        {
            AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

            // not a nested remove, only direct member
            if (this.GroupMembers.Remove(group.ToGroupMember()) && !this.IsInternalGroup)
            {
                DomainEventPublisher
                    .Instance
                    .Publish(new GroupGroupRemoved(
                            this.TenantId,
                            this.Name,
                            group.Name));
            }
        }

        public void RemoveUser(User user)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this group.");

            // not a nested remove, only direct member
            if (this.GroupMembers.Remove(user.ToGroupMember()) && !this.IsInternalGroup)
            {
                DomainEventPublisher
                    .Instance
                    .Publish(new GroupUserRemoved(
                            this.TenantId,
                            this.Name,
                            user.Username));
            }
        }

        internal GroupMember ToGroupMember()
        {
            return new GroupMember(this.TenantId, this.Name, GroupMemberType.Group);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.Name;
        }

        public override string ToString()
        {
            return "Group [description=" + Description + ", name=" + Name + ", tenantId=" + TenantId + "]";
        }
    }
}
