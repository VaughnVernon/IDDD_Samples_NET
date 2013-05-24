using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain;
using SaaSOvation.Collaboration.Domain.Tenants;
using SaaSOvation.Collaboration.Domain.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public class ForumDescriptionChanged : IDomainEvent
    {
        public ForumDescriptionChanged(Tenant tenantId, ForumId forumId, string description, string exclusiveOwner)
        {
            this.TenantId = tenantId;
            this.ForumId = forumId;
            this.Description = description;
            this.ExclusiveOwner = exclusiveOwner;
        }

        public Tenant TenantId { get; private set; }
        public ForumId ForumId { get; private set; }
        public string Description { get; private set; }
        public string ExclusiveOwner { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
