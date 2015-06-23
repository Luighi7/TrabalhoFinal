namespace FluTeLib.Fluent
{
	/// <summary>
	/// An interfance that allows you to bind a value to a pre-specified input label.
	/// </summary>
	public interface IBindToConfigurator
	{
		/// <summary>
		/// Binds a value to the input.
		/// </summary>
		/// <param name="value">The value to bind.</param>
		/// <returns></returns>
		FluTeInstance To(object value);
	}
}