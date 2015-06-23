using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluTeLib.Core.helper;
using FluTeLib.Core.Input;
using FluTeLib.Core.Token;

namespace FluTeLib.Core.Template
{
	[DebuggerTypeProxy(typeof (DebugProxy))]
	public class FluTeCorePrototype : BaseTemplate
	{
		public FluTeCorePrototype(IEnumerable<IToken> fTokens)
			: base(fTokens)
		{
		}

		private FluTeCorePrototype
			(ImmutableMap<TokenKey, IToken> tokenMap, ImmutableList<TokenReference> tokenSeq,
			 ImmutableMap<InputKey, IInput> inputs, ProcessingQueue postProcessingQueue)
			: base(tokenMap, tokenSeq, inputs, postProcessingQueue)
		{
		}

		public FluTeCorePrototype AttachPostProcessingStep(IProcessingStep step)
		{
			return new FluTeCorePrototype(Tokens, TokenSequence, Inputs, PostProcessingQueue.Extend(step));
		}

		public FluTeCorePrototype AttachProcessingStep(string token, IProcessingStep step)
		{
			var key = new TokenKey(token, TokenType.Injection);
			if (!Tokens.ContainsKey(key))
			{
				throw new KeyNotFoundException("Could not find an injection token with the specified name.");
			}
			else
			{
				var tok = (IUnresolvedToken) Tokens[key];
				tok = tok.AttachStep(step);
				return new FluTeCorePrototype(Tokens.Set(key, tok), TokenSequence, Inputs, PostProcessingQueue);
			}
		}

		public FluTeCoreInstance Construct()
		{
			if (Inputs.Values.Any(x => x is IUnboundStaticInput))
			{
				throw new InvalidOperationException("Cannot complete the template because some predefined inputs are still empty.");
			}
			return new FluTeCoreInstance(this);
		}

		public FluTeCorePrototype DefineStaticInput(string name, Func<object> binding)
		{
			var key = new InputKey(name, InputType.Static);
			if (!Inputs.ContainsKey(key))
			{
				throw new KeyNotFoundException("A static input with the specified name could not be found.");
			}
			
			var input = Inputs[key];
			if (input is IBoundInput)
			{
				throw new InvalidOperationException("The specified static input is already bound.");
			}
			var unboundInput = (IUnboundStaticInput) input;
			var boundInput = unboundInput.Define(binding);
			return new FluTeCorePrototype(Tokens, TokenSequence, Inputs.Set(boundInput.Key, boundInput), PostProcessingQueue);
		}

		#region Nested type: DebugProxy

		private class DebugProxy
		{
			private readonly FluTeCorePrototype inner;

			public DebugProxy(FluTeCorePrototype inner)
			{
				this.inner = inner;
			}


			public ImmutableMap<InputKey, IInput> Inputs
			{
				get
				{
					return inner.Inputs;
				}
			}

			public ImmutableList<TokenReference> TokenSequence
			{
				get
				{
					return inner.TokenSequence;
				}
			}

			public ImmutableMap<TokenKey, IToken> Tokens
			{
				get
				{
					return inner.Tokens;
				}
			}
		}

		#endregion
	}
}