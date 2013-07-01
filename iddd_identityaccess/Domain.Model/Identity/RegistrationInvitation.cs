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
    using SaaSOvation.Common.Domain.Model;

    public class RegistrationInvitation
    {
        public RegistrationInvitation(
            TenantId tenantId,
            string invitationId,
            string description,
            DateTime startingOn,
            DateTime until)
        {
            this.Description = description;
            this.InvitationId = invitationId;
            this.StartingOn = startingOn;
            this.TenantId = tenantId;
            this.Until = until;
        }

        public RegistrationInvitation(TenantId tenantId, string invitationId, string description)
            : this(tenantId, invitationId, description, DateTime.MinValue, DateTime.MinValue)
        {
        }

        public string Description { get; private set; }

        public string InvitationId { get; private set; }

        public DateTime StartingOn { get; private set; }

        public TenantId TenantId { get; private set; }

        public DateTime Until { get; private set; }

        public bool IsAvailable()
        {
            var isAvailable = false;

            if (this.StartingOn == DateTime.MinValue && this.Until == DateTime.MinValue)
            {
                isAvailable = true;
            }
            else
            {
                var time = (DateTime.Now).Ticks;
                if (time >= this.StartingOn.Ticks && time <= this.Until.Ticks)
                {
                    isAvailable = true;
                }
            }

            return isAvailable;
        }

        public bool IsIdentifiedBy(string invitationIdentifier)
        {
            var isIdentified = this.InvitationId.Equals(invitationIdentifier);

            if (!isIdentified && this.Description != null)
            {
                isIdentified = this.Description.Equals(invitationIdentifier);
            }

            return isIdentified;
        }

        public RegistrationInvitation OpenEnded()
        {
            this.StartingOn = DateTime.MinValue;
            this.Until = DateTime.MinValue;
            return this;
        }

        public RegistrationInvitation RedefineAs()
        {
            this.StartingOn = DateTime.MinValue;
            this.Until = DateTime.MinValue;
            return this;
        }

        public InvitationDescriptor ToDescriptor()
        {
            return new InvitationDescriptor(
                this.TenantId,
                this.InvitationId,
                this.Description,
                this.StartingOn,
                this.Until);
        }

        public RegistrationInvitation WillStartOn(DateTime date)
        {
            if (this.Until != DateTime.MinValue)
            {
                throw new InvalidOperationException("Cannot set starting-on date after until date.");
            }

            this.StartingOn = date;

            // temporary if Until set properly follows, but
            // prevents illegal state if Until set doesn't follow
            this.Until = new DateTime(date.Ticks + 86400000);

            return this;
        }

        public RegistrationInvitation LastingUntil(DateTime date)
        {
            if (this.StartingOn == DateTime.MinValue)
            {
                throw new InvalidOperationException("Cannot set until date before setting starting-on date.");
            }

            this.Until = date;

            return this;
        }

        public override bool Equals(object anotherObject)
        {
            var equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                var typedObject = (RegistrationInvitation) anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.InvitationId.Equals(typedObject.InvitationId);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            return
                + (6325 * 233)
                + this.TenantId.GetHashCode()
                + this.InvitationId.GetHashCode();
        }

        public override string ToString()
        {
            return "RegistrationInvitation ["
                    + "tenantId=" + TenantId
                    + ", description=" + Description
                    + ", invitationId=" + InvitationId
                    + ", startingOn=" + StartingOn
                    + ", until=" + Until + "]";
        }
    }
}
