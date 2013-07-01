using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class Forum : EventSourcedRootEntity
    {
        public Forum(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : base(eventStream, streamVersion)
        {
        }

        public Forum(Tenant tenantId, ForumId forumId, Creator creator, Moderator moderator, string subject, string description, string exclusiveOwner)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenant must be provided.");
            AssertionConcern.AssertArgumentNotNull(forumId, "The forum id must be provided.");
            AssertionConcern.AssertArgumentNotNull(creator, "The creator must be provided.");
            AssertionConcern.AssertArgumentNotNull(moderator, "The moderator must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(subject, "The subject must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(description, "The description must be provided.");

            Apply(new ForumStarted(tenantId, forumId, creator, moderator, subject, description, exclusiveOwner));
        }

        void When(ForumStarted e)
        {
            this.tenantId = e.TenantId;
            this.forumId = e.ForumId;
            this.creator = e.Creator;
            this.moderator = e.Moderator;
            this.subject = e.Subject;
            this.description = e.Description;
            this.exclusiveOwner = e.ExclusiveOwner;
        }

        Tenant tenantId;
        ForumId forumId;
        Creator creator;
        Moderator moderator;
        string subject;
        string description;
        string exclusiveOwner;
        bool closed;

        public ForumId ForumId
        {
            get { return this.forumId; }
        }

        void AssertOpen()
        {
            if (this.closed)
                throw new InvalidOperationException("Forum is closed.");
        }

        void AssertClosed()
        {
            if (!this.closed)
                throw new InvalidOperationException("Forum is open.");
        }

        public void AssignModerator(Moderator moderator)
        {
            AssertOpen();
            AssertionConcern.AssertArgumentNotNull(moderator, "The moderator must be provided.");
            Apply(new ForumModeratorChanged(this.tenantId, this.forumId, moderator, this.exclusiveOwner));
        }

        void When(ForumModeratorChanged e)
        {
            this.moderator = e.Moderator;
        }


        public void ChangeDescription(string description)
        {
            AssertOpen();
            AssertionConcern.AssertArgumentNotEmpty(description, "The description must be provided.");
            Apply(new ForumDescriptionChanged(this.tenantId, this.forumId, description, this.exclusiveOwner));
        }

        void When(ForumDescriptionChanged e)
        {
            this.description = e.Description;
        }


        public void ChangeSubject(string subject)
        {
            AssertOpen();
            AssertionConcern.AssertArgumentNotEmpty(subject, "The subject must be provided.");
            Apply(new ForumSubjectChanged(this.tenantId, this.forumId, subject, this.exclusiveOwner));
        }

        void When(ForumSubjectChanged e)
        {
            this.subject = e.Subject;
        }


        public void Close()
        {
            AssertOpen();
            Apply(new ForumClosed(this.tenantId, this.forumId, this.exclusiveOwner));
        }

        void When(ForumClosed e)
        {
            this.closed = true;
        }


        public void ModeratePost(Post post, Moderator moderator, string subject, string bodyText)
        {
            AssertOpen();
            AssertionConcern.AssertArgumentNotNull(post, "Post may not be null.");
            AssertionConcern.AssertArgumentEquals(this.forumId, post.ForumId, "Not a post of this forum.");
            AssertionConcern.AssertArgumentTrue(IsModeratedBy(moderator), "Not the moderator of this forum.");
            post.AlterPostContent(subject, bodyText);
        }


        public void ReOpen()
        {
            AssertClosed();
            Apply(new ForumReopened(this.tenantId, this.forumId, this.exclusiveOwner));
        }

        void When(ForumReopened e)
        {
            this.closed = false;
        }

        public Discussion StartDiscussionFor(ForumIdentityService forumIdService, Author author, string subject, string exclusiveOwner = null)
        {
            AssertOpen();
            return new Discussion(
                this.tenantId,
                this.forumId,
                forumIdService.GetNextDiscussionId(),
                author,
                subject,
                exclusiveOwner);
        }


        public bool IsModeratedBy(Moderator moderator)
        {
            return this.moderator.Equals(moderator);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.tenantId;
            yield return this.forumId;
        }
    }
}
