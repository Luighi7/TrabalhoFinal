namespace FluTeLib.Fluent
{
	public interface IBindInputConfiguration
	{
		/// <summary>
		/// Binds the specified value to the input.
		/// </summary>
		/// <param name="label">The label of the input..</param>
		/// <param name="value">The value you want.</param>
		/// <returns></returns>
		FluTeInstance Bind(string label, object value);
	}
}