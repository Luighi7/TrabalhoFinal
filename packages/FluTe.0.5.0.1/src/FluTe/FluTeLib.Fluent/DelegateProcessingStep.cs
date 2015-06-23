using System;
using System.Diagnostics;
using FluTeLib.Core.Token;

namespace FluTeLib.Fluent
{
	/// <summary>
	///   The class for a processing step that wraps a delegate. Cannot be serialized normally.
	/// </summary>
	/// <typeparam name = "TIn"></typeparam>
	/// <typeparam name = "TOut"></typeparam>
	[DebuggerDisplay("{Name}")]
	internal class DelegateProcessingStep<TIn, TOut> : IProcessingStep<TIn, TOut>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string description;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string name;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Func<TIn, TOut> transform;

		public DelegateProcessingStep(string name, Func<TIn, TOut> transform, string description = "")
		{
			this.description = description;
			this.transform = transform;
			this.name = name;
		}

		public string Description
		{
			get
			{
				return description;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public object Invoke(object arg)
		{
			if (arg is TIn)
			{
				return transform((TIn) arg);
			}
			throw new InvalidCastException("Cannot cast.");
		}
	}
}