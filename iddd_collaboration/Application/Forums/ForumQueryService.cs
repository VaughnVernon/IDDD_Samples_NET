using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Port.Adapters.Persistence;

using SaaSOvation.Collaboration.Application.Forums.Data;

namespace SaaSOvation.Collaboration.Application.Forums
{
    public class ForumQueryService : AbstractQueryService
    {
        public ForumQueryService(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public IList<ForumData> GetAllForumsDataByTenant(string tenantId)
        {
            return QueryObjects<ForumData>(
                    "select * from tbl_vw_forum where tenant_id = ?",
                    new JoinOn(),
                    tenantId);
        }

        public ForumData GetForumDataById(string tenantId, string forumId)
        {
            return QueryObject<ForumData>(
                    "select * from tbl_vw_forum where tenant_id = ? and forum_id = ?",
                    new JoinOn(),
                    tenantId,
                    forumId);
        }

        public ForumDiscussionsData GetForumDiscussionsDataById(string tenantId, string forumId)
        {
            return QueryObject<ForumDiscussionsData>(
                    "select "
                    + "forum.closed, forum.creator_email_address, forum.creator_identity, "
                    + "forum.creator_name, forum.description, forum.exclusive_owner, forum.forum_id, "
                    + "forum.moderator_email_address, forum.moderator_identity, forum.moderator_name, "
                    + "forum.subject, forum.tenant_id, "
                    + "disc.author_email_address as o_discussions_author_email_address, "
                    + "disc.author_identity as o_discussions_author_identity, "
                    + "disc.author_name as o_discussions_author_name, "
                    + "disc.closed as o_discussions_closed, "
                    + "disc.discussion_id as o_discussions_discussion_id, "
                    + "disc.exclusive_owner as o_discussions_exclusive_owner, "
                    + "disc.forum_id as o_discussions_forum_id, "
                    + "disc.subject as o_discussions_subject, "
                    + "disc.tenant_id as o_discussions_tenant_id "
                    + "from tbl_vw_forum as forum left outer join tbl_vw_discussion as disc "
                    + " on forum.forum_id = disc.forum_id "
                    + "where (forum.tenant_id = ? and forum.forum_id = ?)",
                    new JoinOn("forum_id", "o_discussions_forum_id"),
                    tenantId,
                    forumId);
        }

        public string GetForumIdByExclusiveOwner(string tenantId, string exclusiveOwner)
        {
            return QueryString(
                    "select forum_id from tbl_vw_forum where tenant_id = ? and exclusive_owner = ?",
                    tenantId,
                    exclusiveOwner);
        }
    }
}
