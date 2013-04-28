using System;

namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public abstract class Collaborator : IComparable<Collaborator>
    {
        private const long SerialVersionUid = 1L;

        public string Identity { get; private set; }
        public string Name { get; private set; }
        public string EmailAddress { get; private set; }

        protected Collaborator()
        {}

        protected Collaborator(string anIdentity, string aName, string anEmailAddress)
        {
            Identity = anIdentity;
            Name = aName;
            EmailAddress = anEmailAddress;
        }

        public int CompareTo(Collaborator aCollaborator)
        {
            var diff = String.Compare(Identity, aCollaborator.Identity, StringComparison.Ordinal);

            if (diff == 0)
            {
                diff = String.Compare(EmailAddress, aCollaborator.EmailAddress, StringComparison.Ordinal);

                if (diff == 0)
                {
                    diff = String.Compare(Name, aCollaborator.Name, StringComparison.Ordinal);
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
