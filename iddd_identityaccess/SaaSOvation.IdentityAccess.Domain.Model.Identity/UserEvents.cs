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

    public class PersonContactInformationChanged : DomainEvent
    {
        public PersonContactInformationChanged(
                TenantId tenantId,
                String username,
                ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public ContactInformation ContactInformation { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class PersonNameChanged : DomainEvent
    {
        public PersonNameChanged(
                TenantId tenantId,
                String username,
                FullName name)
        {
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public FullName Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserEnablementChanged : DomainEvent
    {
        public UserEnablementChanged(
                TenantId tenantId,
                String username,
                Enablement enablement)
        {
            this.Enablement = enablement;
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public Enablement Enablement { get; private set; }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserPasswordChanged : DomainEvent
    {
        public UserPasswordChanged(
                TenantId tenantId,
                String username)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }

    public class UserRegistered : DomainEvent
    {
        public UserRegistered(
                TenantId tenantId,
                String username,
                FullName name,
                EmailAddress emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
            this.Username = username;
        }

        public EmailAddress EmailAddress { get; private set; }

        public int EventVersion { get; set; }

        public FullName Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
