namespace SaaSOvation.Common.Domain.Model
{
    using System;

    public interface DomainEventSubscriber<T> where T : DomainEvent
    {
        void HandleEvent(T domainEvent);

        Type SubscribedToEventType();
    }
}
