using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class Post : EventSourcedRootEntity
    {
        public Post(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : base(eventStream, streamVersion)
        {
        }

        Tenant tenantId; 
        ForumId forumId; 
        DiscussionId discussionId; 
        PostId postId; 
        Author author; 
        string subject; 
        string bodyText; 
        PostId replyToPostId;

        public ForumId ForumId
        {
            get { return this.forumId; }
        }

        public PostId PostId
        {
            get { return this.postId; }
        }

        public Post(Tenant tenantId, ForumId forumId, DiscussionId discussionId, PostId postId, Author author, string subject, string bodyText, PostId replyToPostId = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenant must be provided.");
            AssertionConcern.AssertArgumentNotNull(forumId, "The forum id must be provided.");
            AssertionConcern.AssertArgumentNotNull(discussionId, "The discussion id must be provided.");
            AssertionConcern.AssertArgumentNotNull(postId, "The post id must be provided.");
            AssertionConcern.AssertArgumentNotNull(author, "The author must be provided.");
            AssertPostContent(subject, bodyText);

            Apply(new PostedToDiscussion(tenantId, forumId, discussionId, postId, author, subject, bodyText, replyToPostId));
        }

        void When(PostedToDiscussion e)
        {
            this.tenantId = e.TenantId;
            this.forumId = e.ForumId;
            this.discussionId = e.DiscussionId;
            this.postId = e.PostId;
            this.author = e.Author;
            this.subject = e.Subject;
            this.bodyText = e.BodyText;
            this.replyToPostId = e.ReplyToPostId;
        }

        void AssertPostContent(string subject, string bodyText)
        {
            AssertionConcern.AssertArgumentNotEmpty(subject, "The subject must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(bodyText, "The body text must be provided.");
        }


        internal void AlterPostContent(string subject, string bodyText)
        {
            AssertPostContent(subject, bodyText);
            Apply(new PostedContentAltered(this.tenantId, this.forumId, this.discussionId, this.postId, subject, bodyText));
        }

        void When(PostedContentAltered e)
        {
            this.subject = e.Subject;
            this.bodyText = e.BodyText;
        }
        

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.tenantId;
            yield return this.forumId;
            yield return this.discussionId;
            yield return this.postId;
        }
    }
}
