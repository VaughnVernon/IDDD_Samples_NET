using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Port.Adapters.Persistence;
using SaaSOvation.Collaboration.Application.Forums.Data;

namespace SaaSOvation.Collaboration.Application.Forums
{
    public class DiscussionQueryService : AbstractQueryService
    {
        public DiscussionQueryService(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public IList<DiscussionData> GetAllDiscussionsDataByForum(string tenantId, string forumId)
        {
            return QueryObjects<DiscussionData>(
                    "select * from tbl_vw_discussion where tenant_id = ? and forum_id = ?",
                    new JoinOn(),
                    tenantId,
                    forumId);
        }

        public DiscussionData GetDiscussionDataById(string tenantId, string discussionId)
        {
            return QueryObject<DiscussionData>(
                    "select * from tbl_vw_discussion where tenant_id = ? and discussion_id = ?",
                    new JoinOn(),
                    tenantId,
                    discussionId);
        }

        public string GetDiscussionIdByExclusiveOwner(string tenantId, string exclusiveOwner)
        {
            return QueryString(
                    "select discussion_id from tbl_vw_discussion where tenant_id = ? and exclusive_owner = ?",
                    tenantId,
                    exclusiveOwner);
        }

        public DiscussionPostsData GetDiscussionPostsDataById(string tenantId, string discussionId)
        {
            return QueryObject<DiscussionPostsData>(
                    "select "
                    + "disc.author_email_address, disc.author_identity, disc.author_name, "
                    + "disc.closed, disc.discussion_id, disc.exclusive_owner, "
                    + "disc.forum_id, disc.subject, disc.tenant_id, "
                    + "post.author_email_address as o_posts_author_email_address, "
                    + "post.author_identity as o_posts_author_identity, "
                    + "post.author_name as o_posts_author_name, "
                    + "post.body_text as o_posts_body_text, post.changed_on as o_posts_changed_on, "
                    + "post.created_on as o_posts_created_on, "
                    + "post.discussion_id as o_posts_discussion_id, "
                    + "post.forum_id as o_posts_forum_id, post.post_id as o_posts_post_id, "
                    + "post.reply_to_post_id as o_posts_reply_to_post_id, post.subject as o_posts_subject, "
                    + "post.tenant_id as o_posts_tenant_id "
                    + "from tbl_vw_discussion as disc left outer join tbl_vw_post as post "
                    + " on disc.discussion_id = post.discussion_id "
                    + "where (disc.tenant_id = ? and disc.discussion_id = ?)",
                    new JoinOn("discussion_id", "o_posts_discussion_id"),
                    tenantId,
                    discussionId);
        }
    }
}
