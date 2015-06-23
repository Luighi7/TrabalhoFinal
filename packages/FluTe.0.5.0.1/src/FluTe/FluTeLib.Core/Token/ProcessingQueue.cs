using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluTeLib.Core.helper;
using FluTeLib.Core.helper.Linq;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   Denotes a sequence of processing steps that transform the input value(s) of a single token.
	/// </summary>
	[DebuggerDisplay("Count = {Count,nq}")]
	public class ProcessingQueue
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private readonly ImmutableList<IProcessingStep> steps;

		private ProcessingQueue()
			: this(ImmutableList.Empty<IProcessingStep>())
		{
		}

		private ProcessingQueue(ImmutableList<IProcessingStep> steps)
		{
			this.steps = steps;
		}

		/// <summary>
		///   Gets all extension steps in the current queue.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public IEnumerable<IProcessingStep> Steps
		{
			get
			{
				return steps.AsEnumerable;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Count
		{
			get
			{
				return steps.Count;
			}
		}

		/// <summary>
		///   Returns a completely empty processing queue.
		/// </summary>
		/// <returns></returns>
		public static ProcessingQueue Empty()
		{
			return new ProcessingQueue();
		}

		/// <summary>
		///   Extends the processing queue with the specified processing step.
		/// </summary>
		/// <param name = "step"></param>
		/// <returns></returns>
		public ProcessingQueue Extend(IProcessingStep step)
		{
			return new ProcessingQueue(steps.ConsLast(step));
		}

		/// <summary>
		///   Sequentially processes the specified value using all processing steps in the queue, starting with the initial steps.
		/// </summary>
		/// <param name = "arg"></param>
		/// <returns></returns>
		public object Invoke(object arg)
		{
			var currentResult = arg;
			foreach (var step in Steps.WithIndex())
			{
				try
				{
					currentResult = step.Value.Invoke(currentResult);
				}
				catch (Exception ex)
				{
					throw new FluTeProcessingException(currentResult, step.Value.Name, step.Index, ex);
				}
			}
			return currentResult;
		}
	}
}