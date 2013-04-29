// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.Common.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class DomainEventPublisher
    {
        [ThreadStatic]
        private static DomainEventPublisher _instance;
        public static DomainEventPublisher Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DomainEventPublisher();
                }

                return _instance;
            }
        }

        private DomainEventPublisher()
        {
            this.Publishing = false;
        }

        private bool Publishing { get; set; }

        private List<DomainEventSubscriber<DomainEvent>> _subscribers;
        private List<DomainEventSubscriber<DomainEvent>> Subscribers
        {
            get
            {
                if (this._subscribers == null)
                {
                    this._subscribers = new List<DomainEventSubscriber<DomainEvent>>();
                }

                return this._subscribers;
            }
            set
            {
                this._subscribers = value;
            }
        }

        public void Publish<T>(T domainEvent) where T : DomainEvent
        {
            if (!this.Publishing && this.HasSubscribers())
            {
                try
                {
                    this.Publishing = true;

                    Type eventType = domainEvent.GetType();

                    foreach (DomainEventSubscriber<T> subscriber in this.Subscribers)
                    {
                        Type subscribedToType = subscriber.SubscribedToEventType();

                        if (eventType == subscribedToType || subscribedToType == typeof(DomainEvent))
                        {
                            subscriber.HandleEvent(domainEvent);
                        }
                    }
                }
                finally
                {
                    this.Publishing = false;
                }
            }
        }

        public void PublishAll(ICollection<DomainEvent> domainEvents)
        {
            foreach (DomainEvent domainEvent in domainEvents)
            {
                this.Publish(domainEvent);
            }
        }

        public void Reset()
        {
            if (!this.Publishing)
            {
                this.Subscribers = null;
            }
        }

        public void Subscribe(DomainEventSubscriber<DomainEvent> subscriber)
        {
            if (!this.Publishing)
            {
                this.Subscribers.Add(subscriber);
            }
        }

        private bool HasSubscribers()
        {
            return this._subscribers != null && this.Subscribers.Count != 0;
        }
    }
}
