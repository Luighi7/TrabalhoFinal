namespace FluTeLib.Core.Input
{
	/// <summary>
	///   Denotes the type of the input: Predefined, or instance.
	/// </summary>
	public enum InputType
	{
		/// <summary>
		///   An input that is supplied once the template has been constructed.
		/// </summary>
		Instance,
		/// <summary>
		///   An input that is defined before the templatei s constructed, as a delegate, and is resolved when the template is resolved into a string.,
		/// </summary>
		Static
	}
}