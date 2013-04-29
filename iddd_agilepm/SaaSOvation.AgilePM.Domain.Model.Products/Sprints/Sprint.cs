// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.AgilePM.Domain.Model.Products.Sprints
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class Sprint : Entity
    {
        public Sprint(
            TenantId tenantId,
            ProductId productId,
            SprintId sprintId,
            string name,
            string goals,
            DateTime begins,
            DateTime ends)
        {
            if (ends.Ticks < begins.Ticks)
            {
                throw new InvalidOperationException("Sprint must not end before it begins.");
            }

            this.BacklogItems = new HashSet<CommittedBacklogItem>();
            this.Begins = begins;
            this.Goals = goals;
            this.Ends = ends;
            this.Name = name;
            this.ProductId = productId;
            this.SprintId = sprintId;
            this.TenantId = tenantId;
        }

        public System.DateTime Begins { get; private set; }

        public System.DateTime Ends { get; private set; }

        public string Goals { get; private set; }

        public string Name { get; private set; }

        public ProductId ProductId { get; private set; }

        public SprintId SprintId { get; private set; }

        public TenantId TenantId { get; private set; }

        private ISet<CommittedBacklogItem> BacklogItems;

        public void AdjustGoals(String goals)
        {
            this.Goals = goals;

            // TODO: publish event / student assignment
        }

        public ICollection<CommittedBacklogItem> AllCommittedBacklogItems()
        {
            return new ReadOnlyCollection<CommittedBacklogItem>(new List<CommittedBacklogItem>(this.BacklogItems));
        }

    }
}
