namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    public sealed class Moderator : Collaborator
    {
        public Moderator(string identity, string name, string emailAddress)
            : base(identity, name, emailAddress)
        {
        }

        protected override int GetHashPrimeValue()
        {
            return 59;
        }
    }
}
