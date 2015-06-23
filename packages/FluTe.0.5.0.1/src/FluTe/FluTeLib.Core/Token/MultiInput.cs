using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FluTeLib.Core.Input;
using FluTeLib.Core.Template;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   Wraps the multiple input values of a single token.
	/// </summary>
	internal class MultiInput : DynamicObject, IMultiInput
	{
		private readonly Dictionary<string, object> inputDictionary;

		private readonly IList<object> inputs;

		public MultiInput(FluTeCoreInstance parent, IEnumerable<InputReference> inputs)
		{
			inputDictionary = new Dictionary<string, object>();
			foreach (var inRef in inputs) inputDictionary.Add(inRef.LocalName, inRef.Resolve(parent));
			this.inputs = inputs.Select(x => x.Resolve(parent)).ToList();
		}

		public object this[int index]
		{
			get
			{
				return inputs[index];
			}
		}

		public object this[string local]
		{
			get
			{
				return inputDictionary[local];
			}
		}

		public IEnumerable<KeyValuePair<string, object>> Values
		{
			get
			{
				return inputDictionary;
			}
		}

		public T Get<T>(int index)
		{
			return (T) inputs[index];
		}

		public T Get<T>(string localName)
		{
			return (T) inputDictionary[localName];
		}

		public IEnumerator<object> GetEnumerator()
		{
			return inputs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (inputDictionary.ContainsKey(binder.Name))
			{
				result = inputDictionary[binder.Name];
				return true;
			}

			throw new InvalidTemplateException("The specified member does not exist within this object.");
		}
	}
}