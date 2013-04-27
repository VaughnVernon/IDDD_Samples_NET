namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;

    public class GroupMemberService
    {
        public GroupMemberService(
                UserRepository userRepository,
                GroupRepository groupRepository)
        {
            this.GroupRepository = groupRepository;
            this.UserRepository = userRepository;
        }

        private GroupRepository GroupRepository { get; set; }

        private UserRepository UserRepository { get; set; }

        public bool ConfirmUser(Group group, User user)
        {
            bool userConfirmed = true;

            User confirmedUser =
                    this.UserRepository
                        .UserWithUsername(group.TenantId, user.Username);

            if (confirmedUser == null || !confirmedUser.Enabled)
            {
                userConfirmed = false;
            }

            return userConfirmed;
        }

        public bool IsMemberGroup(Group group, GroupMember memberGroup)
        {
            bool isMember = false;

            foreach (GroupMember member in group.GroupMembers)
            {
                if (member.IsGroup())
                {
                    if (memberGroup.Equals(member))
                    {
                        isMember = true;
                    }
                    else
                    {
                        Group nestedGroup =
                            this.GroupRepository.GroupNamed(member.TenantId, member.Name);

                        if (nestedGroup != null)
                        {
                            isMember = this.IsMemberGroup(nestedGroup, memberGroup);
                        }
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
            bool isInNestedGroup = false;

            foreach (GroupMember member in group.GroupMembers)
            {
                if (member.IsGroup())
                {
                    Group nestedGroup =
                            this.GroupRepository
                                .GroupNamed(member.TenantId, member.Name);

                    if (nestedGroup != null)
                    {
                        isInNestedGroup = nestedGroup.IsMember(user, this);
                    }
                }

                if (isInNestedGroup)
                {
                    break;
                }
            }

            return isInNestedGroup;
        }
    }
}
