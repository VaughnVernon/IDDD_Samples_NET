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

    public enum GroupMemberType { Group, User }

    public class GroupMember
    {
        internal GroupMember(TenantId tenantId, string name, GroupMemberType type)
        {
            this.Name = name;
            this.TenantId = tenantId;
            this.Type = type;
        }

        internal GroupMember()
        {
        }

        public string Name { get; private set; }

        public TenantId TenantId { get; private set; }

        private GroupMemberType _type;
        public GroupMemberType Type
        {
            get
            {
                return this._type;
            }            
            private set
            {
                this._type = value;
            }
        }

        public bool IsGroup()
        {
            return this.Type == GroupMemberType.Group;
        }

        public bool IsUser()
        {
            return this.Type == GroupMemberType.User;
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType()) {
                GroupMember typedObject = (GroupMember) anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.Name.Equals(typedObject.Name) &&
                    this.Type.Equals(typedObject.Type);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (21941 * 197)
                + this.TenantId.GetHashCode()
                + this.Name.GetHashCode()
                + this.Type.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "GroupMember [name=" + Name + ", tenantId=" + TenantId + ", type=" + Type + "]";
        }
    }
}
