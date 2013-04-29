namespace SaaSOvation.Common.Domain.Model.LongRunningProcess
{
    public class ProcessId : Identity
    {
        public ProcessId(string id)
            : base(id)
        {
        }

        public ProcessId()
            : base()
        {
        }
    }
}
