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
    using SaaSOvation.Common.Domain;

    public class User
    {
        internal User(
                TenantId tenantId,
                string username,
                string password,
                Enablement enablement,
                Person person)

            : this()
        {
            this.Enablement = enablement;
            this.Person = person;
            this.TenantId = tenantId;
            this.Username = username;

            this.ProtectPassword("", password);

            person.User = this;

            DomainEventPublisher
                .Instance
                .Publish(new UserRegistered(
                        tenantId,
                        username,
                        person.Name,
                        person.ContactInformation.EmailAddress));
        }

        internal User()
        {
        }

        public bool Enabled
        {
            get
            {
                return this.Enablement.IsEnablementEnabled();
            }
        }

        public Enablement Enablement { get; private set; }

        public string Password { get; internal set; }

        public Person Person { get; private set; }

        public TenantId TenantId { get; private set; }

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

        public bool IsEnabled()
        {
            return this.Enablement.IsEnablementEnabled();
        }

        public override bool Equals(object anotherObject)
        {
            bool equalobjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                User typedobject = (User) anotherObject;
                equalobjects =
                    this.TenantId.Equals(typedobject.TenantId) &&
                    this.Username.Equals(typedobject.Username);
            }

            return equalobjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (45217 * 269)
                + this.TenantId.GetHashCode()
                + this.Username.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "User [tenantId=" + TenantId + ", username=" + Username
                    + ", person=" + Person + ", enablement=" + Enablement + "]";
        }

        internal GroupMember ToGroupMember()
        {
            GroupMember groupMember =
                new GroupMember(
                        this.TenantId,
                        this.Username,
                        GroupMemberType.User);

            return groupMember;
        }

        string AsEncryptedValue(string plainTextPassword)
        {
            string encryptedValue =
                DomainRegistry
                    .EncryptionService
                    .EncryptedValue(plainTextPassword);

            return encryptedValue;
        }

        void ProtectPassword(string currentPassword, string changedPassword)
        {
            AssertionConcern.AssertArgumentNotEquals(currentPassword, changedPassword, "The password is unchanged.");

            AssertionConcern.AssertArgumentFalse(DomainRegistry.PasswordService.IsWeak(changedPassword), "The password must be stronger.");

            AssertionConcern.AssertArgumentNotEquals(this.Username, changedPassword, "The username and password must not be the same.");

            this.Password = AsEncryptedValue(changedPassword);
        }
    }
}
