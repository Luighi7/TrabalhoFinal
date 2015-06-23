using System.Collections.Generic;
using System.Linq;

namespace FluTeLib.Core.helper.Objects
{
	public static class ObjectPlusOne
	{
		/// <summary>
		///   Casts the object to the specified type.
		/// </summary>
		/// <typeparam name = "T">The target type.</typeparam>
		/// <param name = "self">The object.</param>
		/// <returns></returns>
		public static T CastTo<T>(this object self)
		{
			return (T) self;
		}

		/// <summary>
		///   Returns true if the object equals any of the other objects.
		/// </summary>
		/// <param name = "self">The object.</param>
		/// <param name = "others">The collection of objects against which to compare.</param>
		/// <returns></returns>
		public static bool EqualsAny(this object self, params object[] others)
		{
			return self.EqualsAny(others.AsEnumerable());
		}

		public static bool EqualsAny(this object self, IEnumerable<object> others)
		{
			return others.Any(self.Equals);
		}
	}
}