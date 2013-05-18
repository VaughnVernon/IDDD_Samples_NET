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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using SaaSOvation.AgilePM.Domain.Model.Discussions;
    using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems;
    using SaaSOvation.AgilePM.Domain.Model.Products.Releases;
    using SaaSOvation.AgilePM.Domain.Model.Products.Sprints;
    using SaaSOvation.AgilePM.Domain.Model.Teams;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class Product : Entity, IEquatable<Product>
    {
        public Product(
                TenantId tenantId,
                ProductId productId,
                ProductOwnerId productOwnerId,
                string name,
                string description,
                DiscussionAvailability discussionAvailability)
        {
            this.TenantId = tenantId; // must precede productOwnerId for compare
            this.Description = description;
            this.Discussion = ProductDiscussion.FromAvailability(discussionAvailability);
            this.DiscussionInitiationId = null;
            this.Name = name;
            this.ProductId = productId;
            this.ProductOwnerId = productOwnerId; // TODO: validation currently missing

            DomainEventPublisher
                .Instance
                .Publish(new ProductCreated(
                        this.TenantId,
                        this.ProductId,
                        this.ProductOwnerId,
                        this.Name,
                        this.Description,
                        this.Discussion.Availability));
        }

        readonly ISet<ProductBacklogItem> backlogItems;

        public TenantId TenantId { get; private set; } 
        
        public ProductId ProductId { get; private set; }

        public ProductOwnerId ProductOwnerId { get; private set; }

        

        public string Description { get; private set; }

        public ProductDiscussion Discussion { get; private set; }

        public string DiscussionInitiationId { get; private set; }

        public string Name { get; private set; }               

        public ICollection<ProductBacklogItem> AllBacklogItems()
        {
            return new ReadOnlyCollection<ProductBacklogItem>(new List<ProductBacklogItem>(this.backlogItems));
        }

        public void ChangeProductOwner(ProductOwner productOwner)
        {
            if (!this.ProductOwnerId.Equals(productOwner.ProductOwnerId))
            {
                this.ProductOwnerId = productOwner.ProductOwnerId;

                // TODO: publish event
            }
        }

        public void FailDiscussionInitiation()
        {
            if (this.Discussion.Availability != DiscussionAvailability.Ready)
            {
                this.DiscussionInitiationId = null;

                this.Discussion = ProductDiscussion.FromAvailability(DiscussionAvailability.Failed);
            }
        }

        public void InitiateDiscussion(DiscussionDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new InvalidOperationException("The descriptor must not be null.");
            }

            if (this.Discussion.Availability == DiscussionAvailability.Requested)
            {
                this.Discussion = this.Discussion.NowReady(descriptor);

                DomainEventPublisher
                    .Instance
                    .Publish(new ProductDiscussionInitiated(
                            this.TenantId,
                            this.ProductId,
                            this.Discussion));
            }
        }

        public BacklogItems.BacklogItem PlanBacklogItem(
                BacklogItemId newBacklogItemId,
                String summary,
                String category,
                BacklogItemType type,
                StoryPoints storyPoints)
        {
            var backlogItem =
                new BacklogItems.BacklogItem(
                        this.TenantId,
                        this.ProductId,
                        newBacklogItemId,
                        summary,
                        category,
                        type,
                        BacklogItemStatus.Planned,
                        storyPoints);

            DomainEventPublisher
                .Instance
                .Publish(new ProductBacklogItemPlanned(
                        backlogItem.TenantId,
                        backlogItem.ProductId,
                        backlogItem.BacklogItemId,
                        backlogItem.Summary,
                        backlogItem.Category,
                        backlogItem.Type,
                        backlogItem.StoryPoints));

            return backlogItem;
        }

        public void PlannedProductBacklogItem(BacklogItem backlogItem)
        {
            AssertionConcern.AssertArgumentEquals(this.TenantId, backlogItem.TenantId, "The product and backlog item must have same tenant.");
            AssertionConcern.AssertArgumentEquals(this.ProductId, backlogItem.ProductId, "The backlog item must belong to product.");

            int ordering = this.backlogItems.Count + 1;

            ProductBacklogItem productBacklogItem =
                    new ProductBacklogItem(
                            this.TenantId,
                            this.ProductId,
                            backlogItem.BacklogItemId,
                            ordering);

            this.backlogItems.Add(productBacklogItem);
        }

        public void ReorderFrom(BacklogItemId id, int ordering)
        {
            foreach (var productBacklogItem in this.backlogItems)
            {
                productBacklogItem.ReorderFrom(id, ordering);
            }
        }

        public void RequestDiscussion(DiscussionAvailability discussionAvailability)
        {
            if (this.Discussion.Availability != DiscussionAvailability.Ready)
            {
                this.Discussion =
                        ProductDiscussion.FromAvailability(discussionAvailability);

                DomainEventPublisher
                    .Instance
                    .Publish(new ProductDiscussionRequested(
                            this.TenantId,
                            this.ProductId,
                            this.ProductOwnerId,
                            this.Name,
                            this.Description,
                            this.Discussion.Availability == DiscussionAvailability.Requested));
            }
        }

        public Release ScheduleRelease(
                ReleaseId newReleaseId,
                String name,
                String description,
                DateTime begins,
                DateTime ends)
        {
            Release release =
                new Release(
                        this.TenantId,
                        this.ProductId,
                        newReleaseId,
                        name,
                        description,
                        begins,
                        ends);

            DomainEventPublisher
                .Instance
                .Publish(new ProductReleaseScheduled(
                        release.TenantId,
                        release.ProductId,
                        release.ReleaseId,
                        release.Name,
                        release.Description,
                        release.Begins,
                        release.Ends));

            return release;
        }

        public Sprint ScheduleSprint(
                SprintId newSprintId,
                String name,
                String goals,
                DateTime begins,
                DateTime ends)
        {
            Sprint sprint =
                new Sprint(
                        this.TenantId,
                        this.ProductId,
                        newSprintId,
                        name,
                        goals,
                        begins,
                        ends);

            DomainEventPublisher
                .Instance
                .Publish(new ProductSprintScheduled(
                        sprint.TenantId,
                        sprint.ProductId,
                        sprint.SprintId,
                        sprint.Name,
                        sprint.Goals,
                        sprint.Begins,
                        sprint.Ends));

            return sprint;
        }

        public void StartDiscussionInitiation(String discussionInitiationId)
        {
            if (this.Discussion.Availability != DiscussionAvailability.Ready)
            {
                this.DiscussionInitiationId = discussionInitiationId;
            }
        }

        public bool Equals(Product other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(null, other)) return false;
            return this.TenantId.Equals(other.TenantId)
                && this.ProductId.Equals(other.ProductId);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Product);
        }

        public override int GetHashCode()
        {
            return 
                + (2335 * 3)
                + this.TenantId.GetHashCode()
                + this.ProductId.GetHashCode();
        }

        public override string ToString()
        {
            return "Product [tenantId=" + TenantId + ", productId=" + ProductId
                    + ", backlogItems=" + backlogItems + ", description="
                    + Description + ", discussion=" + Discussion
                    + ", discussionInitiationId=" + DiscussionInitiationId
                    + ", name=" + Name + ", productOwnerId=" + ProductOwnerId + "]";
        }
    }
}
