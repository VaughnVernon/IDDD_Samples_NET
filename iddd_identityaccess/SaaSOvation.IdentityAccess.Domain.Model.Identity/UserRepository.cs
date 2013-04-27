namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public interface UserRepository
    {
        void Add(User user);

        User UserFromAuthenticCredentials(Identity<Tenant> tenantId, string username, string encryptedPassword);

        User UserWithUsername(Identity<Tenant> tenantId, string username);
    }
}
