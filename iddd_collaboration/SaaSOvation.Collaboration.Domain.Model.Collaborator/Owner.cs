namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public class Owner : Collaborator
    {
        private const long SerialVersionUid = 1L;

        public Owner(string anIdentity, string aName, string anEmailAddress)
            : base(anIdentity, aName, anEmailAddress)
        { }

        protected Owner()
        {}

        protected override int GetHashPrimeValue()
        {
            return 29;
        }
    }
}
