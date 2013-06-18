using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Teams;
using SaaSOvation.AgilePM.Domain.Model.Tenants;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class TeamApplicationService
    {
        public TeamApplicationService(ITeamMemberRepository teamMemberRepository, IProductOwnerRepository productOwnerRepository)
        {
            this.teamMemberRepository = teamMemberRepository;
            this.productOwnerRepository = productOwnerRepository;
        }

        readonly ITeamMemberRepository teamMemberRepository;
        readonly IProductOwnerRepository productOwnerRepository;

        public void EnableProductOwner(EnableProductOwnerCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var productOwner = this.productOwnerRepository.Get(tenantId, command.Username);
                if (productOwner != null)
                {
                    productOwner.Enable(command.OccurredOn);
                }
                else
                {
                    productOwner = new ProductOwner(tenantId, command.Username, command.FirstName, command.LastName, command.EmailAddress, command.OccurredOn);
                    this.productOwnerRepository.Save(productOwner);
                }
                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void EnableTeamMember(EnableTeamMemberCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var teamMember = this.teamMemberRepository.Get(tenantId, command.Username);
                if (teamMember != null)
                {
                    teamMember.Enable(command.OccurredOn);
                }
                else
                {
                    teamMember = new TeamMember(tenantId, command.Username, command.FirstName, command.LastName, command.EmailAddress, command.OccurredOn);
                    this.teamMemberRepository.Save(teamMember);
                }
                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void ChangeTeamMemberEmailAddress(ChangeTeamMemberEmailAddressCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var productOwner = this.productOwnerRepository.Get(tenantId, command.Username);
                if (productOwner != null)
                {
                    productOwner.ChangeEmailAddress(command.EmailAddress, command.OccurredOn);
                    this.productOwnerRepository.Save(productOwner);
                }

                var teamMember = this.teamMemberRepository.Get(tenantId, command.Username);
                if (teamMember != null)
                {
                    teamMember.ChangeEmailAddress(command.EmailAddress, command.OccurredOn);
                    this.teamMemberRepository.Save(teamMember);
                }

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void ChangeTeamMemberName(ChangeTeamMemberNameCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var productOwner = this.productOwnerRepository.Get(tenantId, command.Username);
                if (productOwner != null)
                {
                    productOwner.ChangeName(command.FirstName, command.LastName, command.OccurredOn);
                    this.productOwnerRepository.Save(productOwner);
                }

                var teamMember = this.teamMemberRepository.Get(tenantId, command.Username);
                if (teamMember != null)
                {
                    teamMember.ChangeName(command.FirstName, command.LastName, command.OccurredOn);
                    this.teamMemberRepository.Save(teamMember);
                }

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void DisableProductOwner(DisableProductOwnerCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var productOwner = this.productOwnerRepository.Get(tenantId, command.Username);
                if (productOwner != null)
                {
                    productOwner.Disable(command.OccurredOn);
                    this.productOwnerRepository.Save(productOwner);
                }
                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void DisableTeamMember(DisableTeamMemberCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var teamMember = this.teamMemberRepository.Get(tenantId, command.Username);
                if (teamMember != null)
                {
                    teamMember.Disable(command.OccurredOn);
                    this.teamMemberRepository.Save(teamMember);
                }
                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }
    }
}
