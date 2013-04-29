namespace SaaSOvation.AgilePM.Domain.Model.Tenants
{
    using SaaSOvation.Common.Domain.Model;

    public class BacklogItemId : Identity
    {
        public BacklogItemId()
            : base()
        {
        }

        public BacklogItemId(string id)
            : base(id)
        {
        }
    }
}
