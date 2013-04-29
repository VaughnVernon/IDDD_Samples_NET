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

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class BacklogItem : Entity
    {
        public BacklogItem(
            TenantId tenantId,
            ProductId productId,
            BacklogItemId backlogItemId,
            string summary,
            string category,
            BacklogItemType type,
            BacklogItemStatus backlogItemStatus,
            StoryPoints storyPoints)
        {
            this.BacklogItemId = backlogItemId;
            this.Category = category;
            this.ProductId = productId;
            this.Status = backlogItemStatus;
            this.StoryPoints = storyPoints;
            this.Summary = summary;
            this.TenantId = tenantId;
            this.Type = type;
        }

        public BacklogItemId BacklogItemId { get; private set; }

        public string Category { get; private set; }

        public ProductId ProductId { get; private set; }

        private BacklogItemStatus Status;

        public StoryPoints StoryPoints { get; private set; }

        public string Summary { get; private set; }

        public TenantId TenantId { get; private set; }

        public BacklogItemType Type { get; private set; }


    }
}
