using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangeEmailAddressCommand
    {
        public ChangeEmailAddressCommand()
        {
        }

        public ChangeEmailAddressCommand(string tenantId, string userName, string emailAddress)
        {
            this.TenantId = tenantId;
            this.Username = userName;
            this.EmailAddress = emailAddress;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
    }
}
