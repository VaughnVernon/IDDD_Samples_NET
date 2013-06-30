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
    public class PostApplicationService
    {
        public PostApplicationService(IPostRepository postRepository, IForumRepository forumRepository, ICollaboratorService collaboratorService)
        {
            this.postRepository = postRepository;
            this.forumRepository = forumRepository;
            this.collaboratorService = collaboratorService;
        }

        readonly IPostRepository postRepository;
        readonly IForumRepository forumRepository;
        readonly ICollaboratorService collaboratorService;

        public void ModeratePost(
            string tenantId,
            string forumId,
            string postId,
            string moderatorId,
            string subject,
            string bodyText)
        {
            var tenant = new Tenant(tenantId);

            var forum = this.forumRepository.Get(tenant, new ForumId(forumId));

            var moderator = this.collaboratorService.GetModeratorFrom(tenant, moderatorId);

            var post = this.postRepository.Get(tenant, new PostId(postId));

            forum.ModeratePost(post, moderator, subject, bodyText);

            this.postRepository.Save(post);
        }
    }
}
