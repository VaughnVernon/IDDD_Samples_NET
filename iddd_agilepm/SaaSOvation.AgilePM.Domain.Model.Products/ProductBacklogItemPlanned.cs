namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;

    public class ProductBacklogItemPlanned : DomainEvent
    {
        public ProductBacklogItemPlanned(
            Identity<Tenants.Tenant> tenantId,
            Identity<Product> productId,
            Identity<BacklogItems.BacklogItem> backlogItemId,
            string summary,
            string category,
            BacklogItems.BacklogItemType backlogItemType,
            BacklogItems.StoryPoints storyPoints)
        {
            this.BacklogItemId = backlogItemId;
            this.Category = category;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.StoryPoints = storyPoints;
            this.Summary = summary;
            this.TenantId = tenantId;
            this.Type = backlogItemType;
        }

        public Identity<BacklogItems.BacklogItem> BacklogItemId { get; private set; }

        public string Category { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public Identity<Product> ProductId { get; private set; }

        public StoryPoints StoryPoints { get; private set; }

        public string Summary { get; private set; }

        public Identity<Tenants.Tenant> TenantId { get; private set; }

        public BacklogItemType Type { get; private set; }
    }
}
