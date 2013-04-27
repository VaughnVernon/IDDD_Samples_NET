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
            Identity<Tenants.Tenant> tenantId,
            Identity<Product> productId,
            Identity<Release> releaseId,
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

        public Identity<Product> ProductId { get; private set; }

        public Identity<Release> ReleaseId { get; private set; }

        public Identity<Tenant> TenantId;

        private ISet<ScheduledBacklogItem> BacklogItems;

        public ICollection<ScheduledBacklogItem> AllScheduledBacklogItems()
        {
            return new ReadOnlyCollection<ScheduledBacklogItem>(new List<ScheduledBacklogItem>(this.BacklogItems));
        }
    }
}
