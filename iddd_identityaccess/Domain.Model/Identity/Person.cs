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

    public class Person : EntityWithCompositeId
    {
        public Person(TenantId tenantId, FullName name, ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;
            this.Name = name;
            this.TenantId = tenantId;
        }

        protected Person() { }

        TenantId tenantId;

        public TenantId TenantId
        {
            get { return this.tenantId; }
            internal set 
            {
                AssertionConcern.AssertArgumentNotNull(value, "The tenantId is required.");
                this.tenantId = value;
            }
        }

        FullName name;

        public FullName Name
        {
            get { return this.name; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The person name is required.");
                this.name = value;
            }
        }

        public User User { get; internal set; }

        ContactInformation contactInformation;

        public ContactInformation ContactInformation
        {
            get { return this.contactInformation; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The person contact information is required.");
                this.contactInformation = value;
            }
        }

        public EmailAddress EmailAddress
        {
            get
            {
                return this.ContactInformation.EmailAddress;
            }
        }

        public void ChangeContactInformation(ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;

            DomainEventPublisher
                .Instance
                .Publish(new PersonContactInformationChanged(
                        this.TenantId,
                        this.User.Username,
                        this.ContactInformation));
        }

        public void ChangeName(FullName name)
        {
            this.Name = name;

            DomainEventPublisher
                .Instance
                .Publish(new PersonNameChanged(
                        this.TenantId,
                        this.User.Username,
                        this.Name));
        }

        protected override System.Collections.Generic.IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.User.Username;
        }

        public override string ToString()
        {
            return "Person [tenantId=" + TenantId
                + ", name=" + Name
                + ", contactInformation=" + ContactInformation + "]";
        }
    }
}
