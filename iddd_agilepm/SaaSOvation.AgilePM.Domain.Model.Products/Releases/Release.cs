namespace SaaSOvation.AgilePM.Domain.Model.Products.Releases
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Release : Entity
    {
        public Release(
            TenantId tenantId,
            ProductId productId,
            ReleaseId releaseId,
            string name,
            string description,
            DateTime begins,
            DateTime ends)
        {
            if (ends.Ticks < begins.Ticks)
            {
                throw new InvalidOperationException("Release must not end before it begins.");
            }

            this.BacklogItems = new HashSet<ScheduledBacklogItem>();
            this.Begins = begins;
            this.Description = description;
            this.Ends = ends;
            this.Name = name;
            this.ProductId = productId;
            this.ReleaseId = releaseId;
            this.TenantId = tenantId;
        }

        public DateTime Begins { get; private set; }

        public string Description { get; private set; }

        public DateTime Ends { get; private set; }

        public string Name { get; private set; }

        public ProductId ProductId { get; private set; }

        public ReleaseId ReleaseId { get; private set; }

        public TenantId TenantId;

        private ISet<ScheduledBacklogItem> BacklogItems;

        public ICollection<ScheduledBacklogItem> AllScheduledBacklogItems()
        {
            return new ReadOnlyCollection<ScheduledBacklogItem>(new List<ScheduledBacklogItem>(this.BacklogItems));
        }
    }
}
