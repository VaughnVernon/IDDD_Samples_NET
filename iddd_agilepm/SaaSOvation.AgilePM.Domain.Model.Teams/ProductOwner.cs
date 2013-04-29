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

    public class ProductOwner : Member
    {
        public ProductOwner(
            TenantId tenantId,
            string username,
            string firstName,
            string lastName,
            string emailAddress,
            DateTime initializedOn)
            : base(tenantId, username, firstName, lastName, emailAddress, initializedOn)
        {
        }

        public ProductOwnerId ProductOwnerId { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                ProductOwner typedObject = (ProductOwner)anotherObject;
                equalObjects =
                    this.TenantId.Equals(typedObject.TenantId) &&
                    this.Username.Equals(typedObject.Username);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (71121 * 79)
                + this.TenantId.GetHashCode()
                + this.Username.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "ProductOwner [productOwnerId()=" + ProductOwnerId + ", emailAddress()=" + EmailAddress + ", isEnabled()="
                    + Enabled + ", firstName()=" + FirstName + ", lastName()=" + LastName + ", tenantId()=" + TenantId
                    + ", username()=" + Username + "]";
        }
    }
}
