using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangeSecondaryTelephoneCommand
    {
        public ChangeSecondaryTelephoneCommand()
        {
        }

        public ChangeSecondaryTelephoneCommand(string tenantId, string userName, string telephone)
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
