using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Collaboration.Domain.Model.Forums
{
    public interface IDiscussionRepository
    {
        Discussion Get(Tenants.Tenant tenantId, DiscussionId discussionId);

        DiscussionId GetNextIdentity();

        void Save(Discussion discussion);
    }
}
