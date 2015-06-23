using System.Collections.Generic;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   The input value passed to processing steps for tokens with multiple input references.
	/// </summary>
	public interface IMultiInput : IEnumerable<object>
	{
		object this[int index]
		{
			get;
		}

		object this[string local]
		{
			get;
		}

		IEnumerable<KeyValuePair<string, object>> Values
		{
			get;
		}

		T Get<T>(int index);

		T Get<T>(string localName);
	}
}