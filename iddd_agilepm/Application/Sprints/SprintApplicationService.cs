using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;
using SaaSOvation.AgilePM.Domain.Model.Products.Sprints;
using SaaSOvation.AgilePM.Domain.Model.Tenants;

namespace SaaSOvation.AgilePM.Application.Sprints
{
    public class SprintApplicationService
    {
        public SprintApplicationService(ISprintRepository sprintRepository, IBacklogItemRepository backlogItemRepository)
        {
            this.sprintRepository = sprintRepository;
            this.backlogItemRepository = backlogItemRepository;
        }

        readonly ISprintRepository sprintRepository;
        readonly IBacklogItemRepository backlogItemRepository;

        public void CommitBacklogItemToSprint(CommitBacklogItemToSprintCommand command)
        {
            var tenantId = new TenantId(command.TenantId);
            var sprint = this.sprintRepository.Get(tenantId, new SprintId(command.SprintId));
            var backlogItem = this.backlogItemRepository.Get(tenantId, new BacklogItemId(command.BacklogItemId));

            sprint.Commit(backlogItem);

            this.sprintRepository.Save(sprint);
        }

    }
}
