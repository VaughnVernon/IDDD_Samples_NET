namespace SaaSOvation.Common.Domain.Model
{
    using System;
    using System.Text.RegularExpressions;

    public class AssertionConcern
    {
        protected AssertionConcern()
        {
        }

        protected void AssertArgumentEquals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentLength(string stringValue, int aMaximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentLength(string stringValue, int aMinimum, int aMaximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < aMinimum || length > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentMatches(string pattern, string stringValue, string message)
        {
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(stringValue))
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentNotEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentRange(double aValue, double aMinimum, double aMaximum, string message)
        {
            if (aValue < aMinimum || aValue > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentRange(float aValue, float aMinimum, float aMaximum, string message)
        {
            if (aValue < aMinimum || aValue > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentRange(int aValue, int aMinimum, int aMaximum, string message)
        {
            if (aValue < aMinimum || aValue > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentRange(long aValue, long aMinimum, long aMaximum, string message)
        {
            if (aValue < aMinimum || aValue > aMaximum)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertArgumentTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertStateFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void AssertStateTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
