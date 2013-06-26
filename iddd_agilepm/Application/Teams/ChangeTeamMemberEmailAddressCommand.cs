using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class ChangeTeamMemberEmailAddressCommand
    {
        public ChangeTeamMemberEmailAddressCommand()
        {
        }

        public ChangeTeamMemberEmailAddressCommand(string tenantId, string username, string emailAddress, DateTime occurredOn)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.EmailAddress = emailAddress;
            this.OccurredOn = occurredOn;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
