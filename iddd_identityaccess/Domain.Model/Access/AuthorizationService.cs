// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
	using System;

	using SaaSOvation.Common.Domain.Model;
	using SaaSOvation.IdentityAccess.Domain.Model.Identity;

	/// <summary>
	/// A domain service providing methods to determine
	/// whether a <see cref="User"/> has a <see cref="Role"/>.
	/// </summary>
	[CLSCompliant(true)]
	public class AuthorizationService
	{
		#region [ ReadOnly Fields and Constructor ]

		private readonly IUserRepository userRepository;
		private readonly IGroupRepository groupRepository;
		private readonly IRoleRepository roleRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationService"/> class.
		/// </summary>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="groupRepository">
		/// An instance of <see cref="IGroupRepository"/> to use internally.
		/// </param>
		/// <param name="roleRepository">
		/// An instance of <see cref="IRoleRepository"/> to use internally.
		/// </param>
		public AuthorizationService(
			IUserRepository userRepository,
			IGroupRepository groupRepository,
			IRoleRepository roleRepository)
		{
			this.groupRepository = groupRepository;
			this.roleRepository = roleRepository;
			this.userRepository = userRepository;
		}

		#endregion

		public bool IsUserInRole(TenantId tenantId, string username, string roleName)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
			AssertionConcern.AssertArgumentNotEmpty(username, "Username must not be provided.");
			AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

			User user = this.userRepository.UserWithUsername(tenantId, username);
			return ((user != null) && this.IsUserInRole(user, roleName));
		}

		public bool IsUserInRole(User user, string roleName)
		{
			AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
			AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

			bool authorized = false;
			if (user.IsEnabled)
			{
				Role role = this.roleRepository.RoleNamed(user.TenantId, roleName);
				if (role != null)
				{
					authorized = role.IsInRole(user, new GroupMemberService(this.userRepository, this.groupRepository));
				}
			}

			return authorized;
		}
	}
}