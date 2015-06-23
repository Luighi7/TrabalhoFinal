using System.Diagnostics;
using FluTeLib.Core.Token;

namespace FluTeLib.Core.Template
{
	/// <summary>
	///   Denotes a reference to a token using its key.
	/// </summary>
	[DebuggerDisplay("Reference: {Key,nq}")]
	public struct TokenReference
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TokenKey key;

		/// <summary>
		///   Initializes a new instance of the <see cref = "TokenReference" /> struct.
		/// </summary>
		/// <param name = "key">The key.</param>
		public TokenReference(TokenKey key)
		{
			this.key = key;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TokenKey Key
		{
			get
			{
				return key;
			}
		}
	}
}