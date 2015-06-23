using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.FSharp.Collections;

namespace FluTeLib.Core.helper
{
	internal static class ImmutableList
	{
		public static ImmutableList<T> Empty<T>()
		{
			return ImmutableList<T>.Empty();
		}

		public static ImmutableList<T> From<T>(IEnumerable<T> items)
		{
			var list = Empty<T>();

			foreach (var item in items) list = list.ConsLast(item);

			return list;
		}

		public static ImmutableList<T> From<T>(params T[] items)
		{
			return From(items.AsEnumerable());
		}

		public static ImmutableList<T> From<T>(T item)
		{
			return From(item);
		}
	}

	[DebuggerDisplay("Count = {Count,nq}")]
	public class ImmutableList<T>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly FSharpList<T> innerList;

		private ImmutableList(FSharpList<T> list)
		{
			innerList = list;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IEnumerable<T> AsEnumerable
		{
			get
			{
				return innerList;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Count
		{
			get
			{
				return innerList.Length;
			}
		}

		public ImmutableList<T> Concat(ImmutableList<T> other)
		{
			return new ImmutableList<T>(ListModule.Append(innerList, other));
		}

		public ImmutableList<T> ConsFirst(T item)
		{
			return FSharpList<T>.Cons(item, innerList);
		}

		public ImmutableList<T> ConsLast(T item)
		{
			return ListModule.Append(innerList, FSharpList<T>.Cons(item, FSharpList<T>.Empty));
		}

		internal static ImmutableList<T> Empty()
		{
			return new ImmutableList<T>(FSharpList<T>.Empty);
		}

		public static implicit operator FSharpList<T>(ImmutableList<T> list)
		{
			return list.innerList;
		}

		public static implicit operator ImmutableList<T>(FSharpList<T> list)
		{
			return new ImmutableList<T>(list);
		}
	}
}