namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Access;

    public class TenantProvisioningService
    {
        public TenantProvisioningService(
                TenantRepository tenantRepository,
                UserRepository userRepository,
                RoleRepository roleRepository)
        {
            this.RoleRepository = roleRepository;
            this.TenantRepository = tenantRepository;
            this.UserRepository = userRepository;
        }

        private RoleRepository RoleRepository { get; set; }
        private TenantRepository TenantRepository { get; set; }
        private UserRepository UserRepository { get; set; }

        public Tenant ProvisionTenant(
                String tenantName,
                String tenantDescription,
                FullName administorName,
                EmailAddress emailAddress,
                PostalAddress postalAddress,
                Telephone primaryTelephone,
                Telephone secondaryTelephone)
        {
            try
            {
                // must be active to register admin
                Tenant tenant = new Tenant(tenantName, tenantDescription, true);

                this.TenantRepository.Add(tenant);

                this.RegisterAdministratorFor(
                        tenant,
                        administorName,
                        emailAddress,
                        postalAddress,
                        primaryTelephone,
                        secondaryTelephone);

                DomainEventPublisher.Instance.Publish(new TenantProvisioned(tenant.TenantId));

                return tenant;

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                        "Cannot provision tenant because: "
                        + e.Message);
            }
        }

        private void RegisterAdministratorFor(
                Tenant tenant,
                FullName administorName,
                EmailAddress emailAddress,
                PostalAddress postalAddress,
                Telephone primaryTelephone,
                Telephone secondaryTelephone)
        {
            RegistrationInvitation invitation =
                    tenant.OfferRegistrationInvitation("init").OpenEnded();

            String strongPassword = new PasswordService().GenerateStrongPassword();

            User admin =
                tenant.RegisterUser(
                        invitation.InvitationId,
                        "admin",
                        strongPassword,
                        Enablement.IndefiniteEnablement(),
                        new Person(
                                tenant.TenantId,
                                administorName,
                                new ContactInformation(
                                        emailAddress,
                                        postalAddress,
                                        primaryTelephone,
                                        secondaryTelephone)));

            tenant.WithdrawInvitation(invitation.InvitationId);

            this.UserRepository.Add(admin);

            Role adminRole =
                tenant.ProvisionRole(
                        "Administrator",
                        "Default " + tenant.Name + " administrator.");

            adminRole.AssignUser(admin);

            this.RoleRepository.Add(adminRole);

            DomainEventPublisher
                .Instance
                .Publish(new TenantAdministratorRegistered(
                            tenant.TenantId,
                            tenant.Name,
                            administorName,
                            emailAddress,
                            admin.Username,
                            strongPassword));
        }
    }
}
