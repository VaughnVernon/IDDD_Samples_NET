using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class TaskId : ValueObject
    {
        public TaskId()
            : this(Guid.NewGuid().ToString().ToUpper().Substring(0, 8))
        {
        }

        public TaskId(string id)
        {
            AssertionConcern.AssertArgumentNotEmpty(id, "The id must be provided.");
            AssertionConcern.AssertArgumentLength(id, 8, "The id must be 8 characters or less.");
            this.Id = id;
        }

        public string Id { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
        }
    }
}
