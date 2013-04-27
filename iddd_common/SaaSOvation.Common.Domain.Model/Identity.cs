using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSOvation.Common.Domain.Model
{
    public class Identity<T>
    {
        public Identity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Identity(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}
