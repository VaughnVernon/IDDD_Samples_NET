using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    public class TeamMemberId : ValueObject
    {
        public TeamMemberId(Tenants.TenantId tenantId, string id)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(id, "The id must be provided.");
            AssertionConcern.AssertArgumentLength(id, 36, "The id must be 36 characters or less.");

            this.TenantId = tenantId;
            this.Id = id;
        }

        public Tenants.TenantId TenantId { get; private set; }

        public string Id { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.TenantId;
            yield return this.Id;
        }
    }
}
