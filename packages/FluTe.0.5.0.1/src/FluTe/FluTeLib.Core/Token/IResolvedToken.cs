namespace FluTeLib.Core.Token
{
	/// <summary>
	///   An interface for all tokens that have been resolved (e.g. transformed into their final string representation).
	/// </summary>
	public interface IResolvedToken : IToken
	{
		/// <summary>
		///   The final string representation of a token of any sort.
		/// </summary>
		string Value
		{
			get;
		}
	}
}