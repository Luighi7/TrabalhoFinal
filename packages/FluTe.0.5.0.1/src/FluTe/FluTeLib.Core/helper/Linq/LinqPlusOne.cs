using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluTeLib.Core.helper.Collections;
using FluTeLib.Core.helper.Objects;

namespace FluTeLib.Core.helper.Linq
{
	/// <summary>
	///   Provides extension methods for IEnumerable and similar interfaces.
	/// </summary>
	//[DebuggerStepThrough]
	public static class LinqPlusOne
	{
		public static bool AtLeast<T>(this IEnumerable<T> seq, int count)
		{
			return seq.WithIndex().Any(item => item.Index == count - 1);
		}

		public static bool AtMost<T>(this IEnumerable<T> seq, int count)
		{
			return !seq.WithIndex().Any(item => item.Index == count);
		}

		/// <summary>
		///   Appends the specified items to the end of the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "items">The items.</param>
		/// <returns></returns>
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> seq, params T[] items)
		{
			return seq.Concat(items.AsEnumerable());
		}

		/// <summary>
		///   Iterates through the enumerable, and returns the cached collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static IEnumerable<T> Consume<T>(this IEnumerable<T> seq)
		{
			return seq.ToArray();
		}

		/// <summary>
		///   Determines if the sequence contains all the elements contained by the other sequence (is a superset of).
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superset">The containing sequence.</param>
		/// <param name = "subset">The contained items.</param>
		/// <param name = "comparer">The comparer.</param>
		/// <returns>
		///   <c>true</c> if the specified sequence is a superset of the other sequence; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsAll<T>(this IEnumerable<T> superset, IEnumerable<T> subset, IEqualityComparer<T> comparer)
		{
			var set = new HashSet<T>(superset, comparer);
			return set.IsSupersetOf(subset);
		}

		/// <summary>
		///   Determines if the sequence shares any elements with the other sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq1">The first sequence.</param>
		/// <param name = "seq2">The second sequence..</param>
		/// <returns>
		///   <c>true</c> if the specified equence shares any elements with theo ther sequence; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsAny<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
		{
			return !seq1.Intersect(seq2).IsEmpty();
		}

		/// <summary>
		///   Returns true if the specified sequence contains the specified subsequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superseq">The superseq.</param>
		/// <param name = "subseq">The subseq.</param>
		/// <returns>
		///   <c>true</c> if the specified supersequence contains the other sequence; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsSequence<T>(this IEnumerable<T> superseq, IEnumerable<T> subseq)
		{
			return !superseq.IndexesOfSeq(subseq).IsEmpty();
		}


		/// <summary>
		///   Determines whether the specified superseq contains the specified subsequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superseq">The superseq.</param>
		/// <param name = "subseq">The subseq.</param>
		/// <param name = "comparer">The comparer.</param>
		/// <returns>
		///   <c>true</c> if the specified superseq contains the subsequence; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsSequence<T>
			(this IEnumerable<T> superseq, IEnumerable<T> subseq, IEqualityComparer<T> comparer)
		{
			return !superseq.IndexesOfSeq(subseq, comparer).IsEmpty();
		}

		/// <summary>
		///   Performs a cross join or cross product between two sequences and returns a sequence of pairs containing the results.
		/// </summary>
		/// <typeparam name = "T1">The type of the left sequence.</typeparam>
		/// <typeparam name = "T2">The type of the right sequence.</typeparam>
		/// <param name = "left">The left sequence.</param>
		/// <param name = "right">The right sequence.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<T1, T2>> Cross<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
		{
			return from item1 in left from item2 in right select Tuple.Create(item1, item2);
		}

