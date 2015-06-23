using System.Collections.Generic;
using System.Diagnostics;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   The class for a finally resolved token.
	/// </summary>
	[DebuggerTypeProxy(typeof (DebugProxy))]
	internal class ResolvedToken : BaseToken, IResolvedToken
	{
		private readonly string value;

		public ResolvedToken(BaseToken original, string value)
			: this(original.Key, original.Inputs, original.Processing, value)
		{
			this.value = value;
		}

		public ResolvedToken(TokenKey key, IEnumerable<InputReference> inputs, ProcessingQueue queue, string value)
			: base(key, inputs, queue)
		{
			this.value = value;
		}

		public string Value
		{
			get
			{
				return value;
			}
		}

		#region Nested type: DebugProxy

		private class DebugProxy
		{
			private readonly ResolvedToken inner;

			public DebugProxy(ResolvedToken inner)
			{
				this.inner = inner;
			}

			public string Label
			{
				get
				{
					return inner.Key.Identifier;
				}
			}

			public TokenType Type
			{
				get
				{
					return inner.Key.Type;
				}
			}

			public string Value
			{
				get
				{
					return inner.Value;
				}
			}
		}

		#endregion
	}
}