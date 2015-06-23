namespace FluTeLib.Fluent
{
	public static class StringExtensions
	{
		/// <summary>
		/// Defines a FluTe template using the template string.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns></returns>
		public static FluTePrototype Template(this string str)
		{
			return FluTe.Create(str);
		}
	}
}