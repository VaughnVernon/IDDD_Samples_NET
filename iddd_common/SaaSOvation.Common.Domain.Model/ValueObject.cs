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

using System;
using System.Collections.Generic;
using System.Linq;

namespace SaaSOvation.Common.Domain.Model
{
    public abstract class ValueObject
    {
        /// <summary>
        /// When overriden in a derived class, returns all components of a value objects which constitute its identity.
        /// </summary>
        /// <returns>An ordered list of equality components.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as ValueObject;
            return GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetEqualityComponents());
        }
    }

    public abstract class ComparableValueObject : ValueObject, IComparable
    {
        protected abstract IEnumerable<IComparable> GetComparableComponents();

        protected IComparable AsNonGenericComparable<T>(IComparable<T> comparable)
        {
            return new NonGenericComparable<T>(comparable);
        }

        class NonGenericComparable<T> : IComparable
        {
            public NonGenericComparable(IComparable<T> comparable)
            {
                this.comparable = comparable;
            }

            readonly IComparable<T> comparable;

            public int CompareTo(object obj)
            {
                if (object.ReferenceEquals(this.comparable, obj)) return 0;
                if (object.ReferenceEquals(null, obj))
                    throw new ArgumentNullException();
                return this.comparable.CompareTo((T)obj);
            }
        }

        protected int CompareTo(ComparableValueObject other)
        {
            using (var thisComponents = GetComparableComponents().GetEnumerator())
            using (var otherComponents = other.GetComparableComponents().GetEnumerator())
            {
                while (true)
                {
                    var x = thisComponents.MoveNext();
                    var y = otherComponents.MoveNext();
                    if (x != y)
                        throw new InvalidOperationException();
                    if (x)
                    {
                        var c = thisComponents.Current.CompareTo(otherComponents.Current);
                        if (c != 0)
                            return c;
                    }
                    else
                    {
                        break;
                    }
                }
                return 0;
            }
        }

        public int CompareTo(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return 0;
            if (object.ReferenceEquals(null, obj)) return 1;

            if (GetType() != obj.GetType())
                throw new InvalidOperationException();

            return CompareTo(obj as ComparableValueObject);
        }
    }

    public abstract class ComparableValueObject<T> : ComparableValueObject, IComparable<T> 
        where T : ComparableValueObject<T>
    {
        public int CompareTo(T other)
        {
            return base.CompareTo(other);
        }
    }
}
