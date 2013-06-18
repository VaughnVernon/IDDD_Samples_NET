using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model.LongRunningProcess;

namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    public class ProductDiscussionRequestTimedOut : ProcessTimedOut
    {
        public ProductDiscussionRequestTimedOut(string tenantId, ProcessId processId, int totalRetriedPermitted, int retryCount)
            : base(tenantId, processId, totalRetriedPermitted, retryCount)
        {
        }
    }
}
