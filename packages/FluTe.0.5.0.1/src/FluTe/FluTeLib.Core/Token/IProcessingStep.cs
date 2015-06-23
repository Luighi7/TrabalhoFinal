namespace FluTeLib.Core.Token
{
	/// <summary>
	///   An input processing step; an encapsulated, typically short operation such as a delegate used to process the inputs of injection tokens before they are injected into a template.
	/// </summary>
	public interface IProcessingStep
	{
		/// <summary>
		///   Gets a description of the processing step.
		/// </summary>
		string Description
		{
			get;
		}

		/// <summary>
		///   Gets the name of the processing step.
		/// </summary>
		string Name
		{
			get;
		}

		/// <summary>
		///   Processes the specified value.
		/// </summary>
		/// <param name = "arg"></param>
		/// <returns></returns>
		object Invoke(object arg);
	}

	/// <summary>
	///   An input processing step; an encapsulated, typically short operation such as a delegate used to process the inputs of injection tokens before they are injected into a template.
	/// </summary>
	/// <typeparam name = "TIn">The type of value accepted by the step.</typeparam>
	/// <typeparam name = "TOut">The type of value returned by the step.</typeparam>
	public interface IProcessingStep<in TIn, out TOut> : IProcessingStep
	{
	}
}