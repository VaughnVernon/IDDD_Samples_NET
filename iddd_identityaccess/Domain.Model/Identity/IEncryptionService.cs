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

	/// <summary>
	/// Contract for a domain service which encrypts
	/// a plain text string to use as a password.
	/// </summary>
	[CLSCompliant(true)]
	public interface IEncryptionService
	{
		/// <summary>
		/// Encrypts a given plain text string and returns the cipher text.
		/// Typically, the returned value should be a one-way hash of the
		/// given <paramref name="plainTextValue"/>, and not cipher text
		/// which could be decrypted with a key.
		/// </summary>
		/// <param name="plainTextValue">
		/// A plain text string representing a password,
		/// to be hashed before it is stored.
		/// </param>
		/// <returns>
		/// A string which is one-way cryptographic hash
		/// of the given <paramref name="plainTextValue"/>.
		/// </returns>
		string EncryptedValue(string plainTextValue);
	}
}