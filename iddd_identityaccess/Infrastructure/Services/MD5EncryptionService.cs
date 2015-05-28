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

namespace SaaSOvation.IdentityAccess.Infrastructure.Services
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	using SaaSOvation.Common.Domain.Model;
	using SaaSOvation.IdentityAccess.Domain.Model.Identity;

	/// <summary>
	/// Implementation of <see cref="IEncryptionService"/>
	/// using an <see cref="MD5"/> hasher to create a
	/// one-way hash of a plain text string.
	/// </summary>
	[CLSCompliant(true)]
	public class MD5EncryptionService : IEncryptionService
	{
		/// <summary>
		/// Creates a one-way MD5 has of a given plain text string.
		/// </summary>
		/// <param name="plainTextValue">
		/// A plain text string to be hashed.
		/// </param>
		/// <returns>
		/// The one-way MD5 has of a given <paramref name="plainTextValue"/>.
		/// </returns>
		public string EncryptedValue(string plainTextValue)
		{
			AssertionConcern.AssertArgumentNotEmpty(plainTextValue, "Plain text value to encrypt must be provided.");

			StringBuilder encryptedValue = new StringBuilder();
			MD5 hasher = MD5.Create();
			byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(plainTextValue));

			foreach (byte t in data)
			{
				// The format string indicates "hexadecimal with a precision of two digits"
				// https://msdn.microsoft.com/en-us/library/dwhawy9k%28v=vs.110%29.aspx
				encryptedValue.Append(t.ToString("x2"));
			}

			return encryptedValue.ToString();
		}
	}
}