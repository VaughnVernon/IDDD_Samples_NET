using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Products
{
    public class TimeOutProductDiscussionRequestCommand
    {
        public TimeOutProductDiscussionRequestCommand()
        {
        }

        public TimeOutProductDiscussionRequestCommand(string tenantId, string processId, DateTime timeOutDate)
        {
            this.TenantId = tenantId;
            this.ProcessId = processId;
            this.TimeOutDate = timeOutDate;
        }

        public string TenantId { get; set; }

        public string ProcessId { get; set; }

        public DateTime TimeOutDate { get; set; }
    }
}
