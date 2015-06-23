using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.FSharp.Collections;

namespace PlusOne.Collections
{
	[DebuggerDisplay("Value = {Value}", Name="Name = {Key}")]
	public struct KeyValuePair<TKey, TValue>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TValue value;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TKey key;

		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue Value
		{
			get
			{
				return value;
			}
		}
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TKey Key
		{
			get
			{
				return key;
			}
		}
	}
	
	public class ImmutableMap<TKey, TValue>
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

		public ImmutableMap<TKey, TValue> Set(TKey key, TValue value)
		{
			var map = innerMap.Remove(key);
			map = map.Add(key, value);
			return map;
		}

		public ImmutableMap<TKey, TValue> Add(TKey key, TValue value)
		{
			return innerMap.Add(key, value);
		}

		public static implicit operator ImmutableMap<TKey, TValue>(FSharpMap<TKey, TValue> inner)
		{
			return new ImmutableMap<TKey, TValue>(inner);
		}
	}
}