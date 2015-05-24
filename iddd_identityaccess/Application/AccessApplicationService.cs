namespace SaaSOvation.IdentityAccess.Application
{
	using System;

	using SaaSOvation.IdentityAccess.Application.Commands;
	using SaaSOvation.IdentityAccess.Domain.Model.Access;
	using SaaSOvation.IdentityAccess.Domain.Model.Identity;

	[CLSCompliant(true)]
	public sealed class AccessApplicationService
	{
		private readonly IGroupRepository groupRepository;
		private readonly IRoleRepository roleRepository;
		private readonly ITenantRepository tenantRepository;
		private readonly IUserRepository userRepository;

		public AccessApplicationService(
			IGroupRepository groupRepository,
			IRoleRepository roleRepository,
			ITenantRepository tenantRepository,
			IUserRepository userRepository)
		{
			this.groupRepository = groupRepository;
			this.roleRepository = roleRepository;
			this.tenantRepository = tenantRepository;
			this.userRepository = userRepository;
		}

		public void AssignUserToRole(AssignUserToRoleCommand command)
		{
			var tenantId = new TenantId(command.TenantId);
			var user = this.userRepository.UserWithUsername(tenantId, command.Username);
			if (user != null)
			{
				var role = this.roleRepository.RoleNamed(tenantId, command.RoleName);
				if (role != null)
				{
					role.AssignUser(user);
				}
			}
		}

		public bool IsUserInRole(string tenantId, string userName, string roleName)
		{
			return UserInRole(tenantId, userName, roleName) != null;
		}

		public User UserInRole(string tenantId, string userName, string roleName)
		{
			var id = new TenantId(tenantId);
			var user = this.userRepository.UserWithUsername(id, userName);
			if (user != null)
			{
				var role = this.roleRepository.RoleNamed(id, roleName);
				if (role != null)
				{
					if (role.IsInRole(user, new GroupMemberService(this.userRepository, this.groupRepository)))
					{
						return user;
					}
				}
			}

			return null;
		}

		public void ProvisionRole(ProvisionRoleCommand command)
		{
			var tenantId = new TenantId(command.TenantId);
			var tenant = this.tenantRepository.Get(tenantId);
			var role = tenant.ProvisionRole(command.RoleName, command.Description, command.SupportsNesting);
			this.roleRepository.Add(role);
		}
	}
}