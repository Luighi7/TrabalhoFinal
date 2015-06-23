using System.Collections.Generic;
using System.Diagnostics;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   The base class for a template token (injection or literal).
	/// </summary>
	internal abstract class BaseToken : IToken
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly IEnumerable<InputReference> inputs;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TokenKey key;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ProcessingQueue processingQueue;

		/// <summary>
		///   Constructs a new token with the specified parameters.
		/// </summary>
		/// <param name = "key"></param>
		/// <param name = "inputs"></param>
		/// <param name = "pQueue"></param>
		protected BaseToken(TokenKey key, IEnumerable<InputReference> inputs, ProcessingQueue pQueue)
		{
			this.key = key;
			this.inputs = inputs;
			processingQueue = pQueue;
		}

		/// <summary>
		///   Gets a sequence of the inputs referenced by this token.
		/// </summary>
		public IEnumerable<InputReference> Inputs
		{
			get
			{
				return inputs;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TokenKey Key
		{
			get
			{
				return key;
			}
		}

		public ProcessingQueue Processing
		{
			get
			{
				return processingQueue;
			}
		}

		public TokenType Type
		{
			get
			{
				return key.Type;
			}
		}
	}
}