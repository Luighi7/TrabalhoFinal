using System;

namespace FluTeLib.Core.Token
{
	[Serializable]
	public class FluTeProcessingException : ApplicationException
	{
		private readonly object inputObject;

		private readonly string processingStepName;

		private readonly int processingStepIndex;

		public FluTeProcessingException(object inputObject, string processingStepName, int processingStepIndex, Exception innerException)
			: base("An error has occurred while applying processing steps on the FluTe template.", innerException)
		{
			this.processingStepName = processingStepName;
			this.processingStepIndex = processingStepIndex;
			this.inputObject = inputObject;
			base.Data.Add("ProcessingStepName", processingStepName);
			base.Data.Add("ProcessingStepIndex", processingStepIndex);
			base.Data.Add("ProcessingStepInput", inputObject.ToString());
		}

		public object InputObject
		{
			get
			{
				return inputObject;
			}
		}

		public int ProcessingStepIndex
		{
			get
			{
				return processingStepIndex;
			}
		}

		public string ProcessingStepName
		{
			get
			{
				return processingStepName;
			}
		}
	}
}