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

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public abstract class Member : EntityWithCompositeId
    {
        public Member(
            TenantId tenantId,
            string userName,
            string firstName,
            string lastName,
            string emailAddress,
            DateTime initializedOn)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenant id must be provided.");

            this.TenantId = tenantId;
            this.EmailAddress = emailAddress;
            this.Enabled = true;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.changeTracker = new MemberChangeTracker(initializedOn, initializedOn, initializedOn);
        }

        string userName;
        string emailAddress;        
        string firstName;
        string lastName;

        public TenantId TenantId { get; private set; }

        public string Username
        {
            get { return this.userName; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The username must be provided.");
                AssertionConcern.AssertArgumentLength(value, 250, "The username must be 250 characters or less.");
                this.userName = value;
            }
        }

        public string EmailAddress
        {
            get { return this.emailAddress; }
            private set
            {
                if (value != null)
                    AssertionConcern.AssertArgumentLength(emailAddress, 100, "Email address must be 100 characters or less.");
                this.emailAddress = value;
            }
        }        

        public string FirstName
        {
            get { return this.firstName; }
            private set
            {
                if (value != null)
                    AssertionConcern.AssertArgumentLength(value, 50, "First name must be 50 characters or less.");
                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            private set
            {
                if (value != null)
                    AssertionConcern.AssertArgumentLength(value, 50, "Last name must be 50 characters or less.");
                this.lastName = value;
            }
        }

        public bool Enabled { get; private set; }

        MemberChangeTracker changeTracker;

        public void ChangeEmailAddress(string emailAddress, DateTime asOfDate)
        {
            if (this.changeTracker.CanChangeEmailAddress(asOfDate) 
                && !this.EmailAddress.Equals(emailAddress))
            {
                this.EmailAddress = emailAddress;
                this.changeTracker = this.changeTracker.EmailAddressChangedOn(asOfDate);
            }
        }

        public void ChangeName(string firstName, string lastName, DateTime asOfDate)
        {
            if (this.changeTracker.CanChangeName(asOfDate))
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.changeTracker = this.changeTracker.NameChangedOn(asOfDate);
            }
        }

        public void Disable(DateTime asOfDate)
        {
            if (this.changeTracker.CanToggleEnabling(asOfDate))
            {
                this.Enabled = false;
                this.changeTracker = this.changeTracker.EnablingOn(asOfDate);
            }
        }

        public void Enable(DateTime asOfDate)
        {
            if (this.changeTracker.CanToggleEnabling(asOfDate))
            {
                this.Enabled = true;
                this.changeTracker = this.changeTracker.EnablingOn(asOfDate);
            }
        }
    }
}
