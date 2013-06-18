using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class DisableMemberCommand
    {
        public DisableMemberCommand()
        {
        }

        public DisableMemberCommand(string tenantId, string username, DateTime occurredOn)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.OccurredOn = occurredOn;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
