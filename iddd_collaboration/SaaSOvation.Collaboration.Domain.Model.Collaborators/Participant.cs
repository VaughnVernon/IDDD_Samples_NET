namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    public sealed class Participant : Collaborator
    {
        public Participant(string identity, string name, string emailAddress)
            : base(identity, name, emailAddress)
        {
        }

        protected override int GetHashPrimeValue()
        {
            return 23;
        }
    }
}
