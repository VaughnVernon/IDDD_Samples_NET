using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class AddUserToGroupCommand
    {
        public AddUserToGroupCommand()
        {
        }

        public AddUserToGroupCommand(string tenantId, string childGroupName, string parentGroupName)
        {
            this.TenantId = tenantId;
            this.ChildGroupName = childGroupName;
            this.ParentGroupName = parentGroupName;
        }

        public string TenantId { get; set; }

        public string ChildGroupName { get; set; }

        public string ParentGroupName { get; set; }
    }
}
