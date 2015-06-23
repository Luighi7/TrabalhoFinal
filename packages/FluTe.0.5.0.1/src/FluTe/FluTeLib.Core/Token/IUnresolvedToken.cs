using FluTeLib.Core.Template;

namespace FluTeLib.Core.Token
{
	/// <summary>
	///   An interface for a token that is unresolved; i.e. the final string representation of which hasn't been determined.
	/// </summary>
	public interface IUnresolvedToken : IToken
	{
		IUnresolvedToken AttachStep(IProcessingStep step);

		IResolvedToken Resolve(FluTeCoreInstance template);
	}
}