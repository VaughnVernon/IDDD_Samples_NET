namespace SaaSOvation.AgilePM.Domain.Model.Tenants
{
    using SaaSOvation.Common.Domain.Model;

    public class ReleaseId : Identity
    {
        public ReleaseId()
            : base()
        {
        }

        public ReleaseId(string id)
            : base(id)
        {
        }
    }
}
