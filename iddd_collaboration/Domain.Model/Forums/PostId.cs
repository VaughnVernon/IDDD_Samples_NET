using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public class PostId : SaaSOvation.Common.Domain.Model.Identity
    {
        public PostId(string id)
            : base(id)
        {
        }
    }
}
