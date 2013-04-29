namespace SaaSOvation.AgilePM.Domain.Model.Tenants
{
    using SaaSOvation.Common.Domain.Model;

    public class TenantId : Identity
    {
        public TenantId()
            : base()
        {
        }

        public TenantId(string id)
            : base(id)
        {
        }
    }
}
