namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class ProductBacklogItem
    {
        public ProductBacklogItem(
            Identity<Tenants.Tenant> tenantId,
            Identity<Product> productId,
            Identity<BacklogItems.BacklogItem> backlogItem,
            int ordering)
        {
            this.BacklogItemId = backlogItem;
            this.Ordering = ordering;
            this.ProductId = productId;
            this.TenantId = tenantId;
        }

        public Identity<BacklogItems.BacklogItem> BacklogItemId { get; private set; }

        public int Ordering { get; private set; }

        public Identity<Product> ProductId { get; private set; }

        public Identity<Tenants.Tenant> TenantId { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                ProductBacklogItem typedObject = (ProductBacklogItem)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.ProductId.Equals(typedObject.ProductId) &&
                    this.BacklogItemId.Equals(typedObject.BacklogItemId);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (15389 * 97)
                + this.TenantId.GetHashCode()
                + this.ProductId.GetHashCode()
                + this.BacklogItemId.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "ProductBacklogItem [tenantId=" + TenantId
                    + ", productId=" + ProductId
                    + ", backlogItemId=" + BacklogItemId
                    + ", ordering=" + Ordering + "]";
        }

        internal void ReorderFrom(Identity<BacklogItems.BacklogItem> id, int ordering)
        {
            if (this.BacklogItemId.Equals(id))
            {
                this.Ordering = ordering;
            }
            else if (this.Ordering >= ordering)
            {
                this.Ordering = this.Ordering + 1;
            }
        }
    }
}
