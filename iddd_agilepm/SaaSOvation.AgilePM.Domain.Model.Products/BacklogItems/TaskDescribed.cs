using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class TaskDescribed : IDomainEvent
    {
        public TaskDescribed(Tenants.TenantId tenantId, BacklogItemId backlogItemId, TaskId taskId, string description)
        {
            this.TenantId = tenantId;
            this.BacklogItemId = backlogItemId;
            this.TaskId = taskId;
            this.Description = description;
        }

        public Tenants.TenantId TenantId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }

        public BacklogItemId BacklogItemId { get; private set; }
        public TaskId TaskId { get; private set; }
        public string Description { get; private set; }
    }
}
