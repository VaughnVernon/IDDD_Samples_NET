using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.IdentityAccess.Domain.Model.Identity;

namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    public class UserAssignedToRole : SaaSOvation.Common.Domain.Model.IDomainEvent
    {
        public UserAssignedToRole(
            TenantId tenantId,
            string roleName,
            string username,
            string firstName,
            string lastName,
            string emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.EventVersion = 1;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.OccurredOn = DateTime.Now;
            this.RoleName = roleName;
            this.TenantId = tenantId;
            this.Username = username;
        }

        public string EmailAddress { get; private set; }

        public int EventVersion { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public TenantId TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
