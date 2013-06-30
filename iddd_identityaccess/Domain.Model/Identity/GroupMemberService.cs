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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using System.Linq;

    public class GroupMemberService
    {
        public GroupMemberService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        readonly IGroupRepository groupRepository;
        readonly IUserRepository userRepository;

        public bool ConfirmUser(Group group, User user)
        {
            var confirmedUser = this.userRepository.UserWithUsername(group.TenantId, user.Username);
            var userConfirmed = confirmedUser == null || !confirmedUser.IsEnabled;
            return userConfirmed;
        }

        public bool IsMemberGroup(Group group, GroupMember memberGroup)
        {
            var isMember = false;

            foreach (var member in group.GroupMembers.Where(x => x.IsGroup))
            {
                if (memberGroup.Equals(member))
                {
                    isMember = true;
                }
                else
                {
                    var nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
                    if (nestedGroup != null)
                    {
                        isMember = IsMemberGroup(nestedGroup, memberGroup);
                    }
                }

                if (isMember)
                {
                    break;
                }
            }

            return isMember;
        }

        public bool IsUserInNestedGroup(Group group, User user)
        {
            foreach (var member in group.GroupMembers.Where(x => x.IsGroup))
            {
                var nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
                if (nestedGroup != null)
                {
                    var isInNestedGroup = nestedGroup.IsMember(user, this);
                    if (isInNestedGroup)
                        return true;
                }
            }
            return false;
        }
    }
}
