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
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Products.Releases;
    using SaaSOvation.Common.Domain.Model;

    public class ProductReleaseScheduled : IDomainEvent
    {
        public ProductReleaseScheduled(
            TenantId tenantId,
            ProductId productId,
            ReleaseId releaseId,
            string name,
            string description,
            DateTime starts,
            DateTime ends)
        {
            this.Description = description;
            this.Ends = ends;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = DateTime.Now;
            this.ProductId = productId;
            this.ReleaseId = releaseId;
            this.Starts = starts;
            this.TenantId = tenantId;
        }

        public string Description { get; private set; }

        public DateTime Ends { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public ProductId ProductId { get; private set; }

        public ReleaseId ReleaseId { get; private set; }

        public DateTime Starts { get; private set; }

        public TenantId TenantId { get; private set; }
    }
}
