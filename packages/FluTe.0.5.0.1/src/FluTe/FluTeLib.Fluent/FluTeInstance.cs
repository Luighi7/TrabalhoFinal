using System.Diagnostics;
using FluTeLib.Core.Template;

namespace FluTeLib.Fluent
{
	/// <summary>
	/// A concrete instance of a FluTe template. Doesn't let you add processing steps, but does let you bind inputs. This class is immutable.
	/// </summary>
	public class FluTeInstance : IBindInputConfiguration
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private readonly FluTeCoreInstance innerTemplate;

		internal FluTeInstance(FluTeCoreInstance innerTemplate)
		{
			this.innerTemplate = innerTemplate;
		}

		/// <summary>
		/// Explicitly binds a value to a named input.
		/// </summary>
		/// <param name="label">The name of the input.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public FluTeInstance Bind(string label, object value)
		{
			return new FluTeInstance(innerTemplate.BindInput(label, value));
		}

		/// <summary>
		/// Resolves this instance to its finalized product string, if all inputs have been bound to values. 
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Thrown if the template has unbound inputs.</exception>
		/// <returns></returns>
		public string Resolve()
		{
			return innerTemplate.Resolve();
		}

		public static implicit operator string(FluTeInstance template)
		{
			return template.innerTemplate.Resolve();
		}

		public static implicit operator FluTeInstance(string templateString)
		{
			return FluTe.Create(templateString).Construct();
		}
	}
}