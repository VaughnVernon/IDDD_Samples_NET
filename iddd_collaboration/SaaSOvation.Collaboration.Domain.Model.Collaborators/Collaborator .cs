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

namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    using System;

    public abstract class Collaborator : IComparable<Collaborator>
    {
        protected Collaborator()
        {
        }

        protected Collaborator(string identity, string name, string emailAddress)
        {
            Identity = identity;
            Name = name;
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }
        public string Identity { get; private set; }
        public string Name { get; private set; }

        public int CompareTo(Collaborator collaborator)
        {
            var diff = String.Compare(Identity, collaborator.Identity, StringComparison.Ordinal);

            if (diff == 0)
            {
                diff = String.Compare(EmailAddress, collaborator.EmailAddress, StringComparison.Ordinal);

                if (diff == 0)
                {
                    diff = String.Compare(Name, collaborator.Name, StringComparison.Ordinal);
                }
            }

            return diff;
        }

        public override bool Equals(object anObject)
        {
            var equalObjects = false;

            if (anObject != null && GetType() == anObject.GetType())
            {
                var typedObject = (Collaborator)anObject;
                equalObjects =
                    EmailAddress.Equals(typedObject.EmailAddress) &&
                    Identity.Equals(typedObject.Identity) &&
                    Name.Equals(typedObject.Name);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            var hash = 57691 * GetHashPrimeValue()
                       + Identity.GetHashCode()
                       + Name.GetHashCode()
                       + EmailAddress.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return GetType().Name +
               " [emailAddress=" + EmailAddress + ", identity=" + Identity + ", Name=" + Name + "]";
        }

        protected abstract int GetHashPrimeValue();
    }
}
