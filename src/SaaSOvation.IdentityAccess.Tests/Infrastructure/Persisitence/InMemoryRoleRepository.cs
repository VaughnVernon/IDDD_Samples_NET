namespace SaaSOvation.IdentityAccess.Tests.Infrastructure.Persisitence
{
    using System;
    using System.Collections.Generic;

    using SaaSOvation.IdentityAccess.Domain.Model.Access;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class InMemoryRoleRepository : RoleRepository
    {
        private IDictionary<string, Role> repository = new Dictionary<string, Role>();

        public void Add(Role role)
        {
            if (this.repository.ContainsKey(this.keyOf(role)))
            {
                throw new ArgumentException("Duplicate key", "role");
            }

            this.repository.Add(this.keyOf(role), role);
        }

        public Role RoleNamed(TenantId identity, string roleName)
        {
            return this.repository[keyOf(identity, roleName)];
        }

        private string keyOf(TenantId tenantId, string roleName)
        {
            var key = string.Format("{0}#{1}", tenantId.Id, roleName);
            return key;
        }

        private string keyOf(Role role)
        {
            return this.keyOf(role.TenantId, role.Name);
        }
    }
}