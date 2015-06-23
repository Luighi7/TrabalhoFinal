using System;
using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   The class for static inputs that aren't bound to values.
	/// </summary>
	[DebuggerTypeProxy(typeof(DebugProxy))]
	internal class UnboundStaticInput : BaseInput, IUnboundStaticInput
	{
		public UnboundStaticInput(InputKey key)
			: base(key, InputBindingStatus.Unbound)
		{
		}

		public IBoundInput Define(Func<object> definition)
		{
			return new BoundStaticInput(Key.Label, definition);
		}

		private class DebugProxy
		{
			private readonly UnboundStaticInput inner;

			public DebugProxy(UnboundStaticInput inner)
			{
				this.inner = inner;
			}

			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public InputKey Key
			{
				get
				{
					return inner.Key;
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
	}
}