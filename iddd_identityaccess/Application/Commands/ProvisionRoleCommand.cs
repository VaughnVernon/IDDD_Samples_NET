using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ProvisionRoleCommand
    {
        public ProvisionRoleCommand()
        {
        }

        public ProvisionRoleCommand(string tenantId, string roleName, string description, bool supportsNesting)
        {
            this.TenantId = tenantId;
            this.RoleName = roleName;
            this.Description = description;
            this.SupportsNesting = supportsNesting;
        }

        public string TenantId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool SupportsNesting { get; set; }
    }
}
