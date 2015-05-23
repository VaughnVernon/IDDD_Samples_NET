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
	using System.Collections.Generic;
	using System.Linq;

	using SaaSOvation.Common.Domain.Model;
	using SaaSOvation.IdentityAccess.Domain.Model.Access;

	/// <summary>
	/// An entity representing a tenant in a multi-tenant
	/// "Identity and Access" Bounded Context.
	/// </summary>
	/// <remarks>
	/// <see cref="Person"/>, <see cref="User"/>, <see cref="Group"/>,
	/// and <see cref="Role"/> entities are each bound to a single tenant.
	/// </remarks>
	[CLSCompliant(true)]
	public class Tenant : EntityWithCompositeId
	{
		#region [ Fields and Constructor Overloads ]

		private readonly ISet<RegistrationInvitation> registrationInvitations;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tenant"/> class.
		/// </summary>
		/// <param name="tenantId">
		/// Initial value of the <see cref="TenantId"/> property.
		/// </param>
		/// <param name="name">
		/// Initial value of the <see cref="Name"/> property.
		/// </param>
		/// <param name="description">
		/// Initial value of the <see cref="Description"/> property.
		/// </param>
		/// <param name="active">
		/// Initial value of the <see cref="Active"/> property.
		/// </param>
		public Tenant(TenantId tenantId, string name, string description, bool active)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "TenentId is required.");
			AssertionConcern.AssertArgumentNotEmpty(name, "The tenant name is required.");
			AssertionConcern.AssertArgumentLength(name, 1, 100, "The name must be 100 characters or less.");
			AssertionConcern.AssertArgumentNotEmpty(description, "The tenant description is required.");
			AssertionConcern.AssertArgumentLength(description, 1, 100, "The name description be 100 characters or less.");

			this.TenantId = tenantId;
			this.Name = name;
			this.Description = description;
			this.Active = active;

			this.registrationInvitations = new HashSet<RegistrationInvitation>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tenant"/> class for a derived type,
		/// and otherwise blocks new instances from being created with an empty constructor.
		/// </summary>
		protected Tenant()
		{
		}

		#endregion

		#region [ Public Properties ]

		public TenantId TenantId { get; private set; }

		public string Name { get; private set; }

		public bool Active { get; private set; }

		public string Description { get; private set; }

		#endregion

		#region [ Command Methods which Publish Domain Events ]

		public void Activate()
		{
			if (!this.Active)
			{
				this.Active = true;
				DomainEventPublisher.Instance.Publish(new TenantActivated(this.TenantId));
			}
		}

		public void Deactivate()
		{
			if (this.Active)
			{
				this.Active = false;

				DomainEventPublisher.Instance.Publish(new TenantDeactivated(this.TenantId));
			}
		}

		public bool IsRegistrationAvailableThrough(string invitationIdentifier)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);
	
			return ((invitation != null) && invitation.IsAvailable());
		}

		public RegistrationInvitation OfferRegistrationInvitation(string description)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");
			AssertionConcern.AssertArgumentTrue(this.IsRegistrationAvailableThrough(description), "Invitation already exists.");

			RegistrationInvitation invitation = new RegistrationInvitation(this.TenantId, new Guid().ToString(), description);

			AssertionConcern.AssertStateTrue(this.registrationInvitations.Add(invitation), "The invitation should have been added.");

			return invitation;
		}

		public Group ProvisionGroup(string name, string description)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			Group group = new Group(this.TenantId, name, description);

			DomainEventPublisher.Instance.Publish(new GroupProvisioned(this.TenantId, name));

			return group;
		}

		public Role ProvisionRole(string name, string description, bool supportsNesting = false)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			Role role = new Role(this.TenantId, name, description, supportsNesting);

			DomainEventPublisher.Instance.Publish(new RoleProvisioned(this.TenantId, name));

			return role;
		}

		public RegistrationInvitation RedefineRegistrationInvitationAs(string invitationIdentifier)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);
			if (invitation != null)
			{
				invitation.RedefineAs().OpenEnded();
			}

			return invitation;
		}

		public User RegisterUser(string invitationIdentifier, string username, string password, Enablement enablement, Person person)
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			User user = null;
			if (this.IsRegistrationAvailableThrough(invitationIdentifier))
			{
				// ensure same tenant
				person.TenantId = this.TenantId;
				user = new User(this.TenantId, username, password, enablement, person);
			}

			return user;
		}

		public void WithdrawInvitation(string invitationIdentifier)
		{
			RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);
			if (invitation != null)
			{
				this.registrationInvitations.Remove(invitation);
			}
		}

		#endregion

		#region [ Additional Methods ]

		public ICollection<InvitationDescriptor> AllAvailableRegistrationInvitations()
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			return this.AllRegistrationInvitationsFor(true);
		}

		public ICollection<InvitationDescriptor> AllUnavailableRegistrationInvitations()
		{
			AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

			return this.AllRegistrationInvitationsFor(false);
		}

		/// <summary>
		/// Returns a string that represents the current entity.
		/// </summary>
		/// <returns>
		/// A unique string representation of an instance of this entity.
		/// </returns>
		public override string ToString()
		{
			const string Format = "Tenant [tenantId={0}, name={1}, description={2}, active={3}]";
			return string.Format(Format, this.TenantId, this.Name, this.Description, this.Active);
		}

		/// <summary>
		/// Gets the values which identify a <see cref="Tenant"/> entity,
		/// which are the <see cref="TenantId"/> and the <see cref="Name"/>.
		/// </summary>
		/// <returns>
		/// A sequence of values which uniquely identifies an instance of this entity.
		/// </returns>
		protected override IEnumerable<object> GetIdentityComponents()
		{
			yield return this.TenantId;
			yield return this.Name;
		}

		private List<InvitationDescriptor> AllRegistrationInvitationsFor(bool isAvailable)
		{
			return this.registrationInvitations
				.Where(x => (x.IsAvailable() == isAvailable))
				.Select(x => x.ToDescriptor())
				.ToList();
		}

		private RegistrationInvitation GetInvitation(string invitationIdentifier)
		{
			return this.registrationInvitations.FirstOrDefault(x => x.IsIdentifiedBy(invitationIdentifier));
		}

		#endregion
	}
}