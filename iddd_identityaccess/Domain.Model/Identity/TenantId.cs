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
	/// A value object derived from <see cref="Identity"/>
	/// which identifies a <see cref="Tenant"/>,
	/// </summary>
	/// <remarks>
	/// This is the only implementation of <see cref="IIdentity"/>
	/// in the "Identity and Access" Bounded Context.
	/// </remarks>
	[CLSCompliant(true), Serializable]
	public sealed class TenantId : Identity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TenantId"/> class
		/// with a new <see cref="Guid"/> assigned to the value of the
		/// base <see cref="IIdentity.Id"/> property.
		/// </summary>
		public TenantId()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TenantId"/> class
		/// using a given <paramref name="id"/> string.
		/// </summary>
		/// <param name="id">
		/// Initial value of the base <see cref="IIdentity.Id"/> property.
		/// </param>
		public TenantId(string id)
			: base(id)
		{
		}
	}
}