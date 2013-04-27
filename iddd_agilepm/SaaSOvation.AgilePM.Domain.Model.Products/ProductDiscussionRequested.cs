namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Teams;

    class ProductDiscussionRequested : DomainEvent
    {
        public ProductDiscussionRequested(
            Identity<Tenant> tenantId,
            Identity<Product> productId,
            Identity<ProductOwner> productOwnerId,
            string name,
            string description,
            bool requestingDiscussion)
        {
            this.Description = description;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.ProductOwnerId = productOwnerId;
            this.RequestingDiscussion = requestingDiscussion;
            this.TenantId = tenantId;
        }

        public string Description { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public Identity<Product> ProductId { get; private set; }

        public Identity<ProductOwner> ProductOwnerId { get; private set; }

        public bool RequestingDiscussion { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }
    }
}
