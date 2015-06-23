using System.Collections.Generic;

namespace FluTeLib.Core.Token
{
	public interface IToken
	{
		IEnumerable<InputReference> Inputs
		{
			get;
		}

		TokenKey Key
		{
			get;
		}
	}
}