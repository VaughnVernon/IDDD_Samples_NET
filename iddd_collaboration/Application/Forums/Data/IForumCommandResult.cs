using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Application.Forums.Data
{
    public interface IForumCommandResult
    {
        void SetResultingForumId(string forumId);

        void SetResultingDiscussionId(string discussionId);
    }
}
