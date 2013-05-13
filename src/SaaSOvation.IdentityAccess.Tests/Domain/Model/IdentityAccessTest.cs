namespace SaaSOvation.IdentityAccess.Tests.Domain.Model
{
    using System;

    using NUnit.Framework;

    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    [TestFixture]
    public abstract class IdentityAccessTest
    {
        protected static readonly string FIXTURE_PASSWORD = "SecretPassword!";
        protected static readonly string FIXTURE_TENANT_DESCRIPTION = "This is a test tenant.";
        protected static readonly string FIXTURE_TENANT_NAME = "Test Tenant";
        protected static readonly string FIXTURE_USER_EMAIL_ADDRESS = "jdoe@saasovation.com";
        protected static readonly string FIXTURE_USER_EMAIL_ADDRESS2 = "zdoe@saasovation.com";
        protected static readonly string FIXTURE_USERNAME = "jdoe";
        protected static readonly string FIXTURE_USERNAME2 = "zdoe";
        protected static readonly long TWENTY_FOUR_HOURS = (1000L * 60L * 60L * 24L);

        private Tenant tenantAggregate;

        public Tenant TenantAggregate
        {
            get
            {
                if (this.tenantAggregate == null)
                {
                    this.tenantAggregate = new Tenant(
                        FIXTURE_TENANT_NAME,
                        FIXTURE_TENANT_DESCRIPTION,
                        true);

                    TestDomainRegistry.TenantRepository.Add(this.tenantAggregate);
                }

                return this.tenantAggregate;
            }
        }

        protected DateTime Today
        {
            get
            {
                return DateTime.Now;
            }
        }

        protected DateTime Tomorrow
        {
            get
            {
                return this.Today.AddDays(1);
            }
        }

        protected DateTime DayAfterTomorrow
        {
            get
            {
                return this.Tomorrow.AddDays(1);
            }
        }

        protected ContactInformation ContactInformation
        {
            get
            {
                return new ContactInformation(
                    new EmailAddress(FIXTURE_USER_EMAIL_ADDRESS),
                    new PostalAddress("123 Pearl Street", "Boulder", "CO", "80301", "US"),
                    new Telephone("303-555-1210"),
                    new Telephone("303-555-1212"));
            }
        }

        [TearDown]
        protected virtual void TearDown()
        {
            this.tenantAggregate = null;
            TestDomainRegistry.Reset();
        }

        protected RegistrationInvitation RegistrationInvitationEntity(Tenant tenant)
        {
            RegistrationInvitation registrationInvitation =
                tenant
                    .OfferRegistrationInvitation("Today-and-Tomorrow: " + DateTime.Now.Ticks)
                    .WillStartOn(this.Today)
                    .LastingUntil(this.Tomorrow);

            return registrationInvitation;
        }

        protected Person PersonEntity(Tenant tenant)
        {
            Person person =
                new Person(
                    tenant.TenantId,
                    new FullName("John", "Doe"),
                    this.ContactInformation);

            return person;
        }
    }
}