		public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> seq)
		{
			return from item in seq
			       group item by item
			       into itemGroup where itemGroup.AtLeast(2) from duplicateItem in itemGroup select duplicateItem;
		}

		public static bool Exactly<T>(this IEnumerable<T> seq, Predicate<T> predicate, int count)
		{
			foreach (var item in seq)
			{
				if (predicate(item))
				{
					count--;
				}
				if (count < 0)
				{
					return false;
				}
			}

			return count == 0;
		}

		/// <summary>
		///   Returns the first value in the sequence, or another value if the sequence is empty.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "or">The alternative value.</param>
		/// <returns></returns>
		public static T FirstOr<T>(this IEnumerable<T> seq, T or)
		{
			return seq.IsEmpty() ? or : seq.First();
		}

		/// <summary>
		///   Iterates through the collection, invoking the specified delegate on each element.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "act">The action.</param>
		public static void ForEach<T>(this IEnumerable<T> seq, Action<T> act)
		{
			foreach (var item in seq) act(item);
		}

		public static void ForEach(this IEnumerable seq, Action<object> act)
		{
			foreach (var item in seq) act(item);
		}

		/// <summary>
		///   Iterates through the collection, invoking the specified delegate on each element and discarding the result.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <typeparam name = "TDiscard">The type of element to discard.</typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "act">The action.</param>
		public static void ForEachDiscard<T, TDiscard>(this IEnumerable<T> seq, Func<T, TDiscard> act)
		{
			foreach (var item in seq) act(item);
		}

		public static int GetSeqHashCode<T>(this IEnumerable<T> seq)
		{
			return seq.Aggregate(0, (hash, item) => hash ^ item.GetHashCode());
		}

		public static bool HasExactly<T>(this IEnumerable<T> seq, int count)
		{
			return seq.WithIndex().Exactly(x => x.Index >= count - 1, 1);
		}

		public static IEnumerable<T> IfThen<T>(this IEnumerable<T> seq, Predicate<T> cond, Func<T, T> selector)
		{
			foreach (var item in seq) yield return cond(item) ? selector(item) : item;
		}

		/// <summary>
		///   Returns the index of the first occurrence of the specified value in the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "value">The value.</param>
		/// <returns></returns>
		public static int IndexOf<T>(this IEnumerable<T> seq, T value)
		{
			return seq.IndexesOf(value).FirstOr(-1);
		}


		/// <summary>
		///   Returns the index of the first occurrence of the subsequence in the supersequences.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superseq">The supersequence.</param>
		/// <param name = "subseq">The subsequence.</param>
		/// <returns></returns>
		public static int IndexOfSequence<T>(this IEnumerable<T> superseq, IEnumerable<T> subseq)
		{
			return superseq.IndexesOfSeq(subseq).FirstOr(-1);
		}

		/// <summary>
		///   Returns the indices of all occurrences of the specified value in the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "value">The value.</param>
		/// <returns></returns>
		public static IEnumerable<int> IndexesOf<T>(this IEnumerable<T> seq, T value)
		{
			return seq.IndexesOf(x => x.Equals(value));
		}


		/// <summary>
		///   Returns a sequence containing all indices of the items that specify a predicate within the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "predicate">The predicate.</param>
		/// <returns></returns>
		public static IEnumerable<int> IndexesOf<T>(this IEnumerable<T> seq, Predicate<T> predicate)
		{
			return from item in seq.WithIndex() where predicate(item.Value) select item.Index;
		}

		/// <summary>
		///   Returns a sequence containing all the indices at which the specified subsequence appears within the specified supersequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superseq">The superseq.</param>
		/// <param name = "subseq">The subseq.</param>
		/// <returns></returns>
		public static IEnumerable<int> IndexesOfSeq<T>(this IEnumerable<T> superseq, IEnumerable<T> subseq)
		{
			return superseq.IndexesOfSeq(subseq, EqualityComparer<T>.Default);
		}

		/// <summary>
		///   Returns a sequence containing all the indices at which the specified subsequence appears within the specified supersequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "superseq">The superseq.</param>
		/// <param name = "seq">The seq.</param>
		/// <param name = "comparer">The comparer.</param>
		/// <returns></returns>
		public static IEnumerable<int> IndexesOfSeq<T>
			(this IEnumerable<T> superseq, IEnumerable<T> seq, IEqualityComparer<T> comparer)
		{
			return from subseq in superseq.SelectNeighbours(seq.Count()).WithIndex()
			       where subseq.Value.SequenceEqual(seq, comparer)
			       select subseq.Index;
		}

		/// <summary>
		///   Determines if a sequence is empty.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns>
		///   <c>true</c> if the specified sequence is empty; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEmpty<T>(this IEnumerable<T> seq)
		{
			return !seq.Any();
		}

		/// <summary>
		///   Returns a range within the sequence..
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "sequence">The sequence.</param>
		/// <param name = "start">The start.</param>
		/// <param name = "count">The count.</param>
		/// <returns></returns>
		public static IEnumerable<T> Range<T>(this IEnumerable<T> sequence, int start, int count)
		{
			return sequence.Skip(start).Take(count);
		}

		/// <summary>
		///   Selects non-intersecting groups of adjacent elements. If there is a remainder, throws an exception.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "size">The size of each non-intersecting group.</param>
		/// <returns></returns>
		public static IEnumerable<ISelection<T>> SelectInGroups<T>(this IEnumerable<T> seq, int size)
		{
			var queue = new Queue<T>();

			foreach (var item in seq)
			{
				queue.Enqueue(item);
				if (queue.Count == size)
				{
					yield return new Selection<T>(queue);
					queue.Clear();
				}
			}

			if (queue.Count != 0)
			{
				throw new InvalidOperationException("The selection operation is finished, but some elements remain.");
			}
		}

		/// <summary>
		///   Selects non-intersecting pairs of adjacent elements. If there is a remainder, throws an exception.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The seq.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<T, T>> SelectInPairs<T>(this IEnumerable<T> seq)
		{
			return seq.SelectInGroups(2).Select(x => Tuple.Create(x[0], x[1]));
		}

		/// <summary>
		///   Returns a sequence of all subsequences of the specified length.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "size">The size of each subsequence.</param>
		/// <returns></returns>
		public static IEnumerable<ISelection<T>> SelectNeighbours<T>(this IEnumerable<T> seq, int size)
		{
			var queue = new Queue<T>();

			foreach (var item in seq)
			{
				queue.Enqueue(item);

				if (queue.Count == size)
				{
					yield return new Selection<T>(queue);
				}
				queue.Dequeue();
			}
		}

		/// <summary>
		///   Returns a sequence of all neighbouring pairs.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<T, T>> SelectNeighbours<T>(this IEnumerable<T> seq)
		{
			return from subseq in seq.SelectNeighbours(2) select Tuple.Create(subseq[0], subseq[1]);
		}

		/// <summary>
		///   Performs two functions on each item, returning pairs containing the results of each.
		/// </summary>
		/// <typeparam name = "T">The type of the sequence.</typeparam>
		/// <typeparam name = "T1">The type of the left element in the pair.</typeparam>
		/// <typeparam name = "T2">The type of the right element in the pair.</typeparam>
		/// <param name = "seq">The seq.</param>
		/// <param name = "selector1">The selector1.</param>
		/// <param name = "selector2">The selector2.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<T1, T2>> SelectWiden<T, T1, T2>
			(this IEnumerable<T> seq, Func<T, T1> selector1, Func<T, T2> selector2)
		{
			return seq.Select(x => Tuple.Create(selector1(x), selector2(x)));
		}

		/// <summary>
		///   Determines if two sequences are set-equal.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "set1">The first sequence.</param>
		/// <param name = "set2">The seconde sequence.</param>
		/// <returns></returns>
		public static bool SetEquals<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
		{
			return SetEquals(set1, set2, EqualityComparer<T>.Default);
		}

		/// <summary>
		///   Determines if two sequences are set-equal using the supplied comparer.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq1">The first sequence.</param>
		/// <param name = "seq2">The second sequence..</param>
		/// <param name = "comparer">The comparer.</param>
		/// <returns></returns>
		public static bool SetEquals<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IEqualityComparer<T> comparer)
		{
			var set1 = new HashSet<T>(seq1, comparer);
			return set1.SetEquals(seq2);
		}

		/// <summary>
		///   Performs a circular shift to the left. The 1st element becomes the last, the 2nd becomes the 1st, and so on.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "shift">The shift size.</param>
		/// <returns></returns>
		public static IEnumerable<T> ShiftLeft<T>(this IEnumerable<T> seq, int shift)
		{
			return seq.Skip(shift).Concat(seq.Take(shift));
		}


		/// <summary>
		///   Performs a circular shift to the right. The last element becomes the 1st, the 1st becomes the 2nd, and so on.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "shift">The shift size.</param>
		/// <returns></returns>
		public static IEnumerable<T> ShiftRight<T>(this IEnumerable<T> seq, int shift)
		{
			return seq.TakeLast(shift).Concat(seq.SkipLast(shift));
		}

		/// <summary>
		///   Shuffles the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> seq)
		{
			var random = new Random();
			var arr = seq.ToArray();
			for (var i = 0; i < arr.Length - 1; i++) arr.Swap(i, random.Next(i + 1, arr.Length));

			return arr.AsEnumerable();
		}

		/// <summary>
		///   Returns the single value in the sequence, or another value if the sequence is empty. Throws an exception if the sequence contains more than one element.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "or">The alternative value.</param>
		/// <returns></returns>
		public static T SingleOr<T>(this IEnumerable<T> seq, T or)
		{
			return seq.IsEmpty() ? or : seq.Single();
		}

		/// <summary>
		///   Discards the last few items.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "count">The number of items to discard.</param>
		/// <returns></returns>
		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> seq, int count)
		{
			count++;
			return seq.SelectNeighbours(count).SkipLast().SelectMany(x => x);
		}

		/// <summary>
		///   Skips the last item.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> seq)
		{
			if (seq.IsEmpty()) yield break;

			T prev = default(T);
			bool started = true;
			foreach (var item in seq)
			{
				if (!started)
				{
					yield return prev;
				}
				prev = item;
				started = false;
			}
		}

		/// <summary>
		///   Splits the sequence into two sequences at the specified index.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "sequence">The sequence.</param>
		/// <param name = "index">The index.</param>
		/// <returns></returns>
		public static Tuple<IEnumerable<T>, IEnumerable<T>> SplitAt<T>(this IEnumerable<T> sequence, int index)
		{
			var newIndex = checked(index + 1);
			return Tuple.Create(sequence.Take(newIndex), sequence.Skip(newIndex));
		}

		/// <summary>
		///   Splits the sequence into several sequences at the specified indices.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "sequence">The sequence.</param>
		/// <param name = "indexes">The indices at whihc to split.</param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> SplitAt<T>(this IEnumerable<T> sequence, IEnumerable<int> indexes)
		{
			var sections = from i in indexes.SelectNeighbours() select i.Item1 - i.Item2;

			var remainder = sequence;

			foreach (var count in sections)
			{
				var pair = remainder.SplitAt(count);
				yield return pair.Item1;
				remainder = pair.Item2;
			}

			if (!remainder.IsEmpty())
			{
				yield return remainder;
			}
		}

		/// <summary>
		///   Splits the sequence whenever a member fulfills the specified predicate.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "sequence">The sequence.</param>
		/// <param name = "predicate">The predicate.</param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> SplitOn<T>(this IEnumerable<T> sequence, Predicate<T> predicate)
		{
			return sequence.SplitAt(sequence.IndexesOf(predicate));
		}

		/// <summary>
		///   Splits the sequence at each occurrence of the specified separator element.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "sequence">The sequence.</param>
		/// <param name = "separator">The separator.</param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> SplitOn<T>(this IEnumerable<T> sequence, T separator)
		{
			return sequence.SplitAt(sequence.IndexesOf(separator));
		}

		/// <summary>
		///   Retrieves the last few items.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "count">The number of items to retrieve..</param>
		/// <returns></returns>
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> seq, int count)
		{
			return seq.SelectNeighbours(count).Last();
		}

		/// <summary>
		///   Invokes the string.Join method on the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static string TextJoin<T>(this IEnumerable<T> seq)
		{
			return TextJoin(seq, string.Empty);
		}

		/// <summary>
		///   Invokes the string.Join method on the sequence.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "separator">The separator.</param>
		/// <returns></returns>
		public static string TextJoin<T>(this IEnumerable<T> seq, string separator)
		{
			var ret = string.Join(separator, seq.Select(x => x.ToString()));
			return ret;
		}

		public static System.Collections.ObjectModel.KeyedCollection<TKey, TObject> ToKeyedCollection<TKey, TObject>
			(this IEnumerable<TObject> objects) where TObject : IKeyedObject<TKey>
		{
			return new KeyedCollection<TKey, TObject>(x => x.Key, objects);
		}

		/// <summary>
		///   Returns a sequence of all items and their indices.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <returns></returns>
		public static IEnumerable<IIndexed<T>> WithIndex<T>(this IEnumerable<T> seq)
		{
			var index = 0;

			return seq.WithSideEffect(x => index++).Select(x => new Indexed<T>(index, x));
		}

		/// <summary>
		///   Invokes a delegate each time the iteration moves to a new element.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "seq">The sequence.</param>
		/// <param name = "sideEffect">The delegate to invoke.</param>
		/// <returns></returns>
		public static IEnumerable<T> WithSideEffect<T>(this IEnumerable<T> seq, Action<T> sideEffect)
		{
			return seq.Select
				(x =>
					{
						sideEffect(x);
						return x;
					});
		}

		/// <summary>
		///   Joins the items of sequences into pairs, based on index. Discards the remainder.
		/// </summary>
		/// <typeparam name = "T1">The type of the first sequence.</typeparam>
		/// <typeparam name = "T2">The type of the 2nd sequence.</typeparam>
		/// <param name = "seq1">The first sequence.</param>
		/// <param name = "seq2">The second sequence.</param>
		/// <returns></returns>
		public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> seq1, IEnumerable<T2> seq2)
		{
			return seq1.Zip(seq2, Tuple.Create);
		}

		#region Nested type: Indexed

		private class Indexed<T> : IIndexed<T>
		{
			private readonly int index;

			private readonly T value;

			public Indexed(int index, T value)
			{
				this.index = index;
				this.value = value;
			}

			public int Index
			{
				get
				{
					return index;
				}
			}

			public T Value
			{
				get
				{
					return value;
				}
			}
		}

		#endregion

		#region Nested type: Selection

		private class Selection<T> : ISelection<T>
		{
			private readonly T[] inner;

			public Selection(IEnumerable<T> items)
			{
				inner = items.ToArray();
			}

			public T this[int i]
			{
				get
				{
					return inner[i];
				}
			}

			public IEnumerator<T> GetEnumerator()
			{
				return inner.OfType<T>().GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		#endregion
	}
}