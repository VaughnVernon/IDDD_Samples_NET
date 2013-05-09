namespace SaaSOvation.IdentityAccess.Tests.Domain.Model.Identity
{
    using System;

    using NUnit.Framework;

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

            tenant.OfferRegistrationInvitation("Today-and-Tommorow")
                  .WillStartOn(this.Today)
                  .LastingUntil(this.Tomorrow);

            Assert.IsTrue(tenant.IsRegistrationAvailableThrough("Today-and-Tommorow"));
        }
    }
}