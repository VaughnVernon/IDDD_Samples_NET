namespace SaaSOvation.IdentityAccess.Tests.Domain.Model.Identity
{
    using System;

    using NUnit.Framework;

    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    [TestFixture]
    public class TenantTest : IdentityAccessTest
    {
        [Test]
        public void CreateOpenEndedInvitation()
        {
            var tenant = this.TenantAggregate;

            tenant
                .OfferRegistrationInvitation("Open-Ended")
                .OpenEnded();

            Assert.IsNotNull(tenant.RedefineRegistrationInvitationAs("Open-Ended"));
        }

        [Test]
        public void OpenEndedInvitationAvailable()
        {
            var tenant = this.TenantAggregate;

            tenant
                .OfferRegistrationInvitation("Open-Ended")
                .OpenEnded();

            Assert.IsTrue(tenant.IsRegistrationAvailableThrough("Open-Ended"));
        }

        [Test]
        public void ClosedEndedInvitationAvailable()
        {
            var tenant = this.TenantAggregate;

            tenant.OfferRegistrationInvitation("Today-and-Tomorrow")
                  .WillStartOn(this.Today)
                  .LastingUntil(this.Tomorrow);

            Assert.IsTrue(tenant.IsRegistrationAvailableThrough("Today-and-Tomorrow"));
        }

        [Test]
        public void ClosedEndedInvitationNotAvailable()
        {
            var tenant = this.TenantAggregate;

            tenant
                .OfferRegistrationInvitation("Tomorrow-and-Day-After-Tomorrow")
                .WillStartOn(this.Tomorrow)
                .LastingUntil(this.DayAfterTomorrow);

            Assert.IsFalse(tenant.IsRegistrationAvailableThrough("Tomorrow-and-Day-After-Tomorrow"));
        }

        [Test]
        public void AvailableInvitationDescriptor()
        {

            var tenant = this.TenantAggregate;

            tenant
                .OfferRegistrationInvitation("Open-Ended")
                .OpenEnded();

            tenant
                .OfferRegistrationInvitation("Today-and-Tomorrow")
                .WillStartOn(this.Today)
                .LastingUntil(this.Tomorrow);

            Assert.AreEqual(2, tenant.AllAvailableRegistrationInvitations().Count);
        }

        [Test]
        public void UnavailableInvitationDescriptor()
        {
            var tenant = this.TenantAggregate;

            tenant
                .OfferRegistrationInvitation("Tomorrow-and-Day-After-Tomorrow")
                .WillStartOn(this.Tomorrow)
                .LastingUntil(this.DayAfterTomorrow);

            Assert.AreEqual(1, tenant.AllUnavailableRegistrationInvitations().Count);
        }

        [Test]
        public void RegisterUser()
        {
            var tenant = this.TenantAggregate;

            RegistrationInvitation registrationInvitation =
                this.RegistrationInvitationEntity(tenant);

            User user =
                tenant.RegisterUser(
                        registrationInvitation.InvitationId,
                        FIXTURE_USERNAME,
                        FIXTURE_PASSWORD,
                        new Enablement(true, null, null),
                        this.PersonEntity(tenant));

            Assert.IsNotNull(user);

            TestDomainRegistry.UserRepository.Add(user);

            Assert.IsNotNull(user.Enablement);
            Assert.IsNotNull(user.Person);
            Assert.IsNotNull(user.UserDescriptor);
        }
    }
}