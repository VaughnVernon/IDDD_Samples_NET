using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class DiscussionStarted : IDomainEvent
    {
        public DiscussionStarted(Tenant tenantId, ForumId forumId, DiscussionId discussionId, Author author, string subject, string exclusiveOwner)
        {
            this.TenantId = tenantId;
            this.ForumId = forumId;
            this.DiscussionId = discussionId;
            this.Author = author;
            this.Subject = subject;
            this.ExclusiveOwner = exclusiveOwner;
        }

        public Tenant TenantId { get; private set; }
        public ForumId ForumId { get; private set; }
        public DiscussionId DiscussionId { get; private set; }
        public Author Author { get; private set; }
        public string Subject { get; private set; }
        public string ExclusiveOwner { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
