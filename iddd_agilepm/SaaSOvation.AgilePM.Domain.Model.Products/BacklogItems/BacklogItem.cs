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
    using System.Collections.Generic;
    using System.Linq;

    using SaaSOvation.Common.Domain.Model;    
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.AgilePM.Domain.Model.Teams;
    using SaaSOvation.AgilePM.Domain.Model.Products.Sprints;
    using SaaSOvation.AgilePM.Domain.Model.Products.Releases;
    using SaaSOvation.AgilePM.Domain.Model.Discussions;

    public class BacklogItem : EntityWithCompositeId
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

            this.tasks = new List<Task>();
        }

        readonly List<Task> tasks;

        public TenantId TenantId { get; private set; }

        public ProductId ProductId { get; private set; }

        public BacklogItemId BacklogItemId { get; private set; }

        string summary;

        public string Summary
        {
            get { return this.summary; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The summary must be provided.");
                AssertionConcern.AssertArgumentLength(value, 100, "The summary must be 100 characters or less.");
                this.summary = value;
            }
        }

        public string Category { get; private set; }

        public BacklogItemType Type { get; private set; }

        public BacklogItemStatus Status { get; private set; }

        public bool IsDone
        {
            get { return this.Status == BacklogItemStatus.Done; }
        }

        public bool IsPlanned
        {
            get { return this.Status == BacklogItemStatus.Planned; }
        }

        public bool IsRemoved
        {
            get { return this.Status == BacklogItemStatus.Removed; }
        }

        public StoryPoints StoryPoints { get; private set; }

        public string AssociatedIssueId { get; private set; }

        public void AssociateWithIssue(string issueId)
        {
            if (this.AssociatedIssueId == null)
            {
                this.AssociatedIssueId = issueId;
            }
        }

        public BusinessPriority BusinessPriority { get; private set; }

        public bool HasBusinessPriority
        {
            get { return this.BusinessPriority != null; }
        }

        public void AssignBusinessPriority(BusinessPriority businessPriority)
        {
            this.BusinessPriority = businessPriority;
            DomainEventPublisher.Instance.Publish(
                new BusinessPriorityAssigned(this.TenantId, this.BacklogItemId, businessPriority));
        }

        public void AssignStoryPoints(StoryPoints storyPoints)
        {
            this.StoryPoints = storyPoints;
            DomainEventPublisher.Instance.Publish(
                new BacklogItemStoryPointsAssigned(this.TenantId, this.BacklogItemId, storyPoints));
        }

        public Task GetTask(TaskId taskId)
        {
            return this.tasks.FirstOrDefault(x => x.TaskId.Equals(taskId));
        }

        Task LoadTask(TaskId taskId)
        {
            var task = GetTask(taskId);
            if (task == null)
                throw new InvalidOperationException("Task does not exist.");
            return task;
        }

        public void AssignTaskVolunteer(TaskId taskId, TeamMember volunteer)
        {
            var task = LoadTask(taskId);
            task.AssignVolunteer(volunteer);
        }

        public void ChangeCategory(string category)
        {
            this.Category = category;
            DomainEventPublisher.Instance.Publish(
                new BacklogItemCategoryChanged(this.TenantId, this.BacklogItemId, category));
        }

        public void ChangeTaskStatus(TaskId taskId, TaskStatus status)
        {
            var task = LoadTask(taskId);
            task.ChangeStatus(status);
        }

        public void ChangeType(BacklogItemType type)
        {
            this.Type = type;
            DomainEventPublisher.Instance.Publish(
                new BacklogItemTypeChanged(this.TenantId, this.BacklogItemId, type));
        }

        public ReleaseId ReleaseId { get; private set; }

        public bool IsScheduledForRelease
        {
            get { return this.ReleaseId != null; }
        }

        public SprintId SprintId { get; private set; }

        public bool IsCommittedToSprint
        {
            get { return this.SprintId != null; }
        }

        public void CommitTo(Sprint sprint)
        {
            AssertionConcern.AssertArgumentNotNull(sprint, "Sprint must not be null.");
            AssertionConcern.AssertArgumentEquals(sprint.TenantId, this.TenantId, "Sprint must be of same tenant.");
            AssertionConcern.AssertArgumentEquals(sprint.ProductId, this.ProductId, "Sprint must be of same product.");

            if (!this.IsScheduledForRelease)
                throw new InvalidOperationException("Must be scheduled for release to commit to sprint.");

            if (this.IsCommittedToSprint)
            {
                if (!sprint.SprintId.Equals(this.SprintId))
                {
                    UncommittFromSprint();
                }
            }

            ElevateStatusWith(BacklogItemStatus.Committed);

            this.SprintId = sprint.SprintId;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemCommitted(this.TenantId, this.BacklogItemId, sprint.SprintId));
        }

        void ElevateStatusWith(BacklogItemStatus status)
        {
            if (this.Status == BacklogItemStatus.Scheduled)
            {
                this.Status = BacklogItemStatus.Committed;
            }
        }

        public void UncommittFromSprint()
        {
            if (!this.IsCommittedToSprint)
                throw new InvalidOperationException("Not currently committed.");

            this.Status = BacklogItemStatus.Scheduled;
            var uncommittedSprintId = this.SprintId;
            this.SprintId = null;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemUncommitted(this.TenantId, this.BacklogItemId, uncommittedSprintId));
        }

        public void DefineTask(TeamMember volunteer, string name, string description, int hoursRemaining)
        {
            var task = new Task(
                this.TenantId,
                this.BacklogItemId,
                new TaskId(),
                volunteer,
                name,
                description,
                hoursRemaining,
                TaskStatus.NotStarted);

            this.tasks.Add(task);

            DomainEventPublisher.Instance.Publish(
                new TaskDefined(this.TenantId, this.BacklogItemId, task.TaskId, volunteer.TeamMemberId.Id, name, description));
        }

        public void DescribeTask(TaskId taskId, string description)
        {
            var task = LoadTask(taskId);
            task.DescribeAs(description);
        }

        public BacklogItemDiscussion Discussion { get; private set; }

        public void FailDiscussionInitiation()
        {
            if (this.Discussion.Availability == DiscussionAvailability.Ready)
            {
                this.DiscussionInitiationId = null;
                this.Discussion = BacklogItemDiscussion.FromAvailability(DiscussionAvailability.Failed);
            }
        }

        string discussionInitiationId;

        public string DiscussionInitiationId
        {
            get { return this.discussionInitiationId; }
            private set
            {
                if (value != null)
                    AssertionConcern.AssertArgumentLength(value, 100, "Discussion initiation identity must be 100 characters or less.");
                this.discussionInitiationId = value;
            }
        }

        public void InitiateDiscussion(DiscussionDescriptor descriptor)
        {
            AssertionConcern.AssertArgumentNotNull(descriptor, "The descriptor must not be null.");
            if (this.Discussion.Availability == DiscussionAvailability.Requested)
            {
                this.Discussion = this.Discussion.NowReady(descriptor);
                DomainEventPublisher.Instance.Publish(
                    new BacklogItemDiscussionInitiated(this.TenantId, this.BacklogItemId, this.Discussion));
            }
        }

        public void InitiateDiscussion(BacklogItemDiscussion discussion)
        {
            this.Discussion = discussion;
            DomainEventPublisher.Instance.Publish(
                new BacklogItemDiscussionInitiated(this.TenantId, this.BacklogItemId, discussion));
        }

        public int TotalTaskHoursRemaining
        {
            get { return this.tasks.Select(x => x.HoursRemaining).Sum(); }
        }

        public bool AnyTaskHoursRemaining
        {
            get { return this.TotalTaskHoursRemaining > 0; }
        }

        public void EstimateTaskHoursRemaining(TaskId taskId, int hoursRemaining)
        {
            var task = LoadTask(taskId);
            task.EstimateHoursRemaining(hoursRemaining);

            var changedStatus = default(BacklogItemStatus?);

            if (hoursRemaining == 0)
            {
                if (!this.AnyTaskHoursRemaining)
                {
                    changedStatus = BacklogItemStatus.Done;
                }
            }
            else if (this.IsDone)
            {
                if (this.IsCommittedToSprint)
                {
                    changedStatus = BacklogItemStatus.Committed;
                }
                else if (this.IsScheduledForRelease)
                {
                    changedStatus = BacklogItemStatus.Scheduled;
                }
                else
                {
                    changedStatus = BacklogItemStatus.Planned;
                }
            }

            if (changedStatus != null)
            {
                this.Status = changedStatus.Value;
                DomainEventPublisher.Instance.Publish(
                    new BacklogItemStatusChanged(this.TenantId, this.BacklogItemId, changedStatus.Value));
            }
        }

        public void MarkAsRemoved()
        {
            if (this.IsRemoved)
                throw new InvalidOperationException("Already removed, not outstanding.");
            if (this.IsDone)
                throw new InvalidOperationException("Already done, not outstanding.");
            
            if (this.IsCommittedToSprint)
            {
                UncommittFromSprint();
            }

            if (this.IsScheduledForRelease)
            {
                UnscheduleFromRelease();
            }

            this.Status = BacklogItemStatus.Removed;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemMarkedAsRemoved(this.TenantId, this.BacklogItemId));
        }

        public void UnscheduleFromRelease()
        {
            if (this.IsCommittedToSprint)
                throw new InvalidOperationException("Must first uncommit.");
            if (!this.IsScheduledForRelease)
                throw new InvalidOperationException("Not scheduled for release.");

            this.Status = BacklogItemStatus.Planned;
            var unscheduledReleaseId = this.ReleaseId;
            this.ReleaseId = null;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemUnscheduled(this.TenantId, this.BacklogItemId, unscheduledReleaseId));
        }

        public void RemoveTask(TaskId taskId)
        {
            var task = LoadTask(taskId);

            if (!this.tasks.Remove(task))
                throw new InvalidOperationException("Task was not removed.");

            DomainEventPublisher.Instance.Publish(
                new TaskRemoved(this.TenantId, this.BacklogItemId));
        }

        public void RenameTask(TaskId taskId, string name)
        {
            var task = LoadTask(taskId);
            task.Rename(name);
        }

        public void RequestDiscussion(DiscussionAvailability availability)
        {
            if (this.Discussion.Availability != DiscussionAvailability.Ready)
            {
                this.Discussion = BacklogItemDiscussion.FromAvailability(availability);

                DomainEventPublisher.Instance.Publish(
                    new BacklogItemDiscussionRequested(
                        this.TenantId,
                        this.ProductId,
                        this.BacklogItemId,
                        availability == DiscussionAvailability.Requested));

            }
        }

        public void ScheduleFor(Release release)
        {
            AssertionConcern.AssertArgumentNotNull(release, "Release must not be null.");
            AssertionConcern.AssertArgumentEquals(this.TenantId, release.TenantId, "Release must be of same tenant.");
            AssertionConcern.AssertArgumentEquals(this.ProductId, release.ProductId, "Release must be of same product.");

            if (this.IsScheduledForRelease && !this.ReleaseId.Equals(release.ReleaseId))
            {
                UnscheduleFromRelease();
            }

            if (this.Status == BacklogItemStatus.Planned)
            {
                this.Status = BacklogItemStatus.Scheduled;
            }

            this.ReleaseId = release.ReleaseId;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemScheduled(this.TenantId, this.BacklogItemId, release.ReleaseId));

        }

        public void StartDiscussionInitiation(string discussionInitiationId)
        {
            if (this.Discussion.Availability != DiscussionAvailability.Ready)
            {
                this.DiscussionInitiationId = discussionInitiationId;
            }
        }

        public void Summarize(string summary)
        {
            this.Summary = summary;
            DomainEventPublisher.Instance.Publish(
                new BacklogItemSummarized(this.TenantId, this.BacklogItemId, summary));
        }

        public string Story { get; private set; }

        public void TellStory(string story)
        {
            if (story != null)
                AssertionConcern.AssertArgumentLength(story, 65000, "The story must be 65000 characters or less.");

            this.Story = story;

            DomainEventPublisher.Instance.Publish(
                new BacklogItemStoryTold(this.TenantId, this.BacklogItemId, story));
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.ProductId;
            yield return this.BacklogItemId;
        }
    }
}
