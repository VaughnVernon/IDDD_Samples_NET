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

	/// <summary>
	/// A domain service providing methods to determine
	/// whether a <see cref="User"/> or <see cref="Group"/>
	/// is a member of a group or of a nested group.
	/// </summary>
	[CLSCompliant(true)]
	public class GroupMemberService
	{
		#region [ ReadOnly Fields and Constructor ]

		private readonly IGroupRepository groupRepository;
		private readonly IUserRepository userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupMemberService"/> class.
		/// </summary>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="groupRepository">
		/// An instance of <see cref="IGroupRepository"/> to use internally.
		/// </param>
		public GroupMemberService(
			IUserRepository userRepository,
			IGroupRepository groupRepository)
		{
			this.groupRepository = groupRepository;
			this.userRepository = userRepository;
		}

		#endregion

		/// <summary>
		/// Determines whether a <see cref="User"/>'s declared
		/// membership in a <see cref="Group"/> is valid.
		/// </summary>
		/// <param name="group">
		/// An instance of <see cref="Group"/> which may have
		/// the <paramref name="user"/> as a member.
		/// </param>
		/// <param name="user">
		/// An instance of <see cref="User"/> which may be
		/// a member of the <paramref name="group"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <paramref name="user"/>'s
		/// <see cref="User.TenantId"/> matches that of
		/// the <paramref name="group"/> and the user's
		/// <see cref="User.IsEnabled"/> property is true;
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool ConfirmUser(Group group, User user)
		{
			User confirmedUser = this.userRepository.UserWithUsername(group.TenantId, user.Username);

			return ((confirmedUser == null) || (!confirmedUser.IsEnabled));
		}

		public bool IsMemberGroup(Group group, GroupMember memberGroup)
		{
			bool isMember = false;
			foreach (GroupMember member in group.GroupMembers.Where(x => x.IsGroup))
			{
				if (memberGroup.Equals(member))
				{
					isMember = true;
				}
				else
				{
					Group nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
					if (nestedGroup != null)
					{
						isMember = this.IsMemberGroup(nestedGroup, memberGroup);
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
			foreach (GroupMember member in group.GroupMembers.Where(x => x.IsGroup))
			{
				Group nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
				if (nestedGroup != null)
				{
					bool isInNestedGroup = nestedGroup.IsMember(user, this);
					if (isInNestedGroup)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}