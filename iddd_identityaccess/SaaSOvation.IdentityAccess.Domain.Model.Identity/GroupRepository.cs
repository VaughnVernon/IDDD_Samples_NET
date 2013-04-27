namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public interface GroupRepository
    {
        Group GroupNamed(Identity<Tenant> identity, string p);
    }
}
