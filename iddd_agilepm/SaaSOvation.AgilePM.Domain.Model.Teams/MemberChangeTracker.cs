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

    public class MemberChangeTracker
    {
        internal MemberChangeTracker(DateTime enablingOn, DateTime nameChangedOn, DateTime emailAddressChangedOn)
        {
            this.EmailAddressChangedOnDate = emailAddressChangedOn;
            this.EnablingOnDate = enablingOn;
            this.NameChangedOnDate = nameChangedOn;
        }

        private DateTime EnablingOnDate { get; set; }

        private DateTime NameChangedOnDate { get; set; }

        private DateTime EmailAddressChangedOnDate { get; set; }

        public bool CanChangeEmailAddress(DateTime asOfDateTime)
        {
            return this.EmailAddressChangedOnDate < asOfDateTime;
        }

        public bool CanChangeName(DateTime asOfDateTime)
        {
            return this.NameChangedOnDate < asOfDateTime;
        }

        public bool CanToggleEnabling(DateTime asOfDateTime)
        {
            return this.EnablingOnDate < asOfDateTime;
        }

        public MemberChangeTracker EmailAddressChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.EnablingOnDate, this.NameChangedOnDate, asOfDateTime);
        }

        public MemberChangeTracker EnablingOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(asOfDateTime, this.NameChangedOnDate, this.EmailAddressChangedOnDate);
        }

        public MemberChangeTracker NameChangedOn(DateTime asOfDateTime)
        {
            return new MemberChangeTracker(this.EnablingOnDate, asOfDateTime, this.EmailAddressChangedOnDate);
        }
    }
}
