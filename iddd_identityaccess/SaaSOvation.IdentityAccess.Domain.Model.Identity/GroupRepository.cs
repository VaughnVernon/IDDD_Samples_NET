namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public interface GroupRepository
    {
        Group GroupNamed(TenantId identity, string p);
    }
}
