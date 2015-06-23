namespace FluTeLib.Core.helper.Linq
{
	/// <summary>
	///   Provides access to the index of the current iteration of an IEnumerable.
	/// </summary>
	/// <typeparam name = "T"></typeparam>
	public interface IIndexed<out T>
	{
		int Index
		{
			get;
		}

		T Value
		{
			get;
		}
	}
}