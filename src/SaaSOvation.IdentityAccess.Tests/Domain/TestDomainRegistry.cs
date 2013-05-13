namespace SaaSOvation.IdentityAccess.Tests.Domain
{
    using SaaSOvation.IdentityAccess.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Access;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;
    using SaaSOvation.IdentityAccess.Tests.Infrastructure.Persisitence;

    /// <summary>
    /// Test domain registry which is used in substitution to <see cref="DomainRegistry"/> until IoC will not be implemented.
    /// </summary>
    public static class TestDomainRegistry
    {
        private static TenantProvisioningService tenantProvisioningService;

        private static RoleRepository roleRepository;

        private static UserRepository userRepository;

        private static TenantRepository tenantRepository;

        public static TenantProvisioningService TenantProvisioningService
        {
            get
            {
                if (tenantProvisioningService == null)
                {
                    tenantProvisioningService = new TenantProvisioningService(TenantRepository, UserRepository, RoleRepository);
                }

                return tenantProvisioningService;
            }
        }

        public static RoleRepository RoleRepository
        {
            get { return roleRepository ?? (roleRepository = new InMemoryRoleRepository()); }
        }

        public static UserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new InMemoryUserRepository()); }
        }

        public static TenantRepository TenantRepository
        {
            get { return tenantRepository ?? (tenantRepository = new InMemoryTenantRepository()); }
        }

        public static void Reset()
        {
            roleRepository = null;
            userRepository = null;
            tenantRepository = null;
        }
    }
}
