using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Common.Domain.Model
{
    public abstract class Identity
    {
        public Identity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Identity(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                Identity typedObject = (Identity)anotherObject;
                equalObjects = this.Id.Equals(typedObject.Id);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (this.GetType().GetHashCode() * 907)
                + this.Id.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return this.GetType().Name + " [Id=" + Id + "]";
        }
    }
}
