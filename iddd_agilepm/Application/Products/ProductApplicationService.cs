using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model.LongRunningProcess;

using SaaSOvation.AgilePM.Domain.Model.Products;
using SaaSOvation.AgilePM.Domain.Model.Teams;
using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.AgilePM.Domain.Model.Discussions;

namespace SaaSOvation.AgilePM.Application.Products
{
    public class ProductApplicationService
    {
        public ProductApplicationService(IProductRepository productRepository, IProductOwnerRepository productOwnerRepository, ITimeConstrainedProcessTrackerRepository processTrackerRepository)
        {
            this.productRepository = productRepository;
            this.productOwnerRepository = productOwnerRepository;
            this.processTrackerRepository = processTrackerRepository;
        }

        readonly IProductRepository productRepository;
        readonly IProductOwnerRepository productOwnerRepository;
        readonly ITimeConstrainedProcessTrackerRepository processTrackerRepository;

        public void InitiateDiscussion(InitiateDiscussionCommand command)
        {
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var product = this.productRepository.Get(new TenantId(command.TenantId), new ProductId(command.ProductId));
                if (product == null)
                    throw new InvalidOperationException(
                        string.Format("Unknown product of tenant id: {0} and product id: {1}.", command.TenantId, command.ProductId));

                product.InitiateDiscussion(new DiscussionDescriptor(command.DiscussionId));

                this.productRepository.Save(product);

                var processId = ProcessId.ExistingProcessId(product.DiscussionInitiationId);

                var tracker = this.processTrackerRepository.Get(command.TenantId, processId);

                tracker.MarkProcessCompleted();

                this.processTrackerRepository.Save(tracker);

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public string NewProduct(NewProductCommand command)
        {
            return NewProductWith(command.TenantId, command.ProductOwnerId, command.Name, command.Description, DiscussionAvailability.NotRequested);
        }

        public string NewProductWithDiscussion(NewProductCommand command)
        {
            return NewProductWith(command.TenantId, command.ProductOwnerId, command.Name, command.Description, RequestDiscussionIfAvailable());
        }

        public void RequestProductDiscussion(RequestProductDiscussionCommand command)
        {
            var product = this.productRepository.Get(new TenantId(command.TenantId), new ProductId(command.ProductId));
            if (product == null)
                throw new InvalidOperationException(
                    string.Format("Unknown product of tenant id: {0} and product id: {1}.", command.TenantId, command.ProductId));

            RequestProductDiscussionFor(product);
        }

        public void RetryProductDiscussionRequest(RetryProductDiscussionRequestCommand command)
        {
            var processId = ProcessId.ExistingProcessId(command.ProcessId);
            var tenantId = new TenantId(command.TenantId);
            var product = this.productRepository.GetByDiscussionInitiationId(tenantId, processId.Id);
            if (product == null)
                throw new InvalidOperationException(
                    string.Format("Unknown product of tenant id: {0} and discussion initiation id: {1}.", command.TenantId, command.ProcessId));

            RequestProductDiscussionFor(product);
        }

        public void StartDiscussionInitiation(StartDiscussionInitiationCommand command)
        {
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var product = this.productRepository.Get(new TenantId(command.TenantId), new ProductId(command.ProductId));
                if (product == null)
                    throw new InvalidOperationException(
                        string.Format("Unknown product of tenant id: {0} and product id: {1}.", command.TenantId, command.ProductId));

                var timedOutEventName = typeof(ProductDiscussionRequestTimedOut).Name;

                var tracker = new TimeConstrainedProcessTracker(
                    tenantId: command.TenantId,
                    processId: ProcessId.NewProcessId(),
                    description: "Create discussion for product: " + product.Name,
                    originalStartTime: DateTime.UtcNow,
                    allowableDuration: 5L * 60L * 1000L,
                    totalRetriesPermitted: 3,
                    processTimedOutEventType: timedOutEventName);

                this.processTrackerRepository.Save(tracker);

                product.StartDiscussionInitiation(tracker.ProcessId.Id);

                this.productRepository.Save(product);

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        public void TimeOutProductDiscussionRequest(TimeOutProductDiscussionRequestCommand command)
        {
            ApplicationServiceLifeCycle.Begin();
            try
            {
                var processId = ProcessId.ExistingProcessId(command.ProcessId);

                var tenantId = new TenantId(command.TenantId);

                var product = this.productRepository.GetByDiscussionInitiationId(tenantId, processId.Id);

                SendEmailForTimedOutProcess(product);

                product.FailDiscussionInitiation();

                this.productRepository.Save(product);

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        void SendEmailForTimedOutProcess(Product product)
        {
            // TODO: implement
        }

        void RequestProductDiscussionFor(Product product)
        {
            ApplicationServiceLifeCycle.Begin();
            try
            {
                product.RequestDiscussion(RequestDiscussionIfAvailable());

                this.productRepository.Save(product);

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
        }

        DiscussionAvailability RequestDiscussionIfAvailable()
        {
            var availability = DiscussionAvailability.AddOnNotEnabled;
            var enabled = true; // TODO: determine add-on enabled
            if (enabled)
            {
                availability = DiscussionAvailability.Requested;
            }
            return availability;
        }

        string NewProductWith(string tenantId, string productOwnerId, string name, string description, DiscussionAvailability discussionAvailability)
        {
            var tid = new TenantId(tenantId);
            var productId = default(ProductId);
            ApplicationServiceLifeCycle.Begin();
            try
            {
                productId = this.productRepository.GetNextIdentity();

                var productOwner = this.productOwnerRepository.Get(tid, productOwnerId);

                var product = new Product(tid, productId, productOwner.ProductOwnerId, name, description, discussionAvailability);

                this.productRepository.Save(product);

                ApplicationServiceLifeCycle.Success();
            }
            catch (Exception ex)
            {
                ApplicationServiceLifeCycle.Fail(ex);
            }
            // TODO: handle null properly
            return productId.Id;
        }
    }
}
