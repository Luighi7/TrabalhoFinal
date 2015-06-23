using System.Collections.Generic;
using FluTeLib.Core.helper;
using FluTeLib.Core.Input;
using FluTeLib.Core.Token;

namespace FluTeLib.Core.Template
{
	public abstract class BaseTemplate
	{
		private readonly ImmutableMap<InputKey, IInput> inputs;

		private readonly ProcessingQueue postProcessingQueue;

		private readonly ImmutableMap<TokenKey, IToken> tokenMap;

		private readonly ImmutableList<TokenReference> tokenSeq;

		protected BaseTemplate
			(ImmutableMap<TokenKey, IToken> tokenMap, ImmutableList<TokenReference> tokenSeq,
			 ImmutableMap<InputKey, IInput> inputs, ProcessingQueue postProcessingQueue)

		{
			this.tokenSeq = tokenSeq;
			this.inputs = inputs;
			this.tokenMap = tokenMap;
			this.postProcessingQueue = postProcessingQueue;
		}

		protected BaseTemplate(IEnumerable<IToken> fTokens)
			: this(
				new ImmutableMap<TokenKey, IToken>(), ImmutableList.Empty<TokenReference>(), new ImmutableMap<InputKey, IInput>(),
				ProcessingQueue.Empty())
		{
			foreach (var tok in fTokens)
			{
				if (tokenMap.ContainsKey(tok.Key))
				{
					tokenSeq = tokenSeq.ConsLast(new TokenReference(tok.Key));
				}
				else
				{
					foreach (var input in tok.Inputs)
					{
						if (!inputs.ContainsKey(input.Key))
						{
							var newInput = input.Key.Type == InputType.Instance
							               	? (IInput) new UnboundInput(input.Key) : new UnboundStaticInput(input.Key);
							inputs = inputs.Add(input.Key, newInput);
						}
					}
					tokenMap = tokenMap.Add(tok.Key, tok);
					tokenSeq = tokenSeq.ConsLast(new TokenReference(tok.Key));
				}
			}
		}

		/// <summary>
		///   A sequence of all inputs known by this instance.
		/// </summary>
		internal ImmutableMap<InputKey, IInput> Inputs
		{
			get
			{
				return inputs;
			}
		}

		internal ProcessingQueue PostProcessingQueue
		{
			get
			{
				return postProcessingQueue;
			}
		}

		/// <summary>
		///   The queue of template tokens composing this instance.
		/// </summary>
		internal ImmutableList<TokenReference> TokenSequence
		{
			get
			{
				return tokenSeq;
			}
		}

		/// <summary>
		///   An unordered queue of all tokens known by this instance, of any type.
		/// </summary>
		internal ImmutableMap<TokenKey, IToken> Tokens
		{
			get
			{
				return tokenMap;
			}
		}
	}
}