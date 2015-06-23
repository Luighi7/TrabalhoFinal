using System.Diagnostics;

namespace FluTeLib.Core.helper
{
	[DebuggerDisplay("{Value}", Name = "{Key}")]
	public struct KeyValuePair<TKey, TValue>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TKey key;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TValue value;

		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TKey Key
		{
			get
			{
				return key;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue Value
		{
			get
			{
				return value;
			}
		}
	}
}