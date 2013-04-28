namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    public sealed class Author : Collaborator
    {
        public Author(string identity, string name, string emailAddress)
            : base(identity, name, emailAddress)
        {
        }

        protected override int GetHashPrimeValue()
        {
            return 19;
        }
    }
}
