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
    public class ForumApplicationService
    {
        public ForumApplicationService(
            ForumQueryService forumQueryService,
            IForumRepository forumRepository,
            ForumIdentityService forumIdentityService,
            DiscussionQueryService discussionQueryService,
            IDiscussionRepository discussionRepository,
            ICollaboratorService collaboratorService)
        {
            this.forumQueryService = forumQueryService;
            this.forumRepository = forumRepository;
            this.forumIdentityService = forumIdentityService;
            this.discussionQueryService = discussionQueryService;
            this.discussionRepository = discussionRepository;
            this.collaboratorService = collaboratorService;
        }

        readonly ForumQueryService forumQueryService;
        readonly IForumRepository forumRepository;
        readonly ForumIdentityService forumIdentityService;
        readonly DiscussionQueryService discussionQueryService;
        readonly IDiscussionRepository discussionRepository;
        readonly ICollaboratorService collaboratorService;

        public void AssignModeratorToForum(string tenantId, string forumId, string moderatorId)
        {
            var tenant = new Tenant(tenantId);

            var forum = this.forumRepository.Get(tenant, new ForumId(forumId));

            var moderator = this.collaboratorService.GetModeratorFrom(tenant, moderatorId);

            forum.AssignModerator(moderator);

            this.forumRepository.Save(forum);
        }

        public void ChangeForumDescription(string tenantId, string forumId, string description)
        {
            var forum = this.forumRepository.Get(new Tenant(tenantId), new ForumId(forumId));

            forum.ChangeDescription(description);

            this.forumRepository.Save(forum);
        }

        public void ChangeForumSubject(string tenantId, string forumId, string subject)
        {
            var forum = this.forumRepository.Get(new Tenant(tenantId), new ForumId(forumId));

            forum.ChangeSubject(subject);

            this.forumRepository.Save(forum);
        }

        public void CloseForum(string tenantId, string forumId)
        {
            var forum = this.forumRepository.Get(new Tenant(tenantId), new ForumId(forumId));

            forum.Close();

            this.forumRepository.Save(forum);
        }

        public void ReOpenForum(string tenantId, string forumId)
        {
            var forum = this.forumRepository.Get(new Tenant(tenantId), new ForumId(forumId));

            forum.ReOpen();

            this.forumRepository.Save(forum);
        }

        public void StartForum(string tenantId, string creatorId, string moderatorId, string subject, string description, IForumCommandResult result = null)
        {
            var forum = StartNewForum(new Tenant(tenantId), creatorId, moderatorId, subject, description, null);

            if (result != null)
            {
                result.SetResultingForumId(forum.ForumId.Id);
            }
        }

        public void StartExclusiveForum(string tenantId, string exclusiveOwner, string creatorId, string moderatorId, string subject, string description, IForumCommandResult result = null)
        {
            var tenant = new Tenant(tenantId);

            Forum forum = null;

            var forumId = this.forumQueryService.GetForumIdByExclusiveOwner(tenantId, exclusiveOwner);
            if (forumId != null)
            {
                forum = this.forumRepository.Get(tenant, new ForumId(forumId));
            }

            if (forum == null)
            {
                forum = StartNewForum(tenant, creatorId, moderatorId, subject, description, exclusiveOwner);
            }

            if (result != null)
            {
                result.SetResultingForumId(forum.ForumId.Id);
            }
        }

        public void StartExclusiveForumWithDiscussion(
            string tenantId,
            string exclusiveOwner,
            string creatorId,
            string moderatorId,
            string authorId,
            string forumSubject,
            string forumDescription,
            string discussionSubject,
            IForumCommandResult result = null)
        {

            var tenant = new Tenant(tenantId);

            Forum forum = null;

            var forumId = this.forumQueryService.GetForumIdByExclusiveOwner(tenantId, exclusiveOwner);
            if (forumId != null)
            {
                forum = this.forumRepository.Get(tenant, new ForumId(forumId));
            }

            if (forum == null)
            {
                forum = StartNewForum(tenant, creatorId, moderatorId, forumSubject, forumDescription, exclusiveOwner);
            }

            Discussion discussion = null;

            var discussionId = this.discussionQueryService.GetDiscussionIdByExclusiveOwner(tenantId, exclusiveOwner);
            if (discussionId != null)
            {
                discussion = this.discussionRepository.Get(tenant, new DiscussionId(discussionId));
            }

            if (discussion == null)
            {
                var author = this.collaboratorService.GetAuthorFrom(tenant, authorId);

                discussion = forum.StartDiscussionFor(this.forumIdentityService, author, discussionSubject, exclusiveOwner);

                this.discussionRepository.Save(discussion);
            }

            if (result != null)
            {
                result.SetResultingForumId(forum.ForumId.Id);
                result.SetResultingDiscussionId(discussion.DiscussionId.Id);
            }
        }

        Forum StartNewForum(
            Tenant tenant,
            string creatorId,
            string moderatorId,
            string subject,
            string description,
            string exclusiveOwner)
        {
            var creator = this.collaboratorService.GetCreatorFrom(tenant, creatorId);

            var moderator = this.collaboratorService.GetModeratorFrom(tenant, moderatorId);

            var newForum = new Forum(
                        tenant,
                        this.forumRepository.GetNextIdentity(),
                        creator,
                        moderator,
                        subject,
                        description,
                        exclusiveOwner);

            this.forumRepository.Save(newForum);

            return newForum;
        }
    }
}
