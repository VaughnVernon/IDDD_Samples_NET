using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.AgilePM.Domain.Model.Products.Sprints;
using SaaSOvation.AgilePM.Domain.Model.Products.Releases;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public interface IBacklogItemRepository
    {
        ICollection<BacklogItem> GetAllComittedTo(TenantId tenantId, SprintId sprintId);

        ICollection<BacklogItem> GetAllScheduledFor(TenantId tenantId, ReleaseId releaseId);

        ICollection<BacklogItem> GetAllOutstanding(TenantId tenantId, ProductId productId);

        ICollection<BacklogItem> GetAll(TenantId tenantId, ProductId productId);

        BacklogItem Get(TenantId tenantId, BacklogItemId backlogItemId);

        BacklogItemId GetNextIdentity();

        void Remove(BacklogItem backlogItem);

        void RemoveAll(IEnumerable<BacklogItem> backlogItems);

        void Save(BacklogItem backlogItem);

        void SaveAll(IEnumerable<BacklogItem> backlogItems);
    }
}
