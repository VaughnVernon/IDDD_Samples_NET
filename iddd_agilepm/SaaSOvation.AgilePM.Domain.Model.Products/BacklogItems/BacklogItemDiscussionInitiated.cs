using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class BacklogItemDiscussionInitiated : IDomainEvent
    {
        public BacklogItemDiscussionInitiated(Tenants.TenantId tenantId, BacklogItemId backlogItemId, BacklogItemDiscussion discussion)
        {
            this.TenantId = tenantId;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.UtcNow;
            this.BacklogItemId = backlogItemId;

            this.Discussion = discussion;
        }

        public Tenants.TenantId TenantId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
        public BacklogItemId BacklogItemId { get; private set; }

        public BacklogItemDiscussion Discussion { get; private set; }
    }
}
