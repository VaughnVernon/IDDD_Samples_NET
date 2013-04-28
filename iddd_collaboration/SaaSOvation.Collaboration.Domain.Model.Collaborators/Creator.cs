namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    public sealed class Creator : Collaborator
    {
        public Creator(string identity, string name, string emailAddress)
            : base(identity, name, emailAddress)
        {
        }

        protected override int GetHashPrimeValue()
        {
            return 43;
        }
    }
}
