namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;
    using System;
    
    public class ProductDiscussionInitiated : DomainEvent
    {
        public ProductDiscussionInitiated(TenantId tenantId, ProductId productId, ProductDiscussion productDiscussion)
        {
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.ProductDiscussion = productDiscussion;
            this.ProductId = productId;
            this.TenantId = tenantId;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public ProductDiscussion ProductDiscussion { get; private set; }

        public ProductId ProductId { get; private set; }

        public TenantId TenantId { get; private set; }
    }
}
