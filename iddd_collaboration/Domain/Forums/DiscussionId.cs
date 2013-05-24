using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public class DiscussionId : SaaSOvation.Common.Domain.Identity
    {
        public DiscussionId(string id)
            : base(id)
        {
        }
    }
}
