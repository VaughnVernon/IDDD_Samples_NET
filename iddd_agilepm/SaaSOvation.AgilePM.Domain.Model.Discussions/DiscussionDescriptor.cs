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

namespace SaaSOvation.AgilePM.Domain.Model.Discussions
{
    public class DiscussionDescriptor
    {
        public const string UNDEFINED_ID = "UNDEFINED";

        public DiscussionDescriptor(string id)
        {
            this.Id = id;
        }

        public DiscussionDescriptor(DiscussionDescriptor discussionDescriptor)
            : this(discussionDescriptor.Id)
        {
        }

        public string Id { get; private set; }

        public bool IsUndefined()
        {
            return this.Id.Equals(UNDEFINED_ID);
        }

        public override bool Equals(object anotherObject)
        {
            bool equalObjects = false;

            if (anotherObject != null && this.GetType() == anotherObject.GetType())
            {
                DiscussionDescriptor typedObject = (DiscussionDescriptor)anotherObject;
                equalObjects = this.Id.Equals(typedObject.Id);
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            int hashCodeValue =
                + (72881 * 101)
                + this.Id.GetHashCode();

            return hashCodeValue;
        }

        public override string ToString()
        {
            return "DiscussionDescriptor [id=" + Id + "]";
        }
    }
}
