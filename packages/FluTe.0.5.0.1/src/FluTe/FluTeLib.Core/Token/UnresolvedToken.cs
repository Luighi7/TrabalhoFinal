using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluTeLib.Core.Template;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   A class for an unresolved injection token. Literal tokens cannot be unresolved.
	/// </summary>
	[DebuggerTypeProxy(typeof (DebugProxy))]
	internal class UnresolvedToken : BaseToken, IUnresolvedToken
	{
		public UnresolvedToken(TokenKey key, IEnumerable<InputReference> inputs, ProcessingQueue processing)
			: base(key, inputs, processing)
		{
		}

		public string Identifier
		{
			get
			{
				return Key.Identifier;
			}
		}

		public IUnresolvedToken AttachStep(IProcessingStep step)
		{
			return new UnresolvedToken(Key, Inputs, Processing.Extend(step));
		}

		public IResolvedToken Resolve(FluTeCoreInstance template)
		{
			var inputObject = new MultiInput(template, Inputs);
			var value = Inputs.Count() == 1 ? Processing.Invoke(inputObject[0]) : Processing.Invoke(inputObject);
			return new ResolvedToken(this, value.ToString());
		}

		#region Nested type: DebugProxy

		private class DebugProxy
		{
			private readonly UnresolvedToken inner;

			public DebugProxy(UnresolvedToken inner)
			{
				this.inner = inner;
			}

			public IEnumerable<InputReference> Inputs
			{
				get
				{
					return inner.Inputs;
				}
			}

			public string Label
			{
				get
				{
					return inner.Key.Identifier;
				}
			}

			public ProcessingQueue Processing
			{
				get
				{
					return inner.Processing;
				}
			}

			public TokenType Type
			{
				get
				{
					return inner.Key.Type;
				}
			}
		}

		#endregion
	}
}