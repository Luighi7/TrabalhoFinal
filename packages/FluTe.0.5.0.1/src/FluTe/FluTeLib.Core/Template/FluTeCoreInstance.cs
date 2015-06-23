using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluTeLib.Core.helper;
using FluTeLib.Core.Input;
using FluTeLib.Core.Token;

namespace FluTeLib.Core.Template
{
	/// <summary>
	///   The core class for a finalized template (one the processing steps/predefined inputs of which cannot be editted)
	/// </summary>
	[DebuggerTypeProxy(typeof (DebugProxy))]
	public class FluTeCoreInstance : BaseTemplate
	{
		public FluTeCoreInstance(FluTeCorePrototype template)
			: base(template.Tokens, template.TokenSequence, template.Inputs, template.PostProcessingQueue)
		{
		}

		private FluTeCoreInstance
			(ImmutableMap<TokenKey, IToken> tokenMap, ImmutableList<TokenReference> tokenSeq,
			 ImmutableMap<InputKey, IInput> inputs, ProcessingQueue postProcessingQueue)
			: base(tokenMap, tokenSeq, inputs, postProcessingQueue)
		{
		}

		public FluTeCoreInstance BindInput(string name, object value)
		{
			var key = new InputKey(name, InputType.Instance);
			if (!Inputs.ContainsKey(key))
			{
				throw new KeyNotFoundException("Tried to bind an input name that does not exist.");
			}
			var input = Inputs[key];

			if (input is IBoundInput)
			{
				throw new InvalidOperationException("Cannot bind a value to an input that has already been bound.");
			}
			var inputAs = input as IUnboundInput;

			return new FluTeCoreInstance(Tokens, TokenSequence, Inputs.Set(key, inputAs.Bind(value)), PostProcessingQueue);
		}

		public string Resolve()
		{
			foreach (var input in Inputs.Values)
			{
				if (!(input is IBoundInput))
				{
					throw new InvalidOperationException("Cannot resolve the template while some inputs remain unbound.");
				}
			}
			var resolvedTokens = from tokRef in TokenSequence.AsEnumerable
			                     let tok = Tokens[tokRef.Key]
			                     let resolvedTok = (tok as IResolvedToken) ?? ((IUnresolvedToken) tok).Resolve(this)
			                     select resolvedTok.Value;

			return (string)PostProcessingQueue.Invoke(string.Join("", resolvedTokens));
		}

		#region Nested type: DebugProxy

		private class DebugProxy
		{
			private readonly FluTeCoreInstance inner;

			public DebugProxy(FluTeCoreInstance inner)
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