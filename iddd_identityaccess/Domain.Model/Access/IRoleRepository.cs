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

	using SaaSOvation.IdentityAccess.Domain.Model.Identity;

	/// <summary>
	/// Contract for a collection-oriented repository of <see cref="Role"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
	/// Subsequent changes to any stored <see cref="Role"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	[CLSCompliant(true)]
	public interface IRoleRepository
	{
		/// <summary>
		/// Stores a given <see cref="Role"/> in the repository.
		/// </summary>
		/// <param name="role">
		/// The instance of <see cref="Role"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="Role"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
		void Add(Role role);

		/// <summary>
		/// Retrieves a <see cref="Role"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="Role"/> may belong, corresponding
		/// to its <see cref="Role.TenantId"/>.
		/// </param>
		/// <param name="roleName">
		/// The name of a <see cref="Role"/>, matching
		/// the value of its <see cref="Role.Name"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Role"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
		Role RoleNamed(TenantId tenantId, string roleName);
	}
}