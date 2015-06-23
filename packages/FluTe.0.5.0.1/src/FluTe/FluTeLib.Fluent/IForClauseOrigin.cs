namespace FluTeLib.Fluent
{
	public interface IForClauseOrigin
	{
		/// <summary>
		/// Stop configuring the current processing steps, and start configuring the template's post-processing steps.
		/// </summary>
		/// <returns></returns>
		IForClause<string> Finally();

		/// <summary>
		/// Stop configuring the current processing steps, and start configuring the processing steps of a named token.
		/// </summary>
		/// <typeparam name="TOut">The initial type you expect the token's value to be.</typeparam>
		/// <param name="identifier">The identifier of the token.</param>
		/// <returns></returns>
		IForClause<TOut> For<TOut>(string identifier);
	}
}