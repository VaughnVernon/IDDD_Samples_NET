namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class PasswordService : AssertionConcern
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

            string password = "";

            Random random = new Random();

            bool isStrong = false;

            int index = 0;

            while (!isStrong)
            {
                int opt = random.Next(4);

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

        private int CalculatePasswordStrength(String plainTextPassword)
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

                if (Char.IsLetter(ch))
                {
                    ++letterCount;

                    if (Char.IsUpper(ch))
                    {
                        ++upperCount;
                    }
                    else
                    {
                        ++lowerCount;
                    }
                }
                else if (Char.IsDigit(ch))
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
