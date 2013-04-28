
namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public class Participant : Collaborator
    {
        private const long SerialVersionUid = 1L;

        public Participant(string anIdentity, string aName, string anEmailAddress)
            : base(anIdentity, aName, anEmailAddress)
        { }

        protected Participant()
        {}

        protected override int GetHashPrimeValue()
        {
            return 23;
        }
    }
}
