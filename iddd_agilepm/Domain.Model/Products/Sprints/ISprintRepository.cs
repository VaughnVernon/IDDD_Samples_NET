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
using System.Text;

using SaaSOvation.AgilePM.Domain.Model.Tenants;

namespace SaaSOvation.AgilePM.Domain.Model.Products.Sprints
{
    public interface ISprintRepository
    {
        Sprint Get(TenantId tenantId, SprintId sprintId);

        ICollection<Sprint> GetAllProductSprints(TenantId tenantId, ProductId productId);

        SprintId GetNextIdentity();

        void Remove(Sprint sprint);

        void RemoveAll(IEnumerable<Sprint> sprints);

        void Save(Sprint sprint);

        void SaveAll(IEnumerable<Sprint> sprints);
    }
}
