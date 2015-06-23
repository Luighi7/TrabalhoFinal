using System;

namespace FluTeLib.Core.Template
{
	/// <summary>
	///   An exception thrown due to an invalid or malformed template.
	/// </summary>
	[Serializable]
	public class InvalidTemplateException : Exception
	{
		private const string defaultMessage = "The template was parsed, but it could not be processed because it was invalid.";

		public InvalidTemplateException(string message = defaultMessage, Exception innerException = null)
			: base(message, innerException)
		{
		}
	}
}