using System.Collections.Generic;
using System.Diagnostics;
using FluTeLib.Core.Input;
using FluTeLib.Core.Template;
using ImpromptuInterface;
using System.Linq;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   Denotes a reference to an input, that appears within the definition of a token. Includes references to input members.
	/// </summary>
	[DebuggerDisplay("{Key,nq}")]
	public struct InputReference
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly IEnumerable<InputMemberReference> invocationChain;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly InputKey key;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string localName;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ProcessingQueue preProcessing;


		/// <summary>
		///   Constructs a new input reference, from the specified input key and chain of member references.
		/// </summary>
		/// <param name = "key">The key.</param>
		/// <param name = "memberChain">The member chain.</param>
		public InputReference(InputKey key, IEnumerable<InputMemberReference> memberChain)
		{
			this.key = key;
			invocationChain = memberChain;
			var names = new[] {key.Label}.Concat(memberChain.Select(x => x.ToString()));
			localName = string.Join("_", names);
			preProcessing = ProcessingQueue.Empty();
			preProcessing = memberChain.Any() ? preProcessing.Extend(new ReferenceMemberStep(memberChain)) : preProcessing;
		}

		/// <summary>
		///   The chain of members that is invoked on the input, as part of the definition of this reference.
		/// </summary>
		public IEnumerable<InputMemberReference> InvocationChain
		{
			get
			{
				return invocationChain;
			}
		}


		/// <summary>
		///   The key of the input references by this instance.
		/// </summary>
		public InputKey Key
		{
			get
			{
				return key;
			}
		}

		/// <summary>
		///   The label of the input references by this instance.
		/// </summary>
		public string Label
		{
			get
			{
				return key.Label;
			}
		}

		/// <summary>
		///   The local name of this input reference, used when accessing input references by name in some cases.
		/// </summary>
		public string LocalName
		{
			get
			{
				return localName;
			}
		}

		/// <summary>
		///   The preprocessing steps used on the input, as it is referenced by this instance (e.g. member invocation steps, etc)
		/// </summary>
		public ProcessingQueue PreProcessing
		{
			get
			{
				return preProcessing;
			}
		}

		/// <summary>
		///   The type of the input references by this instance.
		/// </summary>
		public InputType Type
		{
			get
			{
				return key.Type;
			}
		}

		/// <summary>
		///   Resolves the input reference against the specified parent template. E.g. resolves the internally referenced input, and preprocesses this value as appropriate.
		/// </summary>
		/// <param name = "template"></param>
		/// <returns></returns>
		public object Resolve(FluTeCoreInstance template)
		{
			if (template.Inputs.ContainsKey(Key))
			{
				var input = (IBoundInput) template.Inputs[Key];
				return PreProcessing.Invoke(input.Value);
			}
			else
			{
				throw new KeyNotFoundException("An input reference tried to resolve an input that doesn't exist in the template.");
			}
		}

		#region Nested type: ReferenceMemberStep

		private sealed class ReferenceMemberStep : IProcessingStep
		{
			private readonly IEnumerable<InputMemberReference> referenceChain;

			internal ReferenceMemberStep(IEnumerable<InputMemberReference> members)
			{
				referenceChain = members;
			}

			public string Description
			{
				get
				{
					return "References the specified members.";
				}
			}

			public string Name
			{
				get
				{
					return "ReferenceMembers";
				}
			}

			public IEnumerable<InputMemberReference> ReferenceChain
			{
				get
				{
					return referenceChain;
				}
			}

			public object Invoke(object arg)
			{
				var curObj = arg;
				foreach (var member in referenceChain)
				{
					curObj = member.MemberType == MemberType.PropertyOrField
					         	? Impromptu.InvokeGet(curObj, member.Name) : Impromptu.InvokeMember(curObj, member.Name);
				}
				return curObj;
			}
		}

		#endregion
	}
}