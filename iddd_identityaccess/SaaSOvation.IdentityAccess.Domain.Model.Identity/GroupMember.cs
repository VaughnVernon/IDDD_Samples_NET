namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public enum GroupMemberType { Group, User }

    public class GroupMember : AssertionConcern
    {
        internal GroupMember(Identity<Tenant> tenantId, string name, GroupMemberType type)
        {
            this.Name = name;
            this.TenantId = tenantId;
            this.Type = type;
        }

        internal GroupMember()
        {
        }

        public string Name { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }

        private GroupMemberType _type;
        public GroupMemberType Type
        {
            get
            {
                return this._type;
            }
            
            private set
            {
                this.AssertArgumentNotNull(value, "Type must be provided");
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
