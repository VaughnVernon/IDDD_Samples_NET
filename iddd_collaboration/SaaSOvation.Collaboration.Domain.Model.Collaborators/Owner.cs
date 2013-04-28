namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    public sealed class Owner : Collaborator
    {
        public Owner(string identity, string name, string emailAddress)
            : base(identity, name, emailAddress)
        {
        }

        protected override int GetHashPrimeValue()
        {
            return 29;
        }
    }
}
