namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
	using System;

	/// <summary>
	/// Enumeration of the types of items which may
	/// be placed within a <see cref="Group"/>.
	/// </summary>
	[CLSCompliant(true)]
	public enum GroupMemberType : byte
	{
		/// <summary>
		/// Indicates that the group member is a <see cref="Group"/>.
		/// </summary>
		Group = 0,

		/// <summary>
		/// Indicates that the group member is a <see cref="User"/>.
		/// </summary>
		User = 1
	}
}