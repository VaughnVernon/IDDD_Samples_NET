using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class Discussion : EventSourcedRootEntity
    {
        public Discussion(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : base(eventStream, streamVersion)
        {
        }
    
        public Discussion(Tenant tenantId, ForumId forumId, DiscussionId discussionId, Author author, string subject, string exclusiveOwner = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenant must be provided.");
            AssertionConcern.AssertArgumentNotNull(forumId, "The forum id must be provided.");
            AssertionConcern.AssertArgumentNotNull(discussionId, "The discussion id must be provided.");
            AssertionConcern.AssertArgumentNotNull(author, "The author must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(subject, "The subject must be provided.");

            Apply(new DiscussionStarted(tenantId, forumId, discussionId, author, subject, exclusiveOwner));
        }        

        void When(DiscussionStarted e)
        {
            this.tenantId = e.TenantId;
            this.forumId = e.ForumId;
            this.discussionId = e.DiscussionId;
            this.author = e.Author;
            this.subject = e.Subject;
            this.exclusiveOwner = e.ExclusiveOwner;
        }

        Tenant tenantId;
        ForumId forumId;
        DiscussionId discussionId;
        Author author;
        string subject;
        string exclusiveOwner;    
        bool closed;

        public DiscussionId DiscussionId
        {
            get { return this.discussionId; }
        }

        void AssertClosed()
        {
            if (!this.closed)
                throw new InvalidOperationException("This discussion is already open.");
        }

        public void Close()
        {
            if (this.closed)
                throw new InvalidOperationException("This discussion is already closed.");

            Apply(new DiscussionClosed(this.tenantId, this.forumId, this.discussionId, this.exclusiveOwner));
        }

        void When(DiscussionClosed e)
        {
            this.closed = true;
        }


        public Post Post(ForumIdentityService forumIdService, Author author, string subject, string bodyText, PostId replyToPostId = null)
        {
            return new Post(
                this.tenantId,
                this.forumId,
                this.discussionId,
                forumIdService.GetNexPostId(),
                author,
                subject,
                bodyText,
                replyToPostId);
        }


        public void ReOpen()
        {
            AssertClosed();
            Apply(new DiscussionReopened(this.tenantId, this.forumId, this.discussionId, this.exclusiveOwner));
        }

        void When(DiscussionReopened e)
        {
            this.closed = false;
        }




        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.tenantId;
            yield return this.forumId;
            yield return this.discussionId;
        }
    }
}
