// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using System.Collections.Generic;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Access;

    public class Tenant
    {
        public Tenant(string name, string description, bool active)
        {
            this.Active = active;
            this.Description = description;
            this.Name = name;
            this.RegistrationInvitations = new HashSet<RegistrationInvitation>();
            this.TenantId = new TenantId();
        }

        public bool Active { get; private set; }

        public string Description { get; private set; }

        public string Name { get; private set; }

        public TenantId TenantId { get; private set; }

        private ISet<RegistrationInvitation> RegistrationInvitations { get; set; }

        public void Activate()
        {
            if (!this.Active)
            {
                this.Active = true;

                DomainEventPublisher.Instance.Publish(new TenantActivated(this.TenantId));
            }
        }

        public ICollection<InvitationDescriptor> AllAvailableRegistrationInvitations()
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            return this.AllRegistrationInvitationsFor(true);
        }

        public ICollection<InvitationDescriptor> AllUnavailableRegistrationInvitations()
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            return this.AllRegistrationInvitationsFor(false);
        }

        public void Deactivate()
        {
            if (this.Active)
            {
                this.Active = false;

                DomainEventPublisher.Instance.Publish(new TenantDeactivated(this.TenantId));
            }
        }

        public bool IsRegistrationAvailableThrough(string invitationIdentifier)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            RegistrationInvitation invitation = this.InvitationOf(invitationIdentifier);

            return invitation == null ? false : invitation.IsAvailable();
        }

        public RegistrationInvitation OfferRegistrationInvitation(string description)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");
            AssertionConcern.AssertArgumentFalse(this.IsRegistrationAvailableThrough(description), "Invitation already exists.");

            RegistrationInvitation invitation =
                new RegistrationInvitation(
                        this.TenantId,
                        new Guid().ToString(),
                        description);

            AssertionConcern.AssertStateTrue(this.RegistrationInvitations.Add(invitation), "The invitation should have been added.");

            return invitation;
        }

        public Group ProvisionGroup(string name, string description)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            Group group = new Group(this.TenantId, name, description);

            DomainEventPublisher.Instance.Publish(new GroupProvisioned(this.TenantId, name));

            return group;
        }

        public Role ProvisionRole(string name, string description)
        {
            return this.ProvisionRole(name, description, false);
        }

        public Role ProvisionRole(string name, string description, bool supportsNesting)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            Role role = new Role(this.TenantId, name, description, supportsNesting);

            DomainEventPublisher.Instance.Publish(new RoleProvisioned(this.TenantId, name));

            return role;
        }

        public RegistrationInvitation RedefineRegistrationInvitationAs(string invitationIdentifier)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            RegistrationInvitation invitation = this.InvitationOf(invitationIdentifier);

            if (invitation != null)
            {
                invitation.RedefineAs().OpenEnded();
            }

            return invitation;
        }

        public User RegisterUser(
                string invitationIdentifier,
                string username,
                string password,
                Enablement enablement,
                Person person)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            User user = null;

            if (this.IsRegistrationAvailableThrough(invitationIdentifier))
            {
                // ensure same tenant
                person.TenantId = this.TenantId;

                user = new User(this.TenantId, username, password, enablement, person);
            }

            return user;
        }

        public void WithdrawInvitation(string invitationIdentifier)
        {
            RegistrationInvitation invitation = this.InvitationOf(invitationIdentifier);

            if (invitation != null)
            {
                this.RegistrationInvitations.Remove(invitation);
            }
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                Tenant typedObject = (Tenant) anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.Name.Equals(typedObject.Name);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (48123 * 257)
                + this.TenantId.GetHashCode()
                + this.Name.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "Tenant ["
                    + ", tenantId=" + TenantId
                    + ", name=" + Name
                    + ", description=" + Description
                    + ", active=" + Active
                    + "]";
        }

        private ICollection<InvitationDescriptor> AllRegistrationInvitationsFor(bool isAvailable)
        {
            ISet<InvitationDescriptor> allInvitations = new HashSet<InvitationDescriptor>();

            foreach (RegistrationInvitation invitation in this.RegistrationInvitations)
            {
                if (invitation.IsAvailable() == isAvailable)
                {
                    allInvitations.Add(invitation.ToDescriptor());
                }
            }

            return allInvitations;
        }

        private RegistrationInvitation InvitationOf(string invitationIdentifier)
        {
            foreach (RegistrationInvitation invitation in this.RegistrationInvitations)
            {
                if (invitation.IsIdentifiedBy(invitationIdentifier))
                {
                    return invitation;
                }
            }

            return null;
        }
    }
}
