namespace SaaSOvation.AgilePM.Domain.Model.Products.Sprints
{
    using SaaSOvation.Common.Domain.Model;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;

    public class Sprint : Entity
    {
        public Sprint(
            Identity<Tenants.Tenant> tenantId,
            Identity<Product> productId,
            Identity<Sprint> sprintId,
            string name,
            string goals,
            DateTime begins,
            DateTime ends)
        {
            if (ends.Ticks < begins.Ticks)
            {
                throw new InvalidOperationException("Sprint must not end before it begins.");
            }

            this.BacklogItems = new HashSet<CommittedBacklogItem>();
            this.Begins = begins;
            this.Goals = goals;
            this.Ends = ends;
            this.Name = name;
            this.ProductId = productId;
            this.SprintId = sprintId;
            this.TenantId = tenantId;
        }

        public System.DateTime Begins { get; private set; }

        public System.DateTime Ends { get; private set; }

        public string Goals { get; private set; }

        public string Name { get; private set; }

        public Identity<Product> ProductId { get; private set; }

        public Identity<Sprint> SprintId { get; private set; }

        public Identity<Tenants.Tenant> TenantId { get; private set; }

        private ISet<CommittedBacklogItem> BacklogItems;

        public void AdjustGoals(String goals)
        {
            this.Goals = goals;

            // TODO: publish event / student assignment
        }

        public ICollection<CommittedBacklogItem> AllCommittedBacklogItems()
        {
            return new ReadOnlyCollection<CommittedBacklogItem>(new List<CommittedBacklogItem>(this.BacklogItems));
        }

    }
}
