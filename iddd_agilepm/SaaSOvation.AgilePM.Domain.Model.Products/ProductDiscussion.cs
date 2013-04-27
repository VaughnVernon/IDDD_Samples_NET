namespace SaaSOvation.AgilePM.Domain.Model.Products
{
    using System;
    using SaaSOvation.AgilePM.Domain.Model.Discussions;

    public class ProductDiscussion
    {
        public static ProductDiscussion FromAvailability(
                DiscussionAvailability availability)
        {
            if (availability == DiscussionAvailability.Ready)
            {
                throw new InvalidOperationException("Cannot be created ready.");
            }

            DiscussionDescriptor descriptor =
                    new DiscussionDescriptor(DiscussionDescriptor.UNDEFINED_ID);

            return new ProductDiscussion(descriptor, availability);
        }

        public ProductDiscussion(
                DiscussionDescriptor descriptor,
                DiscussionAvailability availability)
        {
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
            if (descriptor == null || descriptor.IsUndefined())
            {
                throw new InvalidOperationException("The discussion descriptor must be defined.");
            }
            if (this.Availability != DiscussionAvailability.Requested)
            {
                throw new InvalidOperationException("The discussion must be requested first.");
            }

            return new ProductDiscussion(descriptor, DiscussionAvailability.Ready);
        }
    }
}
