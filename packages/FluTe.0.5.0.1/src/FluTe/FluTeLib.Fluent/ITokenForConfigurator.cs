using FluTeLib.Core.Token;

namespace FluTeLib.Fluent
{

	/// <summary>
	/// A strongly-typed interface that lets you configure the processing steps of a template.
	/// </summary>
	/// <typeparam name="TOut">The type of the input.</typeparam>
	public interface IForClause<out TOut> : IForClause
	{
		/// <summary>
		/// Attaches the specified step.
		/// </summary>
		/// <typeparam name="TNew">The output type of the step</typeparam>
		/// <param name="step">The step.</param>
		/// <returns></returns>
		IForClause<TNew> Attach<TNew>(IProcessingStep<TOut, TNew> step);
	}

	/// <summary>
	/// An interface that lets you configure the processing steps of a template.
	/// </summary>
	public interface IForClause : IForClauseOrigin
	{

		/// <summary>
		/// Stops configuring the current processing steps, and returns a copy of the underlying template.
		/// </summary>
		FluTePrototype Next
		{
			get;
		}

		/// <summary>
		/// Attaches the specified step.
		/// </summary>
		/// <typeparam name="TNew">The output type of the step</typeparam>
		/// <param name="step">The step.</param>
		/// <returns></returns>
		IForClause<TNew> Attach<TNew>(IProcessingStep<object, TNew> step);
	}
}