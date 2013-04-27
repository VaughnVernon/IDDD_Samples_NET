namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class UserDescriptor : AssertionConcern
    {
        public static UserDescriptor NullDescriptorInstance()
        {
            return new UserDescriptor();
        }

        public UserDescriptor(Identity<Tenant> tenantId, string username, string emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.TenantId = tenantId;
            this.Username = username;
        }

        private UserDescriptor()
        {
        }

        public string EmailAddress { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }

        public string Username { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType()) {
                UserDescriptor typedObject = (UserDescriptor) anotherObject;
                equalObjects =
                        this.EmailAddress.Equals(typedObject.EmailAddress) &&
                        this.TenantId.Equals(typedObject.TenantId) &&
                        this.Username.Equals(typedObject.Username);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (9429 * 263)
                + this.EmailAddress.GetHashCode()
                + this.TenantId.GetHashCode()
                + this.Username.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "UserDescriptor [emailAddress=" + EmailAddress
                    + ", tenantId=" + TenantId + ", username=" + Username + "]";
        }
    }
}
