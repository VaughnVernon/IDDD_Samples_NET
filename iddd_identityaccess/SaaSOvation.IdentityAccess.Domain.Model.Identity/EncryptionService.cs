namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;

    public interface EncryptionService
    {
        string EncryptedValue(string plainTextValue);
    }
}
