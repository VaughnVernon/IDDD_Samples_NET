using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangeUserPasswordCommand
    {
        public ChangeUserPasswordCommand()
        {
        }

        public ChangeUserPasswordCommand(string tenantId, string userName, string currentPassword, string changedPassword)
        {
            this.TenantId = tenantId;
            this.Username = userName;
            this.CurrentPassword = currentPassword;
            this.ChangedPassword = changedPassword;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string ChangedPassword { get; set; }
    }
}
