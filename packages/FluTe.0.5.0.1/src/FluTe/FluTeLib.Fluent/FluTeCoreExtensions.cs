using FluTeLib.Core.Template;


namespace FluTeLib.Fluent
{
	internal static class FluTeCoreExtensions
	{
		internal static FluTeInstance Convert(this FluTeCoreInstance flute)
		{
			return new FluTeInstance(flute);
		}

		internal static FluTePrototype Convert(this FluTeCorePrototype flute)
		{
			return new FluTePrototype(flute);
		}
	}
}