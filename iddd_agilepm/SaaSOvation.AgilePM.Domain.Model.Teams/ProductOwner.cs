namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class ProductOwner : Member
    {
        public ProductOwner(
            Identity<Tenant> tenantId,
            string username,
            string firstName,
            string lastName,
            string emailAddress,
            DateTime initializedOn)
            : base(tenantId, username, firstName, lastName, emailAddress, initializedOn)
        {
        }

        public Identity<ProductOwner> ProductOwnerId { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                ProductOwner typedObject = (ProductOwner)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.Username.Equals(typedObject.Username);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (71121 * 79)
                + this.TenantId.GetHashCode()
                + this.Username.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "ProductOwner [productOwnerId()=" + ProductOwnerId + ", emailAddress()=" + EmailAddress + ", isEnabled()="
                    + Enabled + ", firstName()=" + FirstName + ", lastName()=" + LastName + ", tenantId()=" + TenantId
                    + ", username()=" + Username + "]";
        }
    }
}
