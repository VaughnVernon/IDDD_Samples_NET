using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Application.Forums.Data
{
    public class ForumDiscussionsData
    {
        public bool Closed { get; set; }
        public string CreatorEmailAddress { get; set; }
        public string CreatorIdentity { get; set; }
        public string CreatorName { get; set; }
        public string Description { get; set; }
        public string ExclusiveOwner { get; set; }
        public ISet<DiscussionData> Discussions { get; set; }
        public string ForumId { get; set; }
        public string ModeratorEmailAddress { get; set; }
        public string ModeratorIdentity { get; set; }
        public string ModeratorName { get; set; }
        public string Subject { get; set; }
        public string TenantId { get; set; }
    }
}
