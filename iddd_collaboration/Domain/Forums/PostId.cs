using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public class PostId : SaaSOvation.Common.Domain.Identity
    {
        public PostId(string id)
            : base(id)
        {
        }
    }
}
