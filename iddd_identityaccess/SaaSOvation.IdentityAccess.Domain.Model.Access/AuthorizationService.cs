namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class AuthorizationService : AssertionConcern
    {
        public AuthorizationService(
                UserRepository userRepository,
                GroupRepository groupRepository,
                RoleRepository roleRepository)
        {
            this.GroupRepository = groupRepository;
            this.RoleRepository = roleRepository;
            this.UserRepository = userRepository;
        }

        private GroupRepository GroupRepository { get; set; }

        private RoleRepository RoleRepository { get; set; }

        private UserRepository UserRepository { get; set; }

        public bool IsUserInRole(TenantId tenantId, string username, string roleName)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(username, "Username must not be provided.");
            AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

            User user = this.UserRepository.UserWithUsername(tenantId, username);

            return user == null ? false : this.IsUserInRole(user, roleName);
        }

        public bool IsUserInRole(User user, string roleName)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

            bool authorized = false;

            if (user.Enabled)
            {
                Role role = this.RoleRepository.RoleNamed(user.TenantId, roleName);

                if (role != null)
                {
                    GroupMemberService groupMemberService =
                            new GroupMemberService(
                                    this.UserRepository,
                                    this.GroupRepository);

                    authorized = role.IsInRole(user, groupMemberService);
                }
            }

            return authorized;
        }
    }
}
