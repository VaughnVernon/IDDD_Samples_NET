using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Tenants;

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    public interface IProductOwnerRepository
    {
        ICollection<ProductOwner> GetAllProductOwners(TenantId tenantId);

        ProductOwner Get(TenantId tenantId, string userName);

        void Remove(ProductOwner owner);

        void RemoveAll(IEnumerable<ProductOwner> owners);

        void Save(ProductOwner owner);

        void SaveAll(IEnumerable<ProductOwner> owners);
    }
}
