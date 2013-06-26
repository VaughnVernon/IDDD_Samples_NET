using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;

namespace SaaSOvation.AgilePM.Application.Products.BacklogItems
{
    public class BacklogItemApplicationService
    {
        public BacklogItemApplicationService(IBacklogItemRepository backlogItemRepository)
        {
            this.backlogItemRepository = backlogItemRepository;
        }

        readonly IBacklogItemRepository backlogItemRepository;

        // TODO: APIs for student assignment
    }
}
