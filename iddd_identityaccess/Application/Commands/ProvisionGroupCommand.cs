using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ProvisionGroupCommand
    {
        public ProvisionGroupCommand()
        {
        }

        public ProvisionGroupCommand(string tenantId, string groupName, string description)
        {
            this.TenantId = tenantId;
            this.GroupName = groupName;
            this.Description = description;
        }

        public string TenantId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}
