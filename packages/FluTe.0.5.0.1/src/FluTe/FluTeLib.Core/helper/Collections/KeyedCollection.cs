using System;
using System.Collections.Generic;

namespace FluTeLib.Core.helper.Collections
{
	public class KeyedCollection<TKey, TObject> : System.Collections.ObjectModel.KeyedCollection<TKey, TObject>
	{
		private readonly Func<TObject, TKey> getKey;

		public KeyedCollection(Func<TObject, TKey> getKey)
		{
			this.getKey = getKey;
		}

		public KeyedCollection(Func<TObject, TKey> getKey, IEnumerable<TObject> objects)
			: this(getKey)
		{
			this.AddMany(objects);
		}

		protected override TKey GetKeyForItem(TObject item)
		{
			return getKey(item);
		}
	}
}