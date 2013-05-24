using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class DiscussionId : SaaSOvation.Common.Domain.Model.Identity
    {
        public DiscussionId(string id)
            : base(id)
        {
        }
    }
}
