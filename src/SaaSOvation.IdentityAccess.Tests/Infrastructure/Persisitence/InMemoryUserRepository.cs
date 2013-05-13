namespace SaaSOvation.IdentityAccess.Tests.Infrastructure.Persisitence
{
    using System;
    using System.Collections.Generic;

    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class InMemoryUserRepository : UserRepository
    {
        private IDictionary<string, User> repository = new Dictionary<string, User>();

        public void Add(User user)
        {
            if (this.repository.ContainsKey(this.keyOf(user)))
            {
                throw new ArgumentException("Duplicate key", "user");
            }

            this.repository.Add(this.keyOf(user), user);
        }

        public User UserFromAuthenticCredentials(TenantId tenantId, string username, string encryptedPassword)
        {
            var userToCheck = this.UserWithUsername(tenantId, username);
            if (userToCheck != null)
            {
                if (userToCheck.Password.Equals(encryptedPassword))
                {
                    return userToCheck;
                }
            }

            return null;
        }

        public User UserWithUsername(TenantId tenantId, string username)
        {
            return this.repository[this.keyOf(tenantId, username)];
        }


        private string keyOf(TenantId tenantId, string username)
        {
            var key = string.Format("{0}#{1}", tenantId.Id, username);
            return key;
        }

        private string keyOf(User user)
        {
            return this.keyOf(user.TenantId, user.Username);
        }
    }
}
