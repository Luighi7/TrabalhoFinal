using System.Collections.Generic;

namespace FluTeLib.Core.helper.Linq
{
	/// <summary>
	///   An indexed collection of sequential elements from a larger collection.
	/// </summary>
	/// <typeparam name = "T"></typeparam>
	public interface ISelection<out T> : IEnumerable<T>
	{
		T this[int index]
		{
			get;
		}
	}
}