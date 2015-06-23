using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FluTeLib.Core.helper
{
	public interface IImmutableMap<TKey, TValue>
		where TKey : IComparable
	{
		TValue this[TKey key]
		{
			get;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		IEnumerable<KeyValuePair<TKey, TValue>> Pairs
		{
			get;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IEnumerable<TValue> Values
		{
			get;
		}

		ImmutableMap<TKey, TValue> Add(TKey key, TValue value);

		bool ContainsKey(TKey key);

		ImmutableMap<TKey, TValue> On(TKey key, Func<TValue, TValue> transform);

		ImmutableMap<TKey, TValue> Set(TKey key, TValue value);
	}
}