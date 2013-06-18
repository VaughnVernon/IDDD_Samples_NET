using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class TaskDefined : IDomainEvent
    {
        public TaskDefined(Tenants.TenantId tenantId, BacklogItemId backlogItemId, TaskId taskId, string volunteerMemberId, string name, string description)
        {
            this.TenantId = tenantId;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.UtcNow;
            this.BacklogItemId = backlogItemId;

            this.TaskId = taskId;
            this.VolunteerMemberId = volunteerMemberId;
            this.Name = name;
            this.Description = description;
        }

        public Tenants.TenantId TenantId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
        public BacklogItemId BacklogItemId { get; private set; }

        public TaskId TaskId { get; private set; }
        public string VolunteerMemberId { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }
    }
}
