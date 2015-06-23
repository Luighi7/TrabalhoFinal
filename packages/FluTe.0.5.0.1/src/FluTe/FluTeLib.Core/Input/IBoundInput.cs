namespace FluTeLib.Core.Input
{
	/// <summary>
	///   An interface for instance inputs that have been bound to a value. All literal inputs are of this sort.
	/// </summary>
	public interface IBoundInput : IInput
	{
		object Value
		{
			get;
		}
	}
}