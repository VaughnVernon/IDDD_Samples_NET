using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Collaboration.Domain.Model.Forums;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Application.Forums.Data;

namespace SaaSOvation.Collaboration.Application.Forums
{
    public class DiscussionApplicationService
    {
        public DiscussionApplicationService(
            IDiscussionRepository discussionRepository,
            ForumIdentityService forumIdentityService,
            IPostRepository postRepository,
            ICollaboratorService collaboratorService)
        {
            this.discussionRepository = discussionRepository;
            this.forumIdentityService = forumIdentityService;
            this.postRepository = postRepository;
            this.collaboratorService = collaboratorService;
        }

        readonly ICollaboratorService collaboratorService;
        readonly IDiscussionRepository discussionRepository;
        readonly ForumIdentityService forumIdentityService;
        readonly IPostRepository postRepository;

        public void CloseDiscussion(string tenantId, string discussionId)
        {
            var discussion = this.discussionRepository.Get(new Tenant(tenantId), new DiscussionId(discussionId));

            discussion.Close();

            this.discussionRepository.Save(discussion);
        }

        public void PostToDiscussion(string tenantId, string discussionId, string authorId, string subject, string bodyText, IDiscussionCommandResult discussionCommandResult)
        {
            var discussion = this.discussionRepository.Get(new Tenant(tenantId), new DiscussionId(discussionId));

            var author = this.collaboratorService.GetAuthorFrom(new Tenant(tenantId), authorId);

            var post = discussion.Post(this.forumIdentityService, author, subject, bodyText);

            this.postRepository.Save(post);

            discussionCommandResult.SetResultingDiscussionId(discussionId);
            discussionCommandResult.SetResultingPostId(post.PostId.Id);
        }

        public void PostToDiscussionInReplyTo(string tenantId, string discussionId, string replyToPostId, string authorId,
            string subject, string bodyText, IDiscussionCommandResult discussionCommandResult)
        {
            var discussion = this.discussionRepository.Get(new Tenant(tenantId), new DiscussionId(discussionId));

            var author = this.collaboratorService.GetAuthorFrom(new Tenant(tenantId), authorId);

            var post = discussion.Post(this.forumIdentityService, author, subject, bodyText, new PostId(replyToPostId));

            this.postRepository.Save(post);

            discussionCommandResult.SetResultingDiscussionId(discussionId);
            discussionCommandResult.SetResultingPostId(post.PostId.Id);
            discussionCommandResult.SetRresultingInReplyToPostId(replyToPostId);
        }

        public void ReOpenDiscussion(string tenantId, string discussionId)
        {
            var discussion = this.discussionRepository.Get(new Tenant(tenantId), new DiscussionId(discussionId));

            discussion.ReOpen();

            this.discussionRepository.Save(discussion);
        }
    }
}
