using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluTeLib.Core.helper.Linq;

namespace FluTeLib.Core.helper.Collections
{
	/// <summary>
	/// </summary>
	public static class CollectionPlusOne
	{
		/// <summary>
		///   Adds each item to the end of the collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <param name = "items"></param>
		public static void AddMany<T>(this ICollection<T> self, IEnumerable<T> items)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (var item in items) self.Add(item);
		}


		/// <summary>
		///   Adds each item to the end of the self.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <param name = "items">The toAdd.</param>
		public static void AddMany(this IList self, IEnumerable items)
		{
			items.OfType<object>().ForEach(x => self.Add(x));
		}


		/// <summary>
		///   Adds each item to the end of the self.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <param name = "toAdd">The toAdd.</param>
		public static void AddMany(this IList self, params object[] toAdd)
		{
			self.AddMany(toAdd.AsEnumerable());
		}

		/// <summary>
		///   Adds all the supplied toAdd to the end of the current collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "col"></param>
		/// <param name = "items"></param>
		public static void AddMany<T>(this ICollection<T> col, params T[] items)
		{
			col.AddMany(items.AsEnumerable());
		}

		/// <summary>
		///   Returns the last item of the self.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <returns></returns>
		public static T Last<T>(this IList<T> self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			return self[self.Count - 1];
		}

		/// <summary>
		///   Transforms all the elements of the self as specified.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <param name = "transform"></param>
		public static void OnEach<T>(this IList<T> self, Func<T, T> transform)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			for (var i = 0; i < self.Count; i++) self[i] = transform(self[i]);
		}

		/// <summary>
		///   Removes all appearances of the specified toAdd in the self.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <param name = "toRemove">The toRemove.</param>
		public static void RemoveMany(this IList self, IEnumerable toRemove)
		{
			self.RemoveWhere(toRemove.OfType<object>().Contains);
		}


		/// <summary>
		///   Removes the supplied toAdd from the current collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <param name = "toRemove"></param>
		private static void RemoveMany<T>(this ICollection<T> self, IEnumerable<T> toRemove)
		{
			self.RemoveWhere(toRemove.Contains);
		}


		/// <summary>
		///   Removes all appearances of the supplied toAdd from the collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <param name = "toRemove"></param>
		public static void RemoveMany<T>(this ICollection<T> self, params T[] toRemove)
		{
			self.RemoveMany(toRemove.AsEnumerable());
		}

		/// <summary>
		///   Removes all toAdd that specify the specified predicate from the self.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <param name = "predicate">The predicate.</param>
		public static void RemoveWhere(this IList self, Predicate<object> predicate)
		{
			var queue = new Queue();

			self.OfType<object>().Where(x => predicate(x)).ForEach(queue.Enqueue);

			queue.OfType<object>().ForEach(self.Remove);
		}

		/// <summary>
		///   Removes all toAdd that fulfill a predicate from the current collection.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "self"></param>
		/// <param name = "predicate"></param>
		public static void RemoveWhere<T>(this ICollection<T> self, Predicate<T> predicate)
		{
			var queue = new Queue<T>();

			self.Where(x => predicate(x)).ForEach(queue.Enqueue);

			queue.ForEachDiscard(self.Remove);
		}

		/// <summary>
		///   Swaps the values in the specified indices within the self or array.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "list"></param>
		/// <param name = "index1"></param>
		/// <param name = "index2"></param>
		public static void Swap<T>(this IList<T> list, int index1, int index2)
		{
			var x = list[index1];
			list[index1] = list[index2];
			list[index2] = x;
		}
	}
}