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
	/// A domain service which generates passwords
	/// and evaluates passwords for strength.
	/// </summary>
	[CLSCompliant(true)]
	public sealed class PasswordService
	{
		#region [ Private Constants ]

		private const string Digits = "0123456789";
		private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string Symbols = "\"`!?$?%^&*()_-+={[}]:;@'~#|\\<,>.?/";

		private const int StrongThreshold = 20;
		private const int VeryStrongThreshold = 40;

		#endregion

		#region [ Public Methods ]

		/// <summary>
		/// Generates a pseudo-random string which
		/// qualifies as a strong password.
		/// </summary>
		/// <returns>
		/// A pseudo-random string which
		/// qualifies as a strong password.
		/// </returns>
		public string GenerateStrongPassword()
		{
			string generatedPassword = null;
			string password = string.Empty;
			Random random = new Random();
			bool isStrong = false;
			while (!isStrong)
			{
				int index;
				int opt = random.Next(4);
				switch (opt)
				{
					case 0:
						index = random.Next(Letters.Length);
						password += (Letters.Substring(index, 1));
						break;
					case 1:
						index = random.Next(Letters.Length);
						password += (Letters.Substring(index, 1).ToLower());
						break;
					case 2:
						index = random.Next(Digits.Length);
						password += (Digits.Substring(index, 1));
						break;
					case 3:
						index = random.Next(Symbols.Length);
						password += (Symbols.Substring(index, 1));
						break;
				}

				generatedPassword = password;

				if (generatedPassword.Length >= 7)
				{
					isStrong = this.IsStrong(generatedPassword);
				}
			}

			return generatedPassword;
		}

		/// <summary>
		/// Determines whether a string qualifies as a "strong" password.
		/// </summary>
		/// <param name="plainTextPassword">
		/// A string to be evaluated for password strength.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <paramref name="plainTextPassword"/>
		/// exceeds the threshold to be considered a "strong"
		/// password; otherwise, <c>false</c>.
		/// </returns>
		public bool IsStrong(string plainTextPassword)
		{
			return CalculatePasswordStrength(plainTextPassword) >= StrongThreshold;
		}

		/// <summary>
		/// Determines whether a string qualifies as a "very strong" password.
		/// </summary>
		/// <param name="plainTextPassword">
		/// A string to be evaluated for password strength.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <paramref name="plainTextPassword"/>
		/// exceeds the threshold to be considered a "very strong"
		/// password; otherwise, <c>false</c>.
		/// </returns>
		public bool IsVeryStrong(string plainTextPassword)
		{
			return CalculatePasswordStrength(plainTextPassword) >= VeryStrongThreshold;
		}

		/// <summary>
		/// Determines whether a string does not qualify as a "strong" password.
		/// </summary>
		/// <param name="plainTextPassword">
		/// A string to be evaluated for password strength.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <paramref name="plainTextPassword"/>
		/// does not exceed the threshold to be considered a "strong"
		/// password; otherwise, <c>false</c>.
		/// </returns>
		public bool IsWeak(string plainTextPassword)
		{
			return CalculatePasswordStrength(plainTextPassword) < StrongThreshold;
		}

		#endregion

		#region [ Private Static Method CalculatePasswordStrength() ]

		private static int CalculatePasswordStrength(string plainTextPassword)
		{
			AssertionConcern.AssertArgumentNotNull(plainTextPassword, "Password strength cannot be tested on null.");

			int strength = 0;
			int length = plainTextPassword.Length;
			if (length > 7)
			{
				strength += 10;

				// bonus: one point each additional
				strength += (length - 7);
			}

			int digitCount = 0;
			int letterCount = 0;
			int lowerCount = 0;
			int upperCount = 0;
			int symbolCount = 0;
			for (int idx = 0; idx < length; ++idx)
			{
				char ch = plainTextPassword[idx];
				if (char.IsLetter(ch))
				{
					++letterCount;

					if (char.IsUpper(ch))
					{
						++upperCount;
					}
					else
					{
						++lowerCount;
					}
				}
				else if (char.IsDigit(ch))
				{
					++digitCount;
				}
				else
				{
					++symbolCount;
				}
			}

			strength += (upperCount + lowerCount + symbolCount);

			// bonus: letters and digits
			if ((letterCount >= 2) && (digitCount >= 2))
			{
				strength += (letterCount + digitCount);
			}

			return strength;
		}

		#endregion
	}
}