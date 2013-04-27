namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Products.Releases;
    using SaaSOvation.Common.Domain.Model;

    public class ProductReleaseScheduled : DomainEvent
    {
        public ProductReleaseScheduled(
            Identity<Tenant> tenantId,
            Identity<Product> productId,
            Identity<Release> releaseId,
            string name,
            string description,
            DateTime starts,
            DateTime ends)
        {
            this.Description = description;
            this.Ends = ends;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.ReleaseId = releaseId;
            this.Starts = starts;
            this.TenantId = tenantId;
        }

        public string Description { get; private set; }

        public DateTime Ends { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public Identity<Product> ProductId { get; private set; }

        public Identity<Release> ReleaseId { get; private set; }

        public DateTime Starts { get; private set; }

        public Identity<Tenant> TenantId { get; private set; }
    }
}
