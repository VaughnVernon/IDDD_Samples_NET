using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class DisableProductOwnerCommand : DisableMemberCommand
    {
        public DisableProductOwnerCommand()
        {
        }

        public DisableProductOwnerCommand(string tenantId, string username, DateTime occurredOn)
            : base(tenantId, username, occurredOn)
        {
        }
    }
}
