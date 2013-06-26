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

namespace SaaSOvation.Common.Domain.Model.LongRunningProcess
{
    using System;

    public interface IProcess
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

        ProcessId ProcessId();

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
