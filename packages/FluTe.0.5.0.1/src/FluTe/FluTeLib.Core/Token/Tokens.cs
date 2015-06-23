using System.Collections.Generic;
using System.Linq;

namespace FluTeLib.Core.Token
{
	public static class Tokens
	{
		public static IToken DefineInjection(string name, IEnumerable<InputReference> inputs)
		{
			return new UnresolvedToken(new TokenKey(name, TokenType.Injection), inputs, ProcessingQueue.Empty());
		}

		public static IToken DefineLiteral(string name)
		{
			return new ResolvedToken
				(new TokenKey(name, TokenType.Literal), Enumerable.Empty<InputReference>(), ProcessingQueue.Empty(), name);
		}
	}
}