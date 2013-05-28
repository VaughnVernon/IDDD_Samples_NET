using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand()
        {
        }

        public RegisterUserCommand(String tenantId, String invitationIdentifier, String username, String password, String firstName,
            String lastName, bool enabled, DateTime startDate, DateTime endDate, String emailAddress, String primaryTelephone,
            String secondaryTelephone, String addressStreetAddress, String addressCity, String addressStateProvince,
            String addressPostalCode, String addressCountryCode)
        {

            this.TenantId = tenantId;
            this.InvitationIdentifier = invitationIdentifier;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Enabled = enabled;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.EmailAddress = emailAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
            this.AddressStreetAddress = addressStreetAddress;
            this.AddressCity = addressCity;
            this.AddressStateProvince = addressStateProvince;
            this.AddressPostalCode = addressPostalCode;
            this.AddressCountryCode = addressCountryCode;
        }

        public string TenantId { get; set; }
        public string InvitationIdentifier { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
