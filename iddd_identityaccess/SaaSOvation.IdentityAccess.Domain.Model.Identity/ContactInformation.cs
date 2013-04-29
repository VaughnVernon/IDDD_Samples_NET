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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class ContactInformation : ValueObject
    {
        public ContactInformation(
                EmailAddress emailAddress,
                PostalAddress postalAddress,
                Telephone primaryTelephone,
                Telephone secondaryTelephone)
        {
            this.EmailAddress = emailAddress;
            this.PostalAddress = postalAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
        }

        public ContactInformation(ContactInformation contactInformation)
            : this(contactInformation.EmailAddress,
                   contactInformation.PostalAddress,
                   contactInformation.PrimaryTelephone,
                   contactInformation.SecondaryTelephone)
        {
        }

        internal ContactInformation()
        {
        }

        public EmailAddress EmailAddress { get; private set; }

        public PostalAddress PostalAddress { get; private set; }

        public Telephone PrimaryTelephone { get; private set; }

        public Telephone SecondaryTelephone { get; private set; }

        public ContactInformation ChangeEmailAddress(EmailAddress emailAddress)
        {
            return new ContactInformation(
                    emailAddress,
                    this.PostalAddress,
                    this.PrimaryTelephone,
                    this.SecondaryTelephone);
        }

        public ContactInformation ChangePostalAddress(PostalAddress postalAddress)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   postalAddress,
                   this.PrimaryTelephone,
                   this.SecondaryTelephone);
        }

        public ContactInformation ChangePrimaryTelephone(Telephone telephone)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   this.PostalAddress,
                   telephone,
                   this.SecondaryTelephone);
        }

        public ContactInformation ChangeSecondaryTelephone(Telephone telephone)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   this.PostalAddress,
                   this.PrimaryTelephone,
                   telephone);
        }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType()) {
                ContactInformation typedObject = (ContactInformation) anotherObject;
                equalObjects =
                    this.EmailAddress.Equals(typedObject.EmailAddress) &&
                    this.PostalAddress.Equals(typedObject.PostalAddress) &&
                    this.PrimaryTelephone.Equals(typedObject.PrimaryTelephone) &&
                    ((this.SecondaryTelephone == null && typedObject.SecondaryTelephone == null) ||
                     (this.SecondaryTelephone != null && this.SecondaryTelephone.Equals(typedObject.SecondaryTelephone)));
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (73213 * 173)
                + this.EmailAddress.GetHashCode()
                + this.PostalAddress.GetHashCode()
                + this.PrimaryTelephone.GetHashCode()
                + (this.SecondaryTelephone == null ? 0:this.SecondaryTelephone.GetHashCode());

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "ContactInformation [emailAddress=" + EmailAddress
                    + ", postalAddress=" + PostalAddress
                    + ", primaryTelephone=" + PrimaryTelephone
                    + ", secondaryTelephone=" + SecondaryTelephone + "]";
        }
    }

    public class EmailAddress : AssertionConcern
    {
        public EmailAddress(String address)
        {
            this.Address = address;
        }

        public EmailAddress(EmailAddress emailAddress)
            : this(emailAddress.Address)
        {
        }

        internal EmailAddress()
        {
        }

        private string _address;
        public string Address
        {
            get
            {
                return this._address;
            }

            set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The email address is required.");
                AssertionConcern.AssertArgumentLength(value, 1, 100, "Email address must be 100 characters or less.");
                AssertionConcern.AssertArgumentMatches(
                        "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",
                        value,
                        "Email address format is invalid.");

                this._address = value;
            }
        }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType()) {
                EmailAddress typedObject = (EmailAddress) anotherObject;
                equalObjects = this.Address.Equals(typedObject.Address);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (17861 * 179)
                + this.Address.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "EmailAddress [address=" + Address + "]";
        }
    }

    public class PostalAddress : AssertionConcern
    {
        public PostalAddress(
                String streetAddress,
                String city,
                String stateProvince,
                String postalCode,
                String countryCode)
        {
            this.City = city;
            this.CountryCode = countryCode;
            this.PostalCode = postalCode;
            this.StateProvince = stateProvince;
            this.StreetAddress = streetAddress;
        }

        public PostalAddress(PostalAddress postalAddress)
            : this(postalAddress.StreetAddress,
                   postalAddress.City,
                   postalAddress.StateProvince,
                   postalAddress.PostalCode,
                   postalAddress.CountryCode)
        {
        }

        public string City { get; private set; }

        public string CountryCode { get; private set; }

        public string PostalCode { get; private set; }

        public string StateProvince { get; private set; }

        public string StreetAddress { get; private set; }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                PostalAddress typedObject = (PostalAddress) anotherObject;
                equalObjects =
                    this.StreetAddress.Equals(typedObject.StreetAddress) &&
                    this.City.Equals(typedObject.City) &&
                    this.StateProvince.Equals(typedObject.StateProvince) &&
                    this.PostalCode.Equals(typedObject.PostalCode) &&
                    this.CountryCode.Equals(typedObject.CountryCode);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (31589 * 227)
                + this.StreetAddress.GetHashCode()
                + this.City.GetHashCode()
                + this.StateProvince.GetHashCode()
                + this.PostalCode.GetHashCode()
                + this.CountryCode.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "PostalAddress [streetAddress=" + StreetAddress
                    + ", city=" + City + ", stateProvince=" + StateProvince
                    + ", postalCode=" + PostalCode
                    + ", countryCode=" + CountryCode + "]";
        }
    }

    public class Telephone : AssertionConcern
    {
        public Telephone(String number)
        {
            this.Number = number;
        }

        public Telephone(Telephone telephone)
            : this(telephone.Number)
        {
        }

        internal Telephone()
        {
        }

        private string _number;
        public string Number
        {
            get
            {
                return this._number;
            }

            set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "Telephone number is required.");
                AssertionConcern.AssertArgumentLength(value, 5, 20, "Telephone number may not be more than 20 characters.");
                AssertionConcern.AssertArgumentMatches(
                        "((\\(\\d{3}\\))|(\\d{3}-))\\d{3}-\\d{4}",
                        value,
                        "Telephone number or its format is invalid.");

                this._number = value;
            }
        }

        public override bool Equals(Object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                Telephone typedObject = (Telephone)anotherObject;
                equalObjects = this.Number.Equals(typedObject.Number);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (35137 * 239)
                + this.Number.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "Telephone [number=" + Number + "]";
        }
    }
}
