using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Events;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.IdentityAccess.Application
{
    public class IdentityAccessEventProcessor
    {
        public IdentityAccessEventProcessor(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        readonly IEventStore eventStore;

        public void Listen()
        {
            DomainEventPublisher.Instance.Subscribe(domainEvent => this.eventStore.Append(domainEvent));
        }
    }
}
