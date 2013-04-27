namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class InvitationDescriptor
    {
        public InvitationDescriptor(Identity<Tenant> tenantId, string invitationId, string description, DateTime startingOn, DateTime until)
        {
            this.Description = description;
            this.InvitationId = invitationId;
            this.StartingOn = startingOn;
            this.TenantId = tenantId.Id;
            this.Until = until;
        }

        public string Description { get; private set; }

        public string InvitationId { get; private set; }

        public DateTime StartingOn;

        public string TenantId { get; private set; }

        public DateTime Until { get; private set; }
    }
}
