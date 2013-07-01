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
    using SaaSOvation.Common.Domain.Model;

    public class GroupMember : ValueObject
    {
        internal GroupMember(TenantId tenantId, string name, GroupMemberType type)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(name, "Member name is required.");
            AssertionConcern.AssertArgumentLength(name, 1, 100, "Member name must be 100 characters or less.");

            this.Name = name;
            this.TenantId = tenantId;
            this.Type = type;
        }

        protected GroupMember() { }

        public TenantId TenantId { get; private set; }

        public string Name { get; private set; }

        public GroupMemberType Type { get; private set; }

        public bool IsGroup
        {
            get
            {
                return this.Type == GroupMemberType.Group;
            }
        }

        public bool IsUser
        {
            get
            {
                return this.Type == GroupMemberType.User;
            }
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.TenantId;
            yield return this.Name;
            yield return this.Type;
        }

        public override string ToString()
        {
            return "GroupMember [name=" + Name + ", tenantId=" + TenantId + ", type=" + Type + "]";
        }
    }
}
