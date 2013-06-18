using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Products
{
    public class InitiateDiscussionCommand
    {
        public InitiateDiscussionCommand()
        {
        }

        public InitiateDiscussionCommand(string tenantId, string discussionId, string productId)
        {
            this.TenantId = tenantId;
            this.DiscussionId = discussionId;
            this.ProductId = productId;
        }

        public string TenantId { get; set; }

        public string DiscussionId { get; set; }

        public string ProductId { get; set; }
    }
}
