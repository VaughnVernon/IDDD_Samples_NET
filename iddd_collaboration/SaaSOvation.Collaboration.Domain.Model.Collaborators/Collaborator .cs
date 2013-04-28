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
