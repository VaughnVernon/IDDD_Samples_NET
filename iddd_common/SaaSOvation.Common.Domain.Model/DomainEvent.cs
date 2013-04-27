namespace SaaSOvation.Common.Domain.Model
{
    using System;

    public interface DomainEvent
    {
        int EventVersion { get; set;  }

        DateTime OccurredOn { get; set; }
    }
}
