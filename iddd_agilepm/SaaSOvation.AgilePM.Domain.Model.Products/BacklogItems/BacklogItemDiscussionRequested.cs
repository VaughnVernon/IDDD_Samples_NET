using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class BacklogItemDiscussionRequested : IDomainEvent
    {
        public BacklogItemDiscussionRequested(Tenants.TenantId tenantId, ProductId productId, BacklogItemId backlogItemId, bool isRequested)
        {
            this.TenantId = tenantId;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.UtcNow;
            this.ProductId = productId;
            this.BacklogItemId = backlogItemId;
            this.IsRequested = isRequested;
        }

        public Tenants.TenantId TenantId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
        public ProductId ProductId { get; private set; }
        public BacklogItemId BacklogItemId { get; private set; }

        public bool IsRequested { get; private set; }
    }
}
