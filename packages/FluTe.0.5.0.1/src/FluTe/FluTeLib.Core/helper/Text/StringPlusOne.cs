using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluTeLib.Core.helper.Text
{
	/// <summary>
	/// </summary>
	public static class StringPlusOne
	{
		/// <summary>
		///   Iterates through the StringBuilder by character.
		/// </summary>
		/// <param name = "self">The StringBuilder.</param>
		/// <returns></returns>
		public static IEnumerable<char> ByChar(this StringBuilder self)
		{
			for (var i = 0; i < self.Length; i++) yield return self[i];
		}


		/// <summary>
		///   Takes the specified number of characters from the end of the string.
		/// </summary>
		/// <param name = "self">The string.</param>
		/// <param name = "count"/>The number of characters./param>
		///   <returns></returns>
		public static string FromEnd(this string self, int count)
		{
			return self.Substring(self.Length - count, count);
		}

		/// <summary>
		///   Takes the specified number of characters from the start of the string.
		/// </summary>
		/// <param name = "self">The string.</param>
		/// <param name = "count">The number of characters.</param>
		/// <returns></returns>
		public static string FromStart(this string self, int count)
		{
			return self.Substring(0, count);
		}


		/// <summary>
		///   Determines whether the specified self contains only letters or digits.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <returns>
		///   <c>true</c> if the specified self is word; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsWord(this string self)
		{
			return self.Aggregate(true, (cur, chr) => cur && char.IsLetterOrDigit(chr));
		}

		public static IEnumerable<string> PadEach(this IEnumerable<string> self, string pad, int num)
		{
			return self.Select(x => pad.Repeat(num) + self + pad.Repeat(num));
		}

		public static IEnumerable<string> PadLeftEach(this IEnumerable<string> self, string pad, int num)
		{
			return self.Select(x => pad.Repeat(num) + x);
		}

		public static IEnumerable<string> PadRightEach(this IEnumerable<string> self, string pad, int num)
		{
			return self.Select(x => x + pad.Repeat(num));
		}

		/// <summary>
		///   Repeats the specified string a number of times, and returns the result.
		/// </summary>
		/// <param name = "self">The self.</param>
		/// <param name = "count">The count.</param>
		/// <returns></returns>
		public static string Repeat(this string self, int count)
		{
			return string.Concat(Enumerable.Repeat(self, count));
		}

		/// <summary>
		///   Determines if two strings are equal, ignoring caps and whitespace on either side.
		/// </summary>
		/// <param name = "self">The string.</param>
		/// <param name = "other">The other.</param>
		/// <returns></returns>
		public static bool TextEquals(this string self, string other)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return self.Trim().ToUpper().Equals(other.Trim().ToUpper());
		}
	}
}