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

    public class Member : Entity
    {
        protected Member(
            TenantId tenantId,
            string username,
            string firstName,
            string lastName,
            string emailAddress,
            DateTime initializedOn)
        {
            this.EmailAddress = emailAddress;
            this.Enabled = true;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ChangeTracker = new MemberChangeTracker(initializedOn, initializedOn, initializedOn);
        }

        public string EmailAddress { get; private set; }

        public bool Enabled { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public TenantId TenantId { get; private set; }

        public string Username { get; private set; }

        protected MemberChangeTracker ChangeTracker { get; private set; }

    }
}
