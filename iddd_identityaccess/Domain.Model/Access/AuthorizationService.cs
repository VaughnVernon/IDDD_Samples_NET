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

namespace SaaSOvation.IdentityAccess.Domain.Model.Access
{
    using SaaSOvation.Common.Domain.Model;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;

    public class AuthorizationService
    {
        public AuthorizationService(
                IUserRepository userRepository,
                IGroupRepository groupRepository,
                IRoleRepository roleRepository)
        {
            this.groupRepository = groupRepository;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        readonly IGroupRepository groupRepository;
        readonly IRoleRepository roleRepository;
        readonly IUserRepository userRepository;

        public bool IsUserInRole(TenantId tenantId, string username, string roleName)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(username, "Username must not be provided.");
            AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

            var user = this.userRepository.UserWithUsername(tenantId, username);
            return user == null ? false : this.IsUserInRole(user, roleName);
        }

        public bool IsUserInRole(User user, string roleName)
        {
            AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

            var authorized = false;
            if (user.IsEnabled)
            {
                var role = this.roleRepository.RoleNamed(user.TenantId, roleName);
                if (role != null)
                {
                    authorized = role.IsInRole(user, new GroupMemberService(this.userRepository, this.groupRepository));
                }
            }
            return authorized;
        }
    }
}
