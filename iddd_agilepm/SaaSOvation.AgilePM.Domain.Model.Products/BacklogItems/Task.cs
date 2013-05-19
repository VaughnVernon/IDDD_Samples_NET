using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;
using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.AgilePM.Domain.Model.Teams;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class Task : EntityWithCompositeId
    {
        public Task(
            TenantId tenantId, 
            BacklogItemId backlogItemId, 
            TaskId taskId, 
            TeamMember teamMember, 
            string name, 
            string description, 
            int hoursRemaining, 
            TaskStatus status)
        {
            this.TenantId = tenantId;
            this.BacklogItemId = backlogItemId;
            this.TaskId = taskId;
            this.Volunteer = teamMember.TeamMemberId;
            this.Name = name;
            this.Description = description;
            this.HoursRemaining = hoursRemaining;
            this.Status = status;
            this.estimationLog = new List<EstimationLogEntry>();
        }

        TeamMemberId volunteer;
        List<EstimationLogEntry> estimationLog;
        string name;
        string description;

        public TenantId TenantId { get; private set; }

        internal BacklogItemId BacklogItemId { get; private set; }

        internal TaskId TaskId { get; private set; }

        public string Name
        {
            get { return this.name; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "Name is required.");
                AssertionConcern.AssertArgumentLength(value, 100, "Name must be 100 characters or less.");
                this.name = value;
            }
        }

        public string Description
        {
            get { return this.description; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "Description is required.");
                AssertionConcern.AssertArgumentLength(value, 65000, "Description must be 65000 characters or less.");
                this.description = value;
            }
        }

        public TaskStatus Status { get; private set; }

        public TeamMemberId Volunteer
        {
            get { return this.volunteer; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The volunteer id must be provided.");
                AssertionConcern.AssertArgumentEquals(this.TenantId, value.TenantId, "The volunteer must be of the same tenant.");
                this.volunteer = value;
            }
        }
       
        internal int HoursRemaining { get; private set; }        

        internal void AssignVolunteer(TeamMember teamMember)
        {
            AssertionConcern.AssertArgumentNotNull(teamMember, "A volunteer must be provided.");
            this.Volunteer = teamMember.TeamMemberId;
            DomainEventPublisher.Instance.Publish(
                new TaskVolunteerAssigned(this.TenantId, this.BacklogItemId, this.TaskId, teamMember.TeamMemberId.Id));
        }

        internal void ChangeStatus(TaskStatus status)
        {
            this.Status = status;
            DomainEventPublisher.Instance.Publish(
                new TaskStatusChanged(this.TenantId, this.BacklogItemId, this.TaskId, status));
        }

        internal void DescribeAs(string description)
        {
            this.Description = description;
            DomainEventPublisher.Instance.Publish(
                new TaskDescribed(this.TenantId, this.BacklogItemId, this.TaskId, description));
        }

        internal void EstimateHoursRemaining(int hoursRemaining)
        {
            if (hoursRemaining < 0)
                throw new ArgumentOutOfRangeException("hoursRemaining");

            if (hoursRemaining != this.HoursRemaining)
            {
                this.HoursRemaining = hoursRemaining;
                DomainEventPublisher.Instance.Publish(
                    new TaskHoursRemainingEstimated(this.TenantId, this.BacklogItemId, this.TaskId, hoursRemaining));

                if (hoursRemaining == 0 && this.Status != TaskStatus.Done)
                {
                    ChangeStatus(TaskStatus.Done);
                }
                else if (hoursRemaining > 0 && this.Status != TaskStatus.InProgress)
                {
                    ChangeStatus(TaskStatus.InProgress);
                }

                LogEstimation(hoursRemaining);
            }
        }

        void LogEstimation(int hoursRemaining)
        {
            var today = EstimationLogEntry.CurrentLogDate;
            var updatedLogForToday = this.estimationLog.Any(entry => entry.UpdateHoursRemainingWhenDateMatches(hoursRemaining, today));
            if (updatedLogForToday)
            {
                this.estimationLog.Add(
                    new EstimationLogEntry(this.TenantId, this.TaskId, today, hoursRemaining));
            }
        }

        internal void Rename(string name)
        {
            this.Name = name;
            DomainEventPublisher.Instance.Publish(
                new TaskRenamed(this.TenantId, this.BacklogItemId, this.TaskId, name));
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.BacklogItemId;
            yield return this.TaskId;
        }
    }
}
