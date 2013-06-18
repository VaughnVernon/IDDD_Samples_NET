using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Products
{
    public class NewProductCommand
    {
        public NewProductCommand()
        {
        }

        public NewProductCommand(string tenantId, string productOwnerId, string name, string description)
        {
            this.TenantId = tenantId;
            this.ProductOwnerId = productOwnerId;
            this.Name = name;
            this.Description = description;
        }

        public string TenantId { get; set; }

        public string ProductOwnerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
