using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    public class Team : Entity
    {
        public Team(TenantId tenantId, string name, ProductOwner productOwner = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId must be provided.");

            this.tenantId = tenantId;
            this.Name = name;
            if (productOwner != null)
                this.ProductOwner = productOwner;
            this.teamMembers = new HashSet<TeamMember>();
        }

        readonly TenantId tenantId;
        string name;
        ProductOwner productOwner;
        readonly HashSet<TeamMember> teamMembers;

        public TenantId TenantId
        {
            get { return this.tenantId; }
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The name must be provided.");
                AssertionConcern.AssertArgumentLength(value, 100, "The name must be 100 characters or less.");
                this.name = value;
            }
        }

        public ProductOwner ProductOwner
        {
            get { return this.productOwner; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The productOwner must be provided.");
                AssertionConcern.AssertArgumentEquals(this.tenantId, value.TenantId, "The productOwner must be of the same tenant.");
                this.productOwner = value;
            }
        }

        public ReadOnlyCollection<TeamMember> AllTeamMembers
        {
            get { return new ReadOnlyCollection<TeamMember>(this.teamMembers.ToArray()); }
        }

        public void AssignProductOwner(ProductOwner productOwner)
        {
            this.ProductOwner = productOwner;
        }

        public void AssignTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            this.teamMembers.Add(teamMember);
        }

        public bool IsTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            return GetTeamMemberByUserName(teamMember.Username) != null;
        }

        public void RemoveTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            var existingTeamMember = GetTeamMemberByUserName(teamMember.Username);
            if (existingTeamMember != null)
            {
                this.teamMembers.Remove(existingTeamMember);
            }
        }

        void AssertValidTeamMember(TeamMember teamMember)
        {
            AssertionConcern.AssertArgumentNotNull(teamMember, "A team member must be provided.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, teamMember.TenantId, "Team member must be of the same tenant.");
        }

        TeamMember GetTeamMemberByUserName(string userName)
        {
            return this.teamMembers.FirstOrDefault(x => x.Username.Equals(userName));
        }
    }
}
