using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain;
using SaaSOvation.Collaboration.Domain.Tenants;
using SaaSOvation.Collaboration.Domain.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public class DiscussionClosed : IDomainEvent
    {
        public DiscussionClosed(Tenant tenantId, ForumId forumId, DiscussionId discussionId, string exclusiveOwner)
        {
            this.TenantId = tenantId;
            this.ForumId = forumId;
            this.DiscussionId = discussionId;
            this.ExclusiveOwner = exclusiveOwner;
        }

        public Tenant TenantId { get; private set; }
        public ForumId ForumId { get; private set; }
        public DiscussionId DiscussionId { get; private set; }
        public string ExclusiveOwner { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
