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

    public class User : EntityWithCompositeId
    {
        public User(
                TenantId tenantId,
                string username,
                string password,
                Enablement enablement,
                Person person)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId is required.");
            AssertionConcern.AssertArgumentNotNull(person, "The person is required.");
            AssertionConcern.AssertArgumentNotEmpty(username, "The username is required.");
            AssertionConcern.AssertArgumentLength(username, 3, 250, "The username must be 3 to 250 characters.");

            this.Enablement = enablement;
            this.Person = person;
            this.TenantId = tenantId;
            this.Username = username;

            ProtectPassword("", password);

            person.User = this;

            DomainEventPublisher
                .Instance
                .Publish(new UserRegistered(
                        tenantId,
                        username,
                        person.Name,
                        person.ContactInformation.EmailAddress));
        }

        protected User() { }

        public TenantId TenantId { get; private set; }

        public bool IsEnabled
        {
            get
            {
                return this.Enablement.IsEnablementEnabled();
            }
        }

        Enablement enablement;

        public Enablement Enablement
        {
            get { return this.enablement; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The enablement is required.");
                this.enablement = value;
            }
        }

        public string Password { get; private set; }

        public Person Person { get; private set; }

        public UserDescriptor UserDescriptor
        {
            get
            {
                return new UserDescriptor(
                        this.TenantId,
                        this.Username,
                        this.Person.EmailAddress.Address);
            }
        }

        public string Username { get; private set; }

        public void ChangePassword(string currentPassword, string changedPassword)
        {
            AssertionConcern.AssertArgumentNotEmpty(
                    currentPassword,
                    "Current and new password must be provided.");

            AssertionConcern.AssertArgumentEquals(
                    this.Password,
                    this.AsEncryptedValue(currentPassword),
                    "Current password not confirmed.");

            this.ProtectPassword(currentPassword, changedPassword);

            DomainEventPublisher.Instance.Publish(new UserPasswordChanged(this.TenantId, this.Username));
        }

        public void ChangePersonalContactInformation(ContactInformation contactInformation)
        {
            this.Person.ChangeContactInformation(contactInformation);
        }

        public void ChangePersonalName(FullName personalName)
        {
            this.Person.ChangeName(personalName);
        }

        public void DefineEnablement(Enablement enablement)
        {
            this.Enablement = enablement;

            DomainEventPublisher
                .Instance
                .Publish(new UserEnablementChanged(
                        this.TenantId,
                        this.Username,
                        this.Enablement));
        }

        internal GroupMember ToGroupMember()
        {
            return new GroupMember(
                        this.TenantId,
                        this.Username,
                        GroupMemberType.User);
        }

        string AsEncryptedValue(string plainTextPassword)
        {
            return DomainRegistry.EncryptionService.EncryptedValue(plainTextPassword);
        }

        void ProtectPassword(string currentPassword, string changedPassword)
        {
            AssertionConcern.AssertArgumentNotEquals(currentPassword, changedPassword, "The password is unchanged.");

            AssertionConcern.AssertArgumentFalse(DomainRegistry.PasswordService.IsWeak(changedPassword), "The password must be stronger.");

            AssertionConcern.AssertArgumentNotEquals(this.Username, changedPassword, "The username and password must not be the same.");

            this.Password = AsEncryptedValue(changedPassword);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetIdentityComponents()
        {
            yield return this.TenantId;
            yield return this.Username;
        }

        public override string ToString()
        {
            return "User [tenantId=" + TenantId + ", username=" + Username
                    + ", person=" + Person + ", enablement=" + Enablement + "]";
        }
    }
}
