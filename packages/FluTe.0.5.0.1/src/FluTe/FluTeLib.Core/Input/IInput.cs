namespace FluTeLib.Core.Input
{
	/// <summary>
	///   Denotes a template input of any type and nature.
	/// </summary>
	public interface IInput
	{
		InputKey Key
		{
			get;
		}

		InputBindingStatus BindingStatus
		{
			get;
		}
	}
}