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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
	using System;

	using SaaSOvation.Common.Domain.Model;

	/// <summary>
	/// A domain service providing a method
	/// to authenticate a <see cref="User"/>.
	/// </summary>
	[CLSCompliant(true)]
	public class AuthenticationService
	{
		#region [ ReadOnly Fields and Constructor ]

		private readonly ITenantRepository tenantRepository;
		private readonly IUserRepository userRepository;
		private readonly IEncryptionService encryptionService;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticationService"/> class.
		/// </summary>
		/// <param name="tenantRepository">
		/// An instance of <see cref="ITenantRepository"/> to use internally.
		/// </param>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="encryptionService">
		/// An instance of <see cref="IEncryptionService"/> to use internally.
		/// </param>
		public AuthenticationService(
			ITenantRepository tenantRepository,
			IUserRepository userRepository,
			IEncryptionService encryptionService)
		{
			this.encryptionService = encryptionService;
			this.tenantRepository = tenantRepository;
			this.userRepository = userRepository;
		}

		#endregion

		/// <summary>
		/// Authenticates a <see cref="User"/> given the
		/// <paramref name="tenantId"/>, <paramref name="username"/>,
		/// and <paramref name="password"/>.
		/// </summary>
		/// <param name="tenantId">
		/// A <see cref="TenantId"/> identifying a <see cref="Tenant"/>
		/// with which a <see cref="User"/> is associated.
		/// </param>
		/// <param name="username">
		/// The username to authenticate.
		/// </param>
		/// <param name="password">
		/// The password to authenticate.
		/// </param>
		/// <returns>
		/// A <see cref="UserDescriptor"/> of the authenticated user
		/// if the user can be authenticated; otherwise, null reference
		/// in the username and password do not match an enabled
		/// <see cref="User"/> for an active <see cref="Tenant"/>.
		/// </returns>
		public UserDescriptor Authenticate(TenantId tenantId, string username, string password)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
			AssertionConcern.AssertArgumentNotEmpty(username, "Username must be provided.");
			AssertionConcern.AssertArgumentNotEmpty(password, "Password must be provided.");

			UserDescriptor userDescriptor = UserDescriptor.NullDescriptorInstance();
			Tenant tenant = this.tenantRepository.Get(tenantId);
			if ((tenant != null) && tenant.Active)
			{
				string encryptedPassword = this.encryptionService.EncryptedValue(password);
				User user = this.userRepository.UserFromAuthenticCredentials(tenantId, username, encryptedPassword);
				if ((user != null) && user.IsEnabled)
				{
					userDescriptor = user.UserDescriptor;
				}
			}

			return userDescriptor;
		}
	}
}