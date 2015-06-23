using System.Diagnostics;
using FluTeLib.Core.Token;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   Denotes a member reference (e.g. method or property invocation) for an input reference,
	/// </summary>
	[DebuggerDisplay("{MemberType}: {Name}")]
	public struct InputMemberReference
	{
		private readonly MemberType memberType;

		private readonly string name;

		/// <summary>
		///   Initializes a new instance of the <see cref = "InputMemberReference" /> struct.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <param name = "memberType">Type of the member.</param>
		public InputMemberReference(string name, MemberType memberType)
			: this()
		{
			this.name = name;
			this.memberType = memberType;
		}

		public MemberType MemberType
		{
			get
			{
				return memberType;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}
	}
}