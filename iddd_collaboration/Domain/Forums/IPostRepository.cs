using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Forums
{
    public interface IPostRepository
    {
        Post Get(Tenants.Tenant tenantId, PostId postId);

        PostId GetNextIdentity();

        void Save(Post post);
    }
}
