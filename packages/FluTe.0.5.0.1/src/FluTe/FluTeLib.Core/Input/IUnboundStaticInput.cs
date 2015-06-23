using System;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   An interface for static inputs that haven't been bound to delegates.
	/// </summary>
	public interface IUnboundStaticInput : IInput
	{
		/// <summary>
		///   Binds the current static input to the specified definition, taken as a delegate.
		/// </summary>
		/// <param name = "definition">The delegate definition.</param>
		/// <returns>The bound input.</returns>
		IBoundInput Define(Func<object> definition);
	}
}