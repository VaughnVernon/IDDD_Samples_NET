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
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;
    using SaaSOvation.Common.Domain.Model;

    public class CommittedBacklogItem : Entity, IEquatable<CommittedBacklogItem>
    {
        public CommittedBacklogItem(TenantId tenantId, SprintId sprintId, BacklogItemId backlogItemId, int ordering = 0)
        {
            this.TenantId = tenantId;
            this.SprintId = sprintId;
            this.BacklogItemId = backlogItemId;
            this.Ordering = ordering;
        }

        public TenantId TenantId { get; private set; }

        public SprintId SprintId { get; private set; }

        public BacklogItemId BacklogItemId { get; private set; }

        public int Ordering { get; private set; }

        public void ReOrderFrom(BacklogItemId id, int orderOfPriority)
        {
            if (this.BacklogItemId.Equals(id))
            {
                this.Ordering = orderOfPriority;
            }
            else if (this.Ordering >= orderOfPriority)
            {
                this.Ordering = this.Ordering + 1;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CommittedBacklogItem);
        }

        public bool Equals(CommittedBacklogItem other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(null, other)) return false;

            return this.TenantId.Equals(other.TenantId)
                && this.SprintId.Equals(other.SprintId)
                && this.BacklogItemId.Equals(other.BacklogItemId);
        }

        public override int GetHashCode()
        {
            return
                + (282891 * 53)
                + this.TenantId.GetHashCode()
                + this.SprintId.GetHashCode()
                + this.BacklogItemId.GetHashCode();
        }
    }
}
