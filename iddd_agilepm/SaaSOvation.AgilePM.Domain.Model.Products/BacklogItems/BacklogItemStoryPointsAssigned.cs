namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;

    public class BacklogItemStoryPointsAssigned
    {
        public BacklogItemStoryPointsAssigned(
            TenantId tenantId,
            BacklogItemId backlogItemId,
            StoryPoints storyPoints)
        {
            this.BacklogItemId = backlogItemId;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.StoryPoints = storyPoints;
            this.TenantId = tenantId;
        }

        public BacklogItemId BacklogItemId { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public StoryPoints StoryPoints { get; private set; }

        public TenantId TenantId { get; private set; }
    }
}
