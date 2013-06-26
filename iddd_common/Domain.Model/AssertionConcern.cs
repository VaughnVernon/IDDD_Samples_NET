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

namespace SaaSOvation.Common.Domain.Model
{
    using System;
    using System.Text.RegularExpressions;

    public class AssertionConcern
    {
        public static void AssertArgumentEquals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentMatches(string pattern, string stringValue, string message)
        {
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(stringValue))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentNotEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertStateFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertStateTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected AssertionConcern()
        {
        }

        protected void SelfAssertArgumentEquals(object object1, object object2, string message)
        {
            AssertionConcern.AssertArgumentEquals(object1, object2, message);
        }

        protected void SelfAssertArgumentFalse(bool boolValue, string message)
        {
            AssertionConcern.AssertArgumentFalse(boolValue, message);
        }

        protected void SelfAssertArgumentLength(string stringValue, int maximum, string message)
        {
            AssertionConcern.AssertArgumentLength(stringValue, maximum, message);
        }

        protected void SelfAssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            AssertionConcern.AssertArgumentLength(stringValue, minimum, maximum, message);
        }

        protected void SelfAssertArgumentMatches(string pattern, string stringValue, string message)
        {
            AssertionConcern.AssertArgumentMatches(pattern, stringValue, message);
        }

        protected void SelfAssertArgumentNotEmpty(string stringValue, string message)
        {
            AssertionConcern.AssertArgumentNotEmpty(stringValue, message);
        }

        protected void SelfAssertArgumentNotEquals(object object1, object object2, string message)
        {
            AssertionConcern.AssertArgumentNotEquals(object1, object2, message);
        }

        protected void SelfAssertArgumentNotNull(object object1, string message)
        {
            AssertionConcern.AssertArgumentNotNull(object1, message);
        }

        protected void SelfAssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            AssertionConcern.AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            AssertionConcern.AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            AssertionConcern.AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            AssertionConcern.AssertArgumentRange(value, minimum, maximum, message);
        }

        protected void SelfAssertArgumentTrue(bool boolValue, string message)
        {
            AssertionConcern.AssertArgumentTrue(boolValue, message);
        }

        protected void SelfAssertStateFalse(bool boolValue, string message)
        {
            AssertionConcern.AssertStateFalse(boolValue, message);
        }

        protected void SelfAssertStateTrue(bool boolValue, string message)
        {
            AssertionConcern.AssertStateTrue(boolValue, message);
        }
    }
}
