using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    public class GroupUserAdded : SaaSOvation.Common.Domain.Model.IDomainEvent
    {
        public GroupUserAdded(TenantId tenantId, string groupName, string username)
        {
            this.EventVersion = 1;
            this.GroupName = groupName;
            this.OccurredOn = DateTime.Now;
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public string GroupName { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
