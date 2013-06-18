using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Products
{
    public class StartDiscussionInitiationCommand
    {
        public StartDiscussionInitiationCommand()
        {
        }

        public StartDiscussionInitiationCommand(string tenantId, string productId)
        {
            this.TenantId = tenantId;
            this.ProductId = productId;
        }

        public string TenantId { get; set; }

        public string ProductId { get; set; }
    }
}
