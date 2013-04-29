namespace SaaSOvation.AgilePM.Domain.Model.Tenants
{
    using SaaSOvation.Common.Domain.Model;

    public class SprintId : Identity
    {
        public SprintId()
            : base()
        {
        }

        public SprintId(string id)
            : base(id)
        {
        }
    }
}
