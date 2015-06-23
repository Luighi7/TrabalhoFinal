namespace FluTeLib.Core.Input
{
	/// <summary>
	///   Denotes an input that is not bound to a value.
	/// </summary>
	public interface IUnboundInput : IInput
	{
		/// <summary>
		///   Binds the specified value to the current input.
		/// </summary>
		/// <param name = "val">The value to bind.</param>
		/// <returns></returns>
		IBoundInput Bind(object val);
	}
}