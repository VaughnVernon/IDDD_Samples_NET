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

    public class PasswordService
    {
        private const string DIGITS = "0123456789";
        private const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int STRONG_THRESHOLD = 20;
        private const string SYMBOLS = "\"`!?$?%^&*()_-+={[}]:;@'~#|\\<,>.?/";
        private const int VERY_STRONG_THRESHOLD = 40;

        public PasswordService() 
        {
        }

        public string GenerateStrongPassword()
        {
            String generatedPassword = null;

            var password = "";

            var random = new Random();

            var isStrong = false;

            var index = 0;

            while (!isStrong)
            {
                var opt = random.Next(4);

                switch (opt)
                {
                    case 0:
                        index = random.Next(LETTERS.Length);
                        password += (LETTERS.Substring(index, 1));
                        break;
                    case 1:
                        index = random.Next(LETTERS.Length);
                        password += (LETTERS.Substring(index, 1).ToLower());
                        break;
                    case 2:
                        index = random.Next(DIGITS.Length);
                        password += (DIGITS.Substring(index, 1));
                        break;
                    case 3:
                        index = random.Next(SYMBOLS.Length);
                        password += (SYMBOLS.Substring(index, 1));
                        break;
                }

                generatedPassword = password.ToString();

                if (generatedPassword.Length >= 7)
                {
                    isStrong = this.IsStrong(generatedPassword);
                }
            }

            return generatedPassword;
        }

        public bool IsStrong(string plainTextPassword)
        {
            return this.CalculatePasswordStrength(plainTextPassword) >= STRONG_THRESHOLD;
        }

        public bool IsVeryStrong(string plainTextPassword)
        {
            return this.CalculatePasswordStrength(plainTextPassword) >= VERY_STRONG_THRESHOLD;
        }

        public bool IsWeak(string plainTextPassword)
        {
            return this.CalculatePasswordStrength(plainTextPassword) < STRONG_THRESHOLD;
        }

        int CalculatePasswordStrength(String plainTextPassword)
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

            var digitCount = 0;
            var letterCount = 0;
            var lowerCount = 0;
            var upperCount = 0;
            var symbolCount = 0;

            for (var idx = 0; idx < length; ++idx)
            {
                var ch = plainTextPassword[idx];

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
            if (letterCount >= 2 && digitCount >= 2)
            {
                strength += (letterCount + digitCount);
            }

            return strength;
        }
    }
}
