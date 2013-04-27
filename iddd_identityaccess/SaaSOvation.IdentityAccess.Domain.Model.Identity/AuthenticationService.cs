namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class AuthenticationService : AssertionConcern
    {
        public AuthenticationService(
                TenantRepository tenantRepository,
                UserRepository userRepository,
                EncryptionService encryptionService)
        {
            this.EncryptionService = encryptionService;
            this.TenantRepository = tenantRepository;
            this.UserRepository = userRepository;
        }

        private EncryptionService EncryptionService { get; set; }

        private TenantRepository TenantRepository { get; set; }

        private UserRepository UserRepository { get; set; }

        public UserDescriptor Authenticate(
                Identity<Tenant> tenantId,
                string username,
                string password)
        {
            this.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
            this.AssertArgumentNotEmpty(username, "Username must be provided.");
            this.AssertArgumentNotEmpty(password, "Password must be provided.");

            UserDescriptor userDescriptor = UserDescriptor.NullDescriptorInstance();

            Tenant tenant = this.TenantRepository.TenantOfId(tenantId);

            if (tenant != null && tenant.Active)
            {
                String encryptedPassword = this.EncryptionService.EncryptedValue(password);

                User user =
                        this.UserRepository
                            .UserFromAuthenticCredentials(
                                tenantId,
                                username,
                                encryptedPassword);

                if (user != null && user.Enabled)
                {
                    userDescriptor = user.UserDescriptor;
                }
            }

            return userDescriptor;
        }
    }
}
