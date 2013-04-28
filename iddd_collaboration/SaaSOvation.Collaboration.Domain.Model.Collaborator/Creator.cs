namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public class Creator : Collaborator
    {
        private const long SerialVersionUid = 1L;

        public Creator(string anIdentity, string aName, string anEmailAddress)
            : base(anIdentity, aName, anEmailAddress)
        { }

        protected Creator()
        {}

        protected override int GetHashPrimeValue()
        {
            return 43;
        }
    }
}
