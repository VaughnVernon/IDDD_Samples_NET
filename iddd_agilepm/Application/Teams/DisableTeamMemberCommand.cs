using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class DisableTeamMemberCommand : DisableMemberCommand
    {
        public DisableTeamMemberCommand()
        {
        }

        public DisableTeamMemberCommand(string tenantId, string username, DateTime occurredOn)
            : base(tenantId, username, occurredOn)
        {
        }
    }
}
