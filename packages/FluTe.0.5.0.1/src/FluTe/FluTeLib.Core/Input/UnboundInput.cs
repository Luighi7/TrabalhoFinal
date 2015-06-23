using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   The class for standard inputs that haven't been bound to values.
	/// </summary>
	[DebuggerTypeProxy(typeof (DebugProxy))]
	internal class UnboundInput : BaseInput, IUnboundInput
	{
		public UnboundInput(InputKey key)
			: base(key, InputBindingStatus.Unbound)
		{
		}

		public IBoundInput Bind(object val)
		{
			return new BoundInput(Key, val);
		}

		#region Nested type: DebugProxy

		
		private class DebugProxy
		{
			private readonly UnboundInput inner;

			public DebugProxy(UnboundInput inner)
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

			public InputType Type
			{
				get
				{
					return inner.Key.Type;
				}
			}

			public InputBindingStatus Status
			{
				get
				{
					return inner.BindingStatus;
				}
			}
		}

		#endregion
	}
}