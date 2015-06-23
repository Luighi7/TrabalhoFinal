namespace FluTeLib.Core.helper.Linq
{
	/// <summary>
	///   A pair of objects. Traditionally, Left is the first element, and Right is the second element.
	/// </summary>
	/// <typeparam name = "T1"></typeparam>
	/// <typeparam name = "T2"></typeparam>
	public interface IPair<out T1, out T2>
	{
		T1 Left
		{
			get;
		}

		T2 Right
		{
			get;
		}
	}
}