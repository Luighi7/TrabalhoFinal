using FluTeLib.Parser;

namespace FluTeLib.Fluent
{
	/// <summary>
	///   A static class that provides the core of FluTe operations.
	/// </summary>
	public static class FluTe
	{
		/// <summary>
		/// Creates a new FluTe prototype template, which you can modify by e.g. adding processing steps.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static FluTePrototype Create(string str)
		{
			return new FluTePrototype(FluTeParser.Parse(str));
		}
	}
}