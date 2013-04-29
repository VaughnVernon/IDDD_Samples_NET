namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Discussions;
    using SaaSOvation.AgilePM.Domain.Model.Teams;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class ProductCreated : DomainEvent
    {
        public ProductCreated(
            TenantId tenantId,
            ProductId productId,
            ProductOwnerId productOwnerId,
            string name,
            string description,
            DiscussionAvailability availability)
        {
            this.Availability = availability;
            this.Description = description;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.ProductOwnerId = productOwnerId;
            this.TenantId = tenantId;
        }

        public DiscussionAvailability Availability { get; private set; }

        public string Description { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public ProductId ProductId { get; private set; }

        public ProductOwnerId ProductOwnerId { get; private set; }

        public TenantId TenantId { get; private set; }
    }
}
