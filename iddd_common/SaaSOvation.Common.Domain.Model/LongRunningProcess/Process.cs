namespace SaaSOvation.Common.Domain.Model.LongRunningProcess
{
    using System;

    public interface Process
    {
        long AllowableDuration();

        bool CanTimeout();

        long CurrentDuration();

        string Description();

        bool DidProcessingComplete();

        void InformTimeout(DateTime timedOutDate);

        bool IsCompleted();

        bool IsTimedOut();

        bool NotCompleted();

        ProcessCompletionType ProcessCompletionType();

        Identity<Process> ProcessId();

        DateTime StartTime();

        TimeConstrainedProcessTracker TimeConstrainedProcessTracker();

        DateTime TimedOutDate();

        long TotalAllowableDuration();

        int TotalRetriesPermitted();
    }

    public enum ProcessCompletionType
    {
        NotCompleted,
        CompletedNormally,
        TimedOut
    }
}
