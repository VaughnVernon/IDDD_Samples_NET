namespace SaaSOvation.IdentityAccess.Port.Adapter.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;
    using SaaSOvation.Common.Domain.Model;

    public class MD5EncryptionService : AssertionConcern, EncryptionService
    {
        public MD5EncryptionService()
        {
        }

        public string EncryptedValue(string plainTextValue)
        {
            this.AssertArgumentNotEmpty(
                    plainTextValue,
                    "Plain text value to encrypt must be provided.");

            StringBuilder encryptedValue = new StringBuilder();

            MD5 hasher = MD5.Create();

            byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(plainTextValue));

            for (int dataIndex = 0; dataIndex < data.Length; dataIndex++)
            {
                encryptedValue.Append(data[dataIndex].ToString("x2"));
            }

            return encryptedValue.ToString();
        }
    }
}
