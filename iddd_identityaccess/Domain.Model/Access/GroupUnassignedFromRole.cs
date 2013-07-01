using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.IdentityAccess.Domain.Model.Identity;

namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    public class GroupUnassignedFromRole : SaaSOvation.Common.Domain.Model.IDomainEvent
    {
        public GroupUnassignedFromRole(TenantId tenantId, string roleName, string groupName)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = DateTime.Now;
            this.RoleName = roleName;
            this.TenantId = tenantId;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string RoleName { get; private set; }

        public TenantId TenantId { get; private set; }
    }
}
