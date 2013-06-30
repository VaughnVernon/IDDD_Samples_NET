using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Application.Forums.Data
{
    public class DiscussionPostsData
    {
        public string AuthorEmailAddress { get; set; }
        public string AuthorIdentity { get; set; }
        public string AuthorName { get; set; }
        public bool Closed { get; set; }
        public string DiscussionId { get; set; }
        public string ExclusiveOwner { get; set; }
        public string ForumId { get; set; }
        public ISet<PostData> Posts { get; set; }
        public string Subject { get; set; }
        public string TenantId { get; set; }
    }
}
