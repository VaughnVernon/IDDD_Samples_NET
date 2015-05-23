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

	using SaaSOvation.Common.Domain.Model;

	/// <summary>
	/// An entity representing a person associated with
	/// a <see cref="Domain.Model.Identity.User"/>, and
	/// identified by that <see cref="User"/>'s
	/// <see cref="Domain.Model.Identity.User.Username"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This entity is not an aggregate, but is used to compose
	/// the <see cref="Domain.Model.Identity.User"/> aggregate.
	/// Is it assumed that all users are persons,
	/// and that there are not other kinds of users?
	/// </para>
	/// <para>
	/// Because a <see cref="Person"/> instance is identified
	/// by the <see cref="Domain.Model.Identity.User.Username"/>
	/// of the associated <see cref="User"/>, no more than
	/// one person can be associated with a single user.
	/// </para>
	/// </remarks>
	[CLSCompliant(true)]
	public class Person : EntityWithCompositeId
	{
		#region [ Fields and Constructor Overloads ]

		private TenantId tenantId;
		private FullName name;
		private ContactInformation contactInformation;

		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		/// <param name="tenantId">
		/// Initial value of the <see cref="TenantId"/> property.
		/// </param>
		/// <param name="name">
		/// Initial value of the <see cref="Name"/> property.
		/// </param>
		/// <param name="contactInformation">
		/// Initial value of the <see cref="ContactInformation"/> property.
		/// </param>
		public Person(TenantId tenantId, FullName name, ContactInformation contactInformation)
		{
			// Defer validation to the property setters.
			this.ContactInformation = contactInformation;
			this.Name = name;
			this.TenantId = tenantId;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class for a derived type,
		/// and otherwise blocks new instances from being created with an empty constructor.
		/// </summary>
		protected Person()
		{
		}

		#endregion

		#region [ Public Properties ]

		public TenantId TenantId
		{
			get
			{
				return this.tenantId;
			}

			internal set
			{
				AssertionConcern.AssertArgumentNotNull(value, "The tenantId is required.");
				this.tenantId = value;
			}
		}

		public FullName Name
		{
			get
			{
				return this.name;
			}

			private set
			{
				AssertionConcern.AssertArgumentNotNull(value, "The person name is required.");
				this.name = value;
			}
		}

		public User User { get; internal set; }

		public ContactInformation ContactInformation
		{
			get
			{
				return this.contactInformation;
			}

			private set
			{
				AssertionConcern.AssertArgumentNotNull(value, "The person contact information is required.");
				this.contactInformation = value;
			}
		}

		public EmailAddress EmailAddress
		{
			get { return this.ContactInformation.EmailAddress; }
		}

		#endregion

		#region [ Command Methods which Publish Domain Events ]

		public void ChangeContactInformation(ContactInformation newContactInformation)
		{
			// Defer validation to the property setter.
			this.ContactInformation = newContactInformation;

			DomainEventPublisher
				.Instance
				.Publish(new PersonContactInformationChanged(
						this.TenantId,
						this.User.Username,
						this.ContactInformation));
		}

		public void ChangeName(FullName newName)
		{
			// Defer validation to the property setter.
			this.Name = newName;

			DomainEventPublisher
				.Instance
				.Publish(new PersonNameChanged(
						this.TenantId,
						this.User.Username,
						this.Name));
		}

		#endregion

		#region [ Additional Methods ]

		/// <summary>
		/// Returns a string that represents the current entity.
		/// </summary>
		/// <returns>
		/// A unique string representation of an instance of this entity.
		/// </returns>
		public override string ToString()
		{
			const string Format = "Person [tenantId={0}, name={1}, contactInformation={2}]";
			return string.Format(Format, this.TenantId, this.Name, this.ContactInformation);
		}

		/// <summary>
		/// Gets the values which identify a <see cref="Person"/> entity,
		/// which are the <see cref="TenantId"/> and the unique
		/// <see cref="Domain.Model.Identity.User.Username"/>
		/// of the associated <see cref="User"/>.
		/// </summary>
		/// <returns>
		/// A sequence of values which uniquely identifies an instance of this entity.
		/// </returns>
		protected override IEnumerable<object> GetIdentityComponents()
		{
			yield return this.TenantId;
			yield return this.User.Username;
		}

		#endregion
	}
}