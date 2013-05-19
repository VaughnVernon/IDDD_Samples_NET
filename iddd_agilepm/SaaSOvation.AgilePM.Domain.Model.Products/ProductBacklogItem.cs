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

namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;

    public class ProductBacklogItem : Entity, IEquatable<ProductBacklogItem>
    {
        public ProductBacklogItem(
            TenantId tenantId,
            ProductId productId,
            BacklogItemId backlogItem,
            int ordering)
        {
            this.BacklogItemId = backlogItem;
            this.Ordering = ordering;
            this.ProductId = productId;
            this.TenantId = tenantId;
        }
       
        public TenantId TenantId { get; private set; }

        public ProductId ProductId { get; private set; }

        public BacklogItemId BacklogItemId { get; private set; }

        public int Ordering { get; private set; }

        internal void ReorderFrom(BacklogItemId id, int ordering)
        {
            if (this.BacklogItemId.Equals(id))
            {
                this.Ordering = ordering;
            }
            else if (this.Ordering >= ordering)
            {
                this.Ordering = this.Ordering + 1;
            }
        }

        public bool Equals(ProductBacklogItem other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(null, other)) return false;

            return this.TenantId.Equals(other.TenantId)
                && this.ProductId.Equals(other.ProductId)
                && this.BacklogItemId.Equals(other.BacklogItemId);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as ProductBacklogItem);
        }

        public override int GetHashCode()
        {
            return
                + (15389 * 97)
                + this.TenantId.GetHashCode()
                + this.ProductId.GetHashCode()
                + this.BacklogItemId.GetHashCode();
        }

        public override string ToString()
        {
            return "ProductBacklogItem [tenantId=" + TenantId
                    + ", productId=" + ProductId
                    + ", backlogItemId=" + BacklogItemId
                    + ", ordering=" + Ordering + "]";
        }
    }
}
