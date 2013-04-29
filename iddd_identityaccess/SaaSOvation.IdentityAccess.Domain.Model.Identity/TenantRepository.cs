namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public interface TenantRepository
    {
        void Add(Tenant tenant);

        Tenant TenantOfId(TenantId tenantId);
    }
}
