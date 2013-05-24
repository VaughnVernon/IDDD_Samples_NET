using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public class ForumId : SaaSOvation.Common.Domain.Identity
    {
        public ForumId(string id)
            : base(id)
        {
        }
    }
}
