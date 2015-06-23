using System;
using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   A class for bound static inputs.
	/// </summary>
	[DebuggerTypeProxy(typeof(DebugProxy))]
	internal class BoundStaticInput : BaseInput, IBoundInput
	{
		private readonly Func<object> definition;

		/// <summary>
		///   Initializes a new instance of the <see cref = "BoundStaticInput" /> class.
		/// </summary>
		/// <param name = "label">The key</param>
		/// <param name = "definition">The delegate called when it is time to resolve the input.</param>
		public BoundStaticInput(string label, Func<object> definition)
			: base(InputKey.Static(label), InputBindingStatus.Bound)
		{
			this.definition = definition;
		}

		public object Value
		{
			get
			{
				return definition();
			}
		}

		private class DebugProxy
		{
			private readonly BoundStaticInput inner;

			public DebugProxy(BoundStaticInput inner)
			{
				this.inner = inner;
			}

			public string Label
			{
				get
				{
					return inner.Key.Label;
				}
			}

			public InputBindingStatus Status
			{
				get
				{
					return inner.BindingStatus;
				}
			}

			public InputType Type
			{
				get
				{
					return inner.Key.Type;
				}
			}
		}
	}
}