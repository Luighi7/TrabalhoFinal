using System;
using System.ComponentModel;
using System.Diagnostics;
using FluTeLib.Core.Template;
using FluTeLib.Core.Token;

namespace FluTeLib.Fluent
{
	/// <summary>
	///   A prototype of a FluTe template. Lets you build on it by e.g. adding processing steps and binding static inputs. This class is immutable.
	/// </summary>
	public class FluTePrototype : IForClauseOrigin, IBindInputConfiguration
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private readonly FluTeCorePrototype innerTemplate;

		internal FluTePrototype(FluTeCorePrototype innerTemplate)
		{
			this.innerTemplate = innerTemplate;
		}

		/// <summary>
		/// Constructs a concrete instance of htis prototype using the Construct() method, and explicitly binds a value to one of its inputs.
		/// </summary>
		/// <param name="label">The label of the input.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public FluTeInstance Bind(string label, object value)
		{
			return innerTemplate.Construct().BindInput(label, value).Convert();
		}

		/// <summary>
		/// Explicitly constructs a concrete instance of this prototype.
		/// </summary>
		/// <returns></returns>
		public FluTeInstance Construct()
		{
			return new FluTeInstance(innerTemplate.Construct());
		}

		/// <summary>
		/// Binds one of this prototype's static $ inputs to a parameterless method that gets invoked when the template is resolved.
		/// </summary>
		/// <param name="label">The label of the input.</param>
		/// <param name="definition">The definition of the static input.</param>
		/// <returns></returns>
		public FluTePrototype BindStatic(string label, Func<object> definition)
		{
			return innerTemplate.DefineStaticInput(label, definition).Convert();
		}

		/// <summary>
		/// Lets you configure the template's post-processing steps.
		/// </summary>
		/// <returns></returns>
		public IForClause<string> Finally()
		{
			return new TemplateFinallyClause<string>(this);
		}

		/// <summary>
		/// Lets you configure a token's processing steps by name.
		/// </summary>
		/// <typeparam name="TOut">The type of value you expect the token's initial input to be.</typeparam>
		/// <param name="identifier">The identifier of the token.</param>
		/// <returns></returns>
		public IForClause<TOut> For<TOut>(string identifier)
		{
			return new TokenForClause<TOut>(this, identifier);
		}

		public static implicit operator FluTeInstance(FluTePrototype template)
		{
			return template.innerTemplate.Construct().Convert();
		}

		public static implicit operator FluTePrototype(string templateString)
		{
			return FluTe.Create(templateString);
		}

		#region Nested type: TemplateFinallyConfigurator

		private class TemplateFinallyClause<TOut> : IForClause<TOut>
		{
			private readonly FluTePrototype innerNext;

			public TemplateFinallyClause(FluTePrototype next)
			{
				innerNext = next;
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			public FluTePrototype Next
			{
				get
				{
					return innerNext;
				}
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			public IForClause<TNew> Attach<TNew>(IProcessingStep<TOut, TNew> step)
			{
				return new TemplateFinallyClause<TNew>(innerNext.innerTemplate.AttachPostProcessingStep(step).Convert());
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			IForClause<TNew> IForClause.Attach<TNew>(IProcessingStep<object, TNew> step)
			{
				return new TemplateFinallyClause<TNew>(innerNext.innerTemplate.AttachPostProcessingStep(step).Convert());
			}

			public IForClause<string> Finally()
			{
				throw new InvalidOperationException("You're already using the Finally configurator.");
			}

			public IForClause<TNew> For<TNew>(string identifier)
			{
				return innerNext.For<TNew>(identifier);
			}
		}

		#endregion

		#region Nested type: TokenForConfigurator

		private class TokenForClause<TOut> : IForClause<TOut>
		{
			private readonly FluTePrototype inner;

			private readonly string token;

			public TokenForClause(FluTePrototype template, string token)
			{
				inner = template;
				this.token = token;
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			public FluTePrototype Next
			{
				get
				{
					return inner;
				}
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			public IForClause<TNew> Attach<TNew>(IProcessingStep<TOut, TNew> step)
			{
				return new TokenForClause<TNew>(inner.innerTemplate.AttachProcessingStep(token, step).Convert(), token);
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			IForClause<TNew> IForClause.Attach<TNew>(IProcessingStep<object, TNew> step)
			{
				return new TokenForClause<TNew>(inner.innerTemplate.AttachProcessingStep(token, step).Convert(), token);
			}

			public IForClause<string> Finally()
			{
				return Next.Finally();
			}

			public IForClause<TNew> For<TNew>(string identifier)
			{
				return inner.For<TNew>(identifier);
			}
		}

		#endregion
	}
}