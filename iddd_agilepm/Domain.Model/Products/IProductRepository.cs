using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllByTenant(Tenants.TenantId tenantId);

        ProductId GetNextIdentity();

        Product GetByDiscussionInitiationId(Tenants.TenantId tenantId, string discussionInitiationId);

        Product Get(Tenants.TenantId tenantId, Products.ProductId productId);

        void Remove(Product product);

        void RemoveAll(IEnumerable<Product> products);

        void Save(Product product);

        void SaveAll(IEnumerable<Product> products);
    }
}
