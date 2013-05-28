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

        public AddUserToGroupCommand(string tenantId, string groupName, string userNmae)
        {
            this.TenantId = tenantId;
            this.GroupName = groupName;
            this.Username = userNmae;
        }

        public string TenantId { get; set; }
        public string GroupName { get; set; }
        public string Username { get; set; }
    }
}
