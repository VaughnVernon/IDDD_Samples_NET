namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using SaaSOvation.AgilePM.Domain.Model.Tenants;
    using SaaSOvation.Common.Domain.Model;

    public class ProductOwnerId : Identity
    {
        public ProductOwnerId(TenantId tenantId, string id)
            : base(tenantId + ":" + id)
        {
        }

        public TenantId TenantId
        {
            get
            {
                return new TenantId(this.Id.Split(':')[0]);
            }
        }

        public string Identity
        {
            get
            {
                return this.Id.Split(':')[1];
            }
        }
    }
}
