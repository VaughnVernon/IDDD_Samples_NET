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

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    using System;

    using SaaSOvation.Common.Domain.Model;

    public class MemberChangeTracker : ValueObject
    {
        internal MemberChangeTracker(DateTime enablingOn, DateTime nameChangedOn, DateTime emailAddressChangedOn)
        {
            this.emailAddressChangedOnDate = emailAddressChangedOn;
            this.enablingOnDate = enablingOn;
            this.nameChangedOnDate = nameChangedOn;
        }

        readonly DateTime enablingOnDate;
        readonly DateTime nameChangedOnDate;
        readonly DateTime emailAddressChangedOnDate;

        public bool CanChangeEmailAddress(DateTime asOfDateTime)
        {
            return this.emailAddressChangedOnDate < asOfDateTime;
        }

        public bool CanChangeName(DateTime asOfDateTime)
        {
            return this.nameChangedOnDate < asOfDateTime;
        }

        public bool CanToggleEnabling(DateTime asOfDateTime)
        {
            return this.enablingOnDate < asOfDateTime;
        }

        public MemberChangeTracker EmailAddressChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.enablingOnDate, this.nameChangedOnDate, asOfDateTime);
        }

        public MemberChangeTracker EnablingOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(asOfDateTime, this.nameChangedOnDate, this.emailAddressChangedOnDate);
        }

        public MemberChangeTracker NameChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.enablingOnDate, asOfDateTime, this.emailAddressChangedOnDate);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.enablingOnDate;
            yield return this.nameChangedOnDate;
            yield return this.emailAddressChangedOnDate;
        }
    }
}
