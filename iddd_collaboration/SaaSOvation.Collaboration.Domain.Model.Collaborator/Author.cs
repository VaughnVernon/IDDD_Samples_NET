namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public class Author : Collaborator
    {
        private const long SerialVersionUid = 1L;

        public Author(string anIdentity, string aName, string anEmailAddress)
            : base(anIdentity, aName, anEmailAddress)
        { }

        protected Author()
        {}

        protected override int GetHashPrimeValue()
        {
            return 19;
        }
    }
}
