using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   A class for inputs that have been bound to values.
	/// </summary>
	[DebuggerDisplay("{Value,nq}", Name = "{BindingStatus,nq} {Type,nq}:{Label,nq}")]
	[DebuggerTypeProxy(typeof (DebugProxy))]
	internal class BoundInput : BaseInput, IBoundInput
	{
		private readonly object value;

		public BoundInput(InputKey key, object value)
			: base(key, InputBindingStatus.Bound)
		{
			this.value = value;
		}

		public object Value
		{
			get
			{
				return value;
			}
		}

		#region Nested type: DebugProxy

		private class DebugProxy
		{
			private readonly BoundInput inner;

			public DebugProxy(BoundInput inner)
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

			public object Value
			{
				get
				{
					return inner.Value;
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