using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

namespace FluTeLib.Core.helper
{
	[DebuggerDisplay("Count = {Count,nq}")]
	public class ImmutableMap<TKey, TValue> : IImmutableMap<TKey, TValue>
		where TKey : IComparable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly FSharpMap<TKey, TValue> innerMap;

		private ImmutableMap(FSharpMap<TKey, TValue> innerMap)
		{
			this.innerMap = innerMap;
		}

		public ImmutableMap(IEnumerable<Tuple<TKey, TValue>> pairs)
		{
			innerMap = new FSharpMap<TKey, TValue>(pairs);
		}

		public ImmutableMap()
			: this(MapModule.Empty<TKey, TValue>())
		{
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Count
		{
			get
			{
				return innerMap.Count;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				return innerMap[key];
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IEnumerable<KeyValuePair<TKey, TValue>> Pairs
		{
			get
			{
				return innerMap.Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value)).ToList();
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public IEnumerable<TValue> Values
		{
			get
			{
				return innerMap.Select(x => x.Value);
			}
		}

		public ImmutableMap<TKey, TValue> Add(TKey key, TValue value)
		{
			return innerMap.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return innerMap.ContainsKey(key);
		}

		public ImmutableMap<TKey, TValue> On(TKey key, Func<TValue, TValue> transform)
		{
			return Set(key, transform(this[key]));
		}

		public ImmutableMap<TKey, TValue> Set(TKey key, TValue value)
		{
			var map = innerMap.Remove(key);
			map = map.Add(key, value);
			return map;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var res = innerMap.TryFind(key);
			if (OptionModule.IsSome(res))
			{
				value = res.Value;
				return true;
			}
			else
			{
				value = default(TValue);
				return false;
			}
		}

		public static implicit operator ImmutableMap<TKey, TValue>(FSharpMap<TKey, TValue> inner)
		{
			return new ImmutableMap<TKey, TValue>(inner);
		}
	}
}