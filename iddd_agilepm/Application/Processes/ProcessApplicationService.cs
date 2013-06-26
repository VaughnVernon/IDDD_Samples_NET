using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model.LongRunningProcess;

namespace SaaSOvation.AgilePM.Application.Processes
{
    public class ProcessApplicationService
    {
        public ProcessApplicationService(ITimeConstrainedProcessTrackerRepository processTrackerRepository)
        {
            this.processTrackerRepository = processTrackerRepository;
        }

        readonly ITimeConstrainedProcessTrackerRepository processTrackerRepository;

        public void CheckForTimedOutProccesses()
        {
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var trackers = this.processTrackerRepository.GetAllTimedOut();

                foreach (var tracker in trackers)
                {
                    tracker.InformProcessTimedOut();
                    this.processTrackerRepository.Save(tracker);
                }

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }
    }
}
