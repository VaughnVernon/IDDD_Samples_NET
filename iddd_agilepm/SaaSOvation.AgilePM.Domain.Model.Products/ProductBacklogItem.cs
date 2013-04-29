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

    public class ProductBacklogItem
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

        public BacklogItemId BacklogItemId { get; private set; }

        public int Ordering { get; private set; }

        public ProductId ProductId { get; private set; }

        public TenantId TenantId { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                ProductBacklogItem typedObject = (ProductBacklogItem)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.ProductId.Equals(typedObject.ProductId) &&
                    this.BacklogItemId.Equals(typedObject.BacklogItemId);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (15389 * 97)
                + this.TenantId.GetHashCode()
                + this.ProductId.GetHashCode()
                + this.BacklogItemId.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "ProductBacklogItem [tenantId=" + TenantId
                    + ", productId=" + ProductId
                    + ", backlogItemId=" + BacklogItemId
                    + ", ordering=" + Ordering + "]";
        }

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
    }
}
