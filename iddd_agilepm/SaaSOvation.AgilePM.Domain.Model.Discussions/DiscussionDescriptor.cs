namespace SaaSOvation.AgilePM.Domain.Model.Discussions
{
    public class DiscussionDescriptor
    {
        public const string UNDEFINED_ID = "UNDEFINED";

        public DiscussionDescriptor(string id)
        {
            this.Id = id;
        }

        public DiscussionDescriptor(DiscussionDescriptor discussionDescriptor)
            : this(discussionDescriptor.Id)
        {
        }

        public string Id { get; private set; }

        public bool IsUndefined()
        {
            return this.Id.Equals(UNDEFINED_ID);
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                DiscussionDescriptor typedObject = (DiscussionDescriptor)anotherObject;
                equalObjects = this.Id.Equals(typedObject.Id);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (72881 * 101)
                + this.Id.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "DiscussionDescriptor [id=" + Id + "]";
        }
    }
}
