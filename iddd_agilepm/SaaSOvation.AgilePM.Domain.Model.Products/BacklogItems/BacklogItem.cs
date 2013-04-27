namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class BacklogItem : Entity
    {
        public BacklogItem(
            Identity<Tenant> tenantId,
            Identity<Product> productId,
            Identity<BacklogItem> backlogItemId,
            string summary,
            string category,
            BacklogItemType type,
            BacklogItemStatus backlogItemStatus,
            StoryPoints storyPoints)
        {
            this.BacklogItemId = backlogItemId;
            this.Category = category;
            this.ProductId = productId;
            this.Status = backlogItemStatus;
            this.StoryPoints = storyPoints;
            this.Summary = summary;
            this.TenantId = tenantId;
            this.Type = type;
        }

        public Identity<BacklogItem> BacklogItemId { get; private set; }

        public string Category { get; private set; }

        public Identity<Product> ProductId { get; private set; }

        private BacklogItemStatus Status;

        public StoryPoints StoryPoints { get; private set; }

        public string Summary { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }

        public BacklogItemType Type { get; private set; }


    }
}
