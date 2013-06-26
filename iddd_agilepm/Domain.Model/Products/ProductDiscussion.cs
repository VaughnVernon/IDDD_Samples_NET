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

namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.AgilePM.Domain.Model.Discussions;

    public class ProductDiscussion : ValueObject
    {
        public static ProductDiscussion FromAvailability(
                DiscussionAvailability availability)
        {
            if (availability == DiscussionAvailability.Ready)
                throw new InvalidOperationException("Cannot be created ready.");

            var descriptor = new DiscussionDescriptor(DiscussionDescriptor.UNDEFINED_ID);

            return new ProductDiscussion(descriptor, availability);
        }

        public ProductDiscussion(DiscussionDescriptor descriptor, DiscussionAvailability availability)
        {
            AssertionConcern.AssertArgumentNotNull(descriptor, "The descriptor must be provided.");
            this.Availability = availability;
            this.Descriptor = descriptor;
        }

        public ProductDiscussion(ProductDiscussion productDiscussion)
            : this(productDiscussion.Descriptor, productDiscussion.Availability)
        {
        }

        public DiscussionAvailability Availability { get; private set; }

        public DiscussionDescriptor Descriptor { get; private set; }

        public ProductDiscussion NowReady(DiscussionDescriptor descriptor)
        {
            if (descriptor == null || descriptor.IsUndefined)
                throw new ArgumentException("The discussion descriptor must be defined.");
            if (this.Availability != DiscussionAvailability.Requested)
                throw new InvalidOperationException("The discussion must be requested first.");
            return new ProductDiscussion(descriptor, DiscussionAvailability.Ready);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Availability;
            yield return this.Descriptor;
        }
    }
}
