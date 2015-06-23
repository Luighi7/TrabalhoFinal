using System;
using System.Linq;

namespace FluTeLib.Fluent
{
	/// <summary>
	///   A class that provides extension methods for binding inputs.
	/// </summary>
	public static class BindExtensions
	{
		public static FluTeInstance Bind(this IBindInputConfiguration config, params Func<string, object>[] inputLambdas)
		{
			foreach (var lambda in inputLambdas)
			{
				var name = lambda.Method.GetParameters().First().Name;
				var localLambda = lambda;
				config = config.Bind(name, localLambda(name));
			}
			return (FluTeInstance) config;
		}

		public static IBindToConfigurator Bind(this IBindInputConfiguration config, string name)
		{
			return new BindToConfigurator(config, name);
		}

		#region Nested type: BindToConfigurator

		private class BindToConfigurator : IBindToConfigurator
		{
			private readonly IBindInputConfiguration innerConfig;

			private readonly string input;

			public BindToConfigurator(IBindInputConfiguration innerConfig, string input)
			{
				this.innerConfig = innerConfig;
				this.input = input;
			}

			public FluTeInstance To(object value)
			{
				return innerConfig.Bind(input, value);
			}
		}

		#endregion
	}
}