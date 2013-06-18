using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.AgilePM.Application.Teams
{
    public class EnableMemberCommand
    {
        public EnableMemberCommand()
        {
        }

        public EnableMemberCommand(string tenantId, string username, string firstName, string lastName, string emailAddress, DateTime occurredOn)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.OccurredOn = occurredOn;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
