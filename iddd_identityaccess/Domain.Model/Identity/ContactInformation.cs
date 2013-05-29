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
    using SaaSOvation.Common.Domain;

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

        public override string ToString()
        {
            return "ContactInformation [emailAddress=" + EmailAddress
                    + ", postalAddress=" + PostalAddress
                    + ", primaryTelephone=" + PrimaryTelephone
                    + ", secondaryTelephone=" + SecondaryTelephone + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EmailAddress;
            yield return this.PostalAddress;
            yield return this.PrimaryTelephone;
            yield return this.SecondaryTelephone;
        }
    }

    public class EmailAddress : ValueObject
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

        string _address;
        
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

        public override string ToString()
        {
            return "EmailAddress [address=" + Address + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return _address.ToUpper();
        }
    }

    public class PostalAddress : ValueObject
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

        public override string ToString()
        {
            return "PostalAddress [streetAddress=" + StreetAddress
                    + ", city=" + City + ", stateProvince=" + StateProvince
                    + ", postalCode=" + PostalCode
                    + ", countryCode=" + CountryCode + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.StreetAddress;
            yield return this.City;
            yield return this.StateProvince;
            yield return this.PostalCode;
            yield return this.CountryCode;
        }
    }

    public class Telephone : ValueObject
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

        string _number;
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

        public override string ToString()
        {
            return "Telephone [number=" + Number + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Number;
        }
    }
}
