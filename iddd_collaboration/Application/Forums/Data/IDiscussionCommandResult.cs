using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Application.Forums.Data
{
    public interface IDiscussionCommandResult
    {
        void SetResultingDiscussionId(string discussionId);

        void SetResultingPostId(string postId);

        void SetRresultingInReplyToPostId(string replyToPostId);
    }
}
