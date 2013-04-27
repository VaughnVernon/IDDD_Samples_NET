namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Products.Releases;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Products.Sprints;

    public class ProductSprintScheduled : DomainEvent
    {
        public ProductSprintScheduled(
            Identity<Tenant> tenantId,
            Identity<Product> productId,
            Identity<Sprint> sprintId,
            string name,
            string goals,
            DateTime starts,
            DateTime ends)
        {
            this.Ends = ends;
            this.EventVersion = 1;
            this.Goals = goals;
            this.Name = name;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.SprintId = sprintId;
            this.Starts = starts;
            this.TenantId = tenantId;
        }

        public DateTime Ends { get; private set; }

        public int EventVersion { get; set; }

        public string Goals { get; private set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public Identity<Product> ProductId { get; private set; }

        public Identity<Sprint> SprintId { get; private set; }

        public DateTime Starts { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }
    }
}
