namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public class Moderator : Collaborator
    {
        private const long SerialVersionUid = 1L;

        public Moderator(string anIdentity, string aName, string anEmailAddress)
            : base(anIdentity, aName, anEmailAddress)
        { }

        protected Moderator()
        {}

        protected override int GetHashPrimeValue()
        {
            return 59;
        }
    }
}
