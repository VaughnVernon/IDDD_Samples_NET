namespace SaaSOvation.Collaboration.Domain.Model.Tenants
{
    public sealed class Tenant
    {
        public Tenant(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}
