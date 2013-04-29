namespace SaaSOvation.AgilePM.Domain.Model.Tenants
{
    using SaaSOvation.Common.Domain.Model;

    public class ProductId : Identity
    {
        public ProductId()
            : base()
        {
        }

        public ProductId(string id)
            : base(id)
        {
        }
    }
}
