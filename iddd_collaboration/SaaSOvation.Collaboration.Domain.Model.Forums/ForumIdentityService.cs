using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class ForumIdentityService
    {
        public ForumIdentityService(IDiscussionRepository discussionRepository, IForumRepository forumRepository, IPostRepository postRepository)
        {
            this.discussionRepository = discussionRepository;
            this.forumRepository = forumRepository;
            this.postRepository = postRepository;
        }

        readonly IDiscussionRepository discussionRepository;
        readonly IForumRepository forumRepository;
        readonly IPostRepository postRepository;

        public DiscussionId GetNextDiscussionId()
        {
            return this.discussionRepository.GetNextIdentity();
        }

        public ForumId GetNextForumId()
        {
            return this.forumRepository.GetNextIdentity();
        }

        public PostId GetNexPostId()
        {
            return this.postRepository.GetNextIdentity();
        }
    }
}
