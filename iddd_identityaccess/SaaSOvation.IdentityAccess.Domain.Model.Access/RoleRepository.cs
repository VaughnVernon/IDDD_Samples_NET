namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public interface RoleRepository
    {
        void Add(Role role);

        Role RoleNamed(Identity<Tenant> identity, string roleName);
    }
}
