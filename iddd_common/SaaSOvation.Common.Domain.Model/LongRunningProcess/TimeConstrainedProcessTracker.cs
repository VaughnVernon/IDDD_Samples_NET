namespace SaaSOvation.Common.Domain.Model.LongRunningProcess
{
    using System;
    using System.Reflection;

    public class TimeConstrainedProcessTracker : AssertionConcern
    {
        public TimeConstrainedProcessTracker(
                string tenantId,
                Identity<Process> processId,
                string description,
                DateTime originalStartTime,
                long allowableDuration,
                int totalRetriesPermitted,
                string processTimedOutEventType)
        {
            this.AllowableDuration = allowableDuration;
            this.Description = description;
            this.ProcessId = processId;
            this.ProcessInformedOfTimeout = false;
            this.ProcessTimedOutEventType = processTimedOutEventType;
            this.TenantId = tenantId;
            this.TimeConstrainedProcessTrackerId = -1L;
            this.TimeoutOccursOn = originalStartTime.Ticks + allowableDuration;
            this.TotalRetriesPermitted = totalRetriesPermitted;
        }

        public long AllowableDuration { get; private set; }

        public bool Completed { get; private set; }

        public int ConcurrencyVersion { get; private set; }

        public string Description { get; private set; }

        public Identity<Process> ProcessId { get; private set; }

        public bool ProcessInformedOfTimeout { get; private set; }

        public string ProcessTimedOutEventType { get; private set; }

        public int RetryCount { get; private set; }

        public string TenantId { get; private set; }

        public long TimeConstrainedProcessTrackerId { get; private set; }

        public long TimeoutOccursOn { get; private set; }

        public int TotalRetriesPermitted { get; private set; }

        public bool HasTimedOut()
        {
            long timeout = this.TimeoutOccursOn;
            long now = DateTime.Now.Ticks;

            return (timeout <= now);
        }

        public void InformProcessTimedOut()
        {
            if (!this.ProcessInformedOfTimeout && this.HasTimedOut())
            {
                ProcessTimedOut processTimedOut = null;

                if (this.TotalRetriesPermitted == 0)
                {
                    processTimedOut = this.ProcessTimedOutEvent();

                    this.ProcessInformedOfTimeout = true;
                }
                else
                {
                    this.IncrementRetryCount();

                    processTimedOut = this.ProcessTimedOutEventWithRetries();

                    if (this.TotalRetriesReached())
                    {
                        this.ProcessInformedOfTimeout = true;
                    }
                    else
                    {
                        this.TimeoutOccursOn = this.TimeoutOccursOn + this.AllowableDuration;
                    }
                }

                DomainEventPublisher.Instance.Publish(processTimedOut);
            }
        }

        public void MarkProcessCompleted()
        {
            this.Completed = true;
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                TimeConstrainedProcessTracker typedObject = (TimeConstrainedProcessTracker)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.ProcessId.Equals(typedObject.ProcessId);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (79157 * 107)
                + this.TenantId.GetHashCode()
                + this.ProcessId.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "TimeConstrainedProcessTracker [allowableDuration=" + AllowableDuration + ", completed=" + Completed
                    + ", description=" + Description + ", processId=" + ProcessId + ", processInformedOfTimeout="
                    + ProcessInformedOfTimeout + ", processTimedOutEventType=" + ProcessTimedOutEventType + ", retryCount="
                    + RetryCount + ", tenantId=" + TenantId + ", timeConstrainedProcessTrackerId=" + TimeConstrainedProcessTrackerId
                    + ", timeoutOccursOn=" + TimeoutOccursOn + ", totalRetriesPermitted=" + TotalRetriesPermitted + "]";
        }

        private void IncrementRetryCount()
        {
            this.RetryCount = this.RetryCount + 1;
        }

        private ProcessTimedOut ProcessTimedOutEvent()
        {
            ProcessTimedOut processTimedOut = null;

            try
            {
                Type type = Type.GetType(this.ProcessTimedOutEventType);

                processTimedOut = (ProcessTimedOut)Activator.CreateInstance(
                                        type,
                                        new Object[] { this.ProcessId });
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                        "Error creating new ProcessTimedOut instance because: "
                        + e.Message);
            }

            return processTimedOut;
        }

        private ProcessTimedOut ProcessTimedOutEventWithRetries()
        {
            ProcessTimedOut processTimedOut = null;

            try
            {
                Type type = Type.GetType(this.ProcessTimedOutEventType);

                processTimedOut = (ProcessTimedOut)Activator.CreateInstance(
                                        type,
                                        new Object[] {
                                            this.ProcessId,
                                            this.TotalRetriesPermitted,
                                            this.RetryCount } );
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                        "Error creating new ProcessTimedOut instance because: "
                        + e.Message);
            }

            return processTimedOut;
        }

        private bool TotalRetriesReached()
        {
            return this.RetryCount >= this.TotalRetriesPermitted;
        }
    }
}
