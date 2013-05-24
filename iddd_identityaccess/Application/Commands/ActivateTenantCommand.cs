using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ActivateTenantCommand
    {
        public ActivateTenantCommand() { }

        public ActivateTenantCommand(string tenantId)
        {
            this.TenantId = tenantId;
        }

        public string TenantId { get; set; }
    }
}
