using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    public class UserEnablementChanged : SaaSOvation.Common.Domain.Model.IDomainEvent
    {
        public UserEnablementChanged(
                TenantId tenantId,
                String username,
                Enablement enablement)
        {
            this.Enablement = enablement;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public Enablement Enablement { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
