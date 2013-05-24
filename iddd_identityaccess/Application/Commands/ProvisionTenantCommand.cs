using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ProvisionTenantCommand
    {
        public ProvisionTenantCommand()
        {
        }

        public ProvisionTenantCommand(string tenantName, string tenantDescription, string administorFirstName,
            string administorLastName, string emailAddress, string primaryTelephone, string secondaryTelephone,
            string addressStreetAddress, string addressCity, string addressStateProvince, string addressPostalCode,
            string addressCountryCode)
        {
            this.TenantName = tenantName;
            this.TenantDescription = tenantDescription;
            this.AdministorFirstName = administorFirstName;
            this.AdministorLastName = administorLastName;
            this.EmailAddress = emailAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
            this.AddressStreetAddress = addressStreetAddress;
            this.AddressCity = addressCity;
            this.AddressStateProvince = addressStateProvince;
            this.AddressPostalCode = addressPostalCode;
            this.AddressCountryCode = addressCountryCode;
        }

        public string TenantName { get; set; }
        public string TenantDescription { get; set; }
        public string AdministorFirstName { get; set; }
        public string AdministorLastName { get; set; }
        public string EmailAddress { get; set; }
        public string PrimaryTelephone { get; set; }
        public string SecondaryTelephone { get; set; }
        public string AddressStreetAddress { get; set; }
        public string AddressCity { get; set; }
        public string AddressStateProvince { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountryCode { get; set; }
    }
}
