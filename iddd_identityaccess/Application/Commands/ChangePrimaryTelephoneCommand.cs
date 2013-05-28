using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangePrimaryTelephoneCommand
    {
        public ChangePrimaryTelephoneCommand()
        {
        }

        public ChangePrimaryTelephoneCommand(string tenantId, string userName, string telephone)
        {
            this.TenantId = tenantId;
            this.Username = userName;
            this.Telephone = telephone;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string Telephone { get; set; }
    }
}
