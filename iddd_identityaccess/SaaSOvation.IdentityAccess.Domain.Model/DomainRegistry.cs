namespace SaaSOvation.IdentityAccess.Domain.Model
{
    using System;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;
    using SaaSOvation.IdentityAccess.Port.Adapter.Security;

    public class DomainRegistry
    {
        public static EncryptionService EncryptionService
        {
            get
            {
                // this is not a desirable dependency since it
                // references port adapters, but it doesn't
                // require an IoC container
                return new MD5EncryptionService();
            }
        }

        public static PasswordService PasswordService
        {
            get
            {
                return new PasswordService();
            }
        }
    }
}
