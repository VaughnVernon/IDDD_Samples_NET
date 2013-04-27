namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class FullName : AssertionConcern
    {
        public FullName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public FullName(FullName fullName)
            : this(fullName.FirstName, fullName.LastName)
        {
        }

        internal FullName()
        {
        }


        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public String asFormattedName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public FullName WithChangedFirstName(string firstName)
        {
            return new FullName(firstName, this.LastName);
        }

        public FullName WithChangedLastName(string lastName)
        {
            return new FullName(this.FirstName, lastName);
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType()) {
                FullName typedObject = (FullName) anotherObject;
                equalObjects =
                    this.FirstName.Equals(typedObject.FirstName) &&
                    this.LastName.Equals(typedObject.LastName);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (59151 * 191)
                + this.FirstName.GetHashCode()
                + this.LastName.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "FullName [firstName=" + FirstName + ", lastName=" + LastName + "]";
        }
    }
}
