using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class RemoveGroupFromGroupCommand
    {
        public RemoveGroupFromGroupCommand()
        {
        }

        public RemoveGroupFromGroupCommand(String tenantId, String parentGroupName, String childGroupName)
        {
            this.TenantId = tenantId;
            this.ParentGroupName = parentGroupName;
            this.ChildGroupName = childGroupName;
        }

        public string TenantId { get; set; }
        public string ChildGroupName { get; set; }
        public string ParentGroupName { get; set; }
    }
}
