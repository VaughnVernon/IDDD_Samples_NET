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

namespace SaaSOvation.IdentityAccess.Domain.Identity
{
    using System;
    using SaaSOvation.Common.Domain;

    public class TenantAdministratorRegistered : IDomainEvent
    {
        public TenantAdministratorRegistered(
            TenantId tenantId,
            string name,
            FullName administorName,
            EmailAddress emailAddress,
            string username,
            string temporaryPassword)
        {
            this.AdministorName = administorName;
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TemporaryPassword = temporaryPassword;
            this.TenantId = tenantId.Id;
        }

        public FullName AdministorName { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TemporaryPassword { get; private set; }

        public string TenantId { get; private set; }
    }

    public class GroupProvisioned : IDomainEvent
    {
        public GroupProvisioned(TenantId tenantId, string name)
        {
            this.EventVersion = 1;
            this.Name = name;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantActivated : IDomainEvent
    {
        public TenantActivated(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public  DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantDeactivated : IDomainEvent
    {
        public TenantDeactivated(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }

    public class TenantProvisioned : IDomainEvent
    {
        public TenantProvisioned(TenantId tenantId)
        {
            this.EventVersion = 1;
            this.OccurredOn = new DateTime();
            this.TenantId = tenantId.Id;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public string TenantId { get; private set; }
    }
}
