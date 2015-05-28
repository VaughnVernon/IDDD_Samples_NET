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
	using SaaSOvation.IdentityAccess.Domain.Model.Access;

	/// <summary>
	/// <para>
	/// A domain service encapsulating the process to create
	/// and store a new <see cref="Tenant"/> instance.
	/// </para>
	/// <para>
	/// This operation is complex, involving the creation
	/// of a <see cref="User"/> and <see cref="Role"/>
	/// for default administration of the new tenant,
	/// and publication of requisite domain events.
	/// </para>
	/// </summary>
	[CLSCompliant(true)]
	public class TenantProvisioningService
	{
		#region [ Fields and Constructor ]

		private readonly IRoleRepository roleRepository;
		private readonly ITenantRepository tenantRepository;
		private readonly IUserRepository userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="TenantProvisioningService"/> class.
		/// </summary>
		/// <param name="tenantRepository">
		/// An instance of <see cref="ITenantRepository"/> to use internally.
		/// </param>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="roleRepository">
		/// An instance of <see cref="IRoleRepository"/> to use internally.
		/// </param>
		public TenantProvisioningService(
			ITenantRepository tenantRepository,
			IUserRepository userRepository,
			IRoleRepository roleRepository)
		{
			this.roleRepository = roleRepository;
			this.tenantRepository = tenantRepository;
			this.userRepository = userRepository;
		}

		#endregion

		#region [ Public Method ProvisionTenant() ]

		/// <summary>
		/// Creates a new <see cref="Tenant"/>, stores it in
		/// its <see cref="ITenantRepository"/> instance, and
		/// publishes a <see cref="TenantProvisioned"/> event,
		/// along with requisite domain events for the creation
		/// of a <see cref="User"/> and <see cref="Role"/>
		/// for default administration of the new tenant.
		/// Refer to remarks for details.
		/// </summary>
		/// <param name="tenantName">
		/// The <see cref="Tenant.Name"/> of the new tenant.
		/// </param>
		/// <param name="tenantDescription">
		/// The <see cref="Tenant.Description"/> of the new tenant.
		/// </param>
		/// <param name="administorName">
		/// The <see cref="Person.Name"/> of the
		/// default administrator for the new tenant.
		/// </param>
		/// <param name="emailAddress">
		/// The <see cref="Person.EmailAddress"/> of the
		/// default administrator for the new tenant.
		/// </param>
		/// <param name="postalAddress">
		/// The <see cref="ContactInformation.PostalAddress"/>
		/// of the default administrator for the new tenant.
		/// </param>
		/// <param name="primaryTelephone">
		/// The <see cref="ContactInformation.PrimaryTelephone"/>
		/// of the default administrator for the new tenant.
		/// </param>
		/// <param name="secondaryTelephone">
		/// The <see cref="ContactInformation.SecondaryTelephone"/>
		/// of the default administrator for the new tenant.
		/// </param>
		/// <returns>
		/// The newly registered <see cref="Tenant"/>,
		/// which has already been added to the internal
		/// <see cref="ITenantRepository"/> instance.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The events published, in order, are:
		/// </para>
		/// <list type="bullet">
		/// <item><description><see cref="UserRegistered"/></description></item>
		/// <item><description><see cref="RoleProvisioned"/></description></item>
		/// <item><description><see cref="UserAssignedToRole"/></description></item>
		/// <item><description><see cref="TenantAdministratorRegistered"/></description></item>
		/// <item><description><see cref="TenantProvisioned"/></description></item>
		/// </list>
		/// </remarks>
		public Tenant ProvisionTenant(
			string tenantName,
			string tenantDescription,
			FullName administorName,
			EmailAddress emailAddress,
			PostalAddress postalAddress,
			Telephone primaryTelephone,
			Telephone secondaryTelephone)
		{
			try
			{
				// must be active to register admin
				Tenant tenant = new Tenant(this.tenantRepository.GetNextIdentity(), tenantName, tenantDescription, true);

				// Since this is a new entity, add it to
				// the collection-oriented repository.
				// Subsequent changes to the entity
				// are implicitly persisted.
				this.tenantRepository.Add(tenant);

				// Creates user and role entities and stores them
				// in their respective repositories, and publishes
				// domain events UserRegistered, RoleProvisioned,
				// UserAssignedToRole, and TenantAdministratorRegistered.
				this.RegisterAdministratorFor(
					tenant,
					administorName,
					emailAddress,
					postalAddress,
					primaryTelephone,
					secondaryTelephone);

				DomainEventPublisher
					.Instance
					.Publish(new TenantProvisioned(
							tenant.TenantId));

				return tenant;
			}
			catch (Exception e)
			{
				throw new InvalidOperationException(
					string.Concat("Cannot provision tenant because: ", e.Message), e);
			}
		}

		#endregion

		#region [ Private Method used by ProvisionTenant() ]

		private void RegisterAdministratorFor(
			Tenant tenant,
			FullName administorName,
			EmailAddress emailAddress,
			PostalAddress postalAddress,
			Telephone primaryTelephone,
			Telephone secondaryTelephone)
		{
			RegistrationInvitation invitation = tenant.OfferRegistrationInvitation("init").OpenEnded();
			string strongPassword = new PasswordService().GenerateStrongPassword();

			// Publishes domain event UserRegistered.
			User admin = tenant.RegisterUser(
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

			// Since this is a new entity, add it to
			// the collection-oriented repository.
			// Subsequent changes to the entity
			// are implicitly persisted.
			this.userRepository.Add(admin);

			// Publishes domain event RoleProvisioned.
			Role adminRole = tenant.ProvisionRole(
				"Administrator",
				string.Format("Default {0} administrator.", tenant.Name));

			// Publishes domain event UserAssignedToRole,
			// but not GroupUserAdded because the group
			// reference held by the role is an "internal" group.
			adminRole.AssignUser(admin);

			// Since this is a new entity, add it to
			// the collection-oriented repository.
			// Subsequent changes to the entity
			// are implicitly persisted.
			this.roleRepository.Add(adminRole);

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

		#endregion
	}
}