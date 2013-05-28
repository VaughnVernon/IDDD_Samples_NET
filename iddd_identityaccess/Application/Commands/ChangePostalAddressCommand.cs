using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangePostalAddressCommand
    {
        public ChangePostalAddressCommand(string tenantId, string username,
            string addressStreetAddress, string addressCity, string addressStateProvince, string addressPostalCode, string addressCountryCode)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.AddressStreetAddress = addressStreetAddress;
            this.AddressCity = addressCity;
            this.AddressStateProvince = addressStateProvince;
            this.AddressPostalCode = addressPostalCode;
            this.AddressCountryCode = addressCountryCode;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string AddressStreetAddress { get; set; }
        public string AddressCity { get; set; }
        public string AddressStateProvince { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountryCode { get; set; }
    }
}
