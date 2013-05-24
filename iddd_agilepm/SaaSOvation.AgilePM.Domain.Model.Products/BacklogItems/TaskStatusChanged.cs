using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class TaskStatusChanged : IDomainEvent
    {
        public TaskStatusChanged(Tenants.TenantId tenantId, BacklogItemId backlogItemId, TaskId taskId, TaskStatus status)
        {
            this.TenantId = tenantId;
            this.BacklogItemId = backlogItemId;
            this.TaskId = taskId;
            this.Status = status;
        }

        public Tenants.TenantId TenantId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }

        public BacklogItemId BacklogItemId { get; private set; }
        public TaskId TaskId { get; private set; }
        public TaskStatus Status { get; private set; }
    }
}
