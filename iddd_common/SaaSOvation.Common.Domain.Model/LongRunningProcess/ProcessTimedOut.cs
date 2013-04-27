namespace SaaSOvation.Common.Domain.Model.LongRunningProcess
{
    using System;

    public class ProcessTimedOut : DomainEvent
    {
        public ProcessTimedOut(
                string tenantId,
                Identity<Process> processId,
                int totalRetriesPermitted,
                int retryCount)
        {
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
            this.ProcessId = processId;
            this.RetryCount = retryCount;
            this.TenantId = tenantId;
            this.TotalRetriesPermitted = totalRetriesPermitted;
        }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
        public Identity<Process> ProcessId { get; private set; }
        public int RetryCount { get; private set; }
        public string TenantId { get; private set; }
        public int TotalRetriesPermitted { get; private set; }
    }
}
