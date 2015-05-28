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

namespace SaaSOvation.IdentityAccess.Domain.Model
{
	using System;

	using SaaSOvation.IdentityAccess.Domain.Model.Identity;

	/// <summary>
	/// Holds static references to domain services
	/// which would normally be configured by an
	/// Inversion of Control container.
	/// </summary>
	[CLSCompliant(true)]
	public static class DomainRegistry
	{
		/// <summary>
		/// Gets the instance of <see cref="IEncryptionService"/> to use.
		/// </summary>
		public static IEncryptionService EncryptionService
		{
			get
			{
				// this is not a desirable dependency since it
				// references port adapters, but it doesn't
				// require an IoC container
				return new Infrastructure.Services.MD5EncryptionService();
			}
		}

		/// <summary>
		/// Gets an instance of a domain service which generates
		/// passwords and evaluates passwords for strength.
		/// </summary>
		public static PasswordService PasswordService
		{
			get { return new PasswordService(); }
		}
	}
}