namespace SaaSOvation.IdentityAccess.Tests.Infrastructure.Persisitence
{
    using System;
    using System.Collections.Generic;

    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class InMemoryTenantRepository : TenantRepository
    {
        private IDictionary<string, Tenant> repository = new Dictionary<string, Tenant>();

        public void Add(Tenant user)
        {
            if (this.repository.ContainsKey(this.keyOf(user)))
            {
                throw new ArgumentException("Duplicate key", "user");
            }

            this.repository.Add(this.keyOf(user), user);
        }

        public Tenant TenantOfId(TenantId tenantId)
        {
            return this.repository[this.keyOf(tenantId)];
        }

        private string keyOf(TenantId tenantId)
        {
            var key = tenantId.Id;
            return key;
        }

        private string keyOf(Tenant tenant)
        {
            return this.keyOf(tenant.TenantId);
        }
    }
}