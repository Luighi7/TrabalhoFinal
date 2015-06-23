using System.Diagnostics;

namespace FluTeLib.Core.Input
{
	/// <summary>
	///   The base class for all sorts of template inputs.
	/// </summary>
	internal abstract class BaseInput : IInput
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly InputKey key;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly InputBindingStatus bindingStatus;

		protected BaseInput(InputKey key, InputBindingStatus bindingStatus)
		{
			this.key = key;
			this.bindingStatus = bindingStatus;
		}

		public InputBindingStatus BindingStatus
		{
			get
			{
				return bindingStatus;
			}
		}

		public InputKey Key
		{
			get
			{
				return key;
			}
		}
	}
}