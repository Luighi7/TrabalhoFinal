using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Collections;

namespace FluTeLib.Core.helper
{
	internal static class ImmutableSet
	{
		public static ImmutableSet<T> Empty<T>()
		{
			return ImmutableSet<T>.Empty();
		}

		public static ImmutableSet<T> From<T>(IEnumerable<T> items)
		{
			return Empty<T>().Add(items);
		}
	}

	internal class ImmutableSet<T>
	{
		private readonly FSharpSet<T> innerSet;

		private ImmutableSet(FSharpSet<T> items)
		{
			innerSet = items;
		}

		public IEnumerable<T> AsEnumerable
		{
			get
			{
				return innerSet.AsEnumerable();
			}
		}

		public ImmutableSet<T> Add(T item)
		{
			return innerSet.Add(item);
		}

		public ImmutableSet<T> Add(IEnumerable<T> items)
		{
			return items.Aggregate(innerSet, (accum, cur) => accum.Add(cur));
		}

		public bool Contains(T item)
		{
			return innerSet.Contains(item);
		}

		internal static ImmutableSet<T> Empty()
		{
			return new ImmutableSet<T>(SetModule.Empty<T>());
		}

		public static implicit operator ImmutableSet<T>(FSharpSet<T> set)
		{
			return new ImmutableSet<T>(set);
		}
	}
}