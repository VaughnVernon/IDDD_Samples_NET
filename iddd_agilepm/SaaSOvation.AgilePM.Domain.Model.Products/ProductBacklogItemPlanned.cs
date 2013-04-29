namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;

    public class ProductBacklogItemPlanned : DomainEvent
    {
        public ProductBacklogItemPlanned(
            TenantId tenantId,
            ProductId productId,
            BacklogItemId backlogItemId,
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

        public BacklogItemId BacklogItemId { get; private set; }

        public string Category { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public ProductId ProductId { get; private set; }

        public StoryPoints StoryPoints { get; private set; }

        public string Summary { get; private set; }

        public TenantId TenantId { get; private set; }

        public BacklogItemType Type { get; private set; }
    }
}
