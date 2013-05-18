namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class BusinessPriorityAssigned : DomainEvent
    {
        public BusinessPriorityAssigned(
            TenantId tenantId,
            BacklogItemId backlogItemId,
            BusinessPriority businessPriority)
        {
            this.BacklogItemId = backlogItemId;
            this.BusinessPriority = businessPriority;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.TenantId = tenantId;
        }

        public BacklogItemId BacklogItemId { get; private set; }

        public BusinessPriority BusinessPriority { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public TenantId TenantId { get; private set; }
    }
}
