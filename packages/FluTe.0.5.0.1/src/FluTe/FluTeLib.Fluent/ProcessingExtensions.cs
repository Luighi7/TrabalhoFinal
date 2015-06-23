using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluTeLib.Core.Input;

namespace FluTeLib.Fluent
{
	/// <summary>
	/// Provides extension methods for attaching processing steps to tokens.
	/// </summary>
	public static class ProcessingExtensions
	{
		/// <summary>
		/// Casts the value from one type to another.
		/// </summary>
		/// <typeparam name="TOut">The type of the cast.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <returns></returns>
		public static IForClause<TOut> As<TOut>(this IForClause config)
		{
			return config.Attach(ProcessingStep.As<TOut>());
		}

		/// <summary>
		/// Casts the value to a sequence of a particular type.
		/// </summary>
		/// <typeparam name="T">The type of the sequence.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<T>> AsSeq<T>(this IForClause config)
		{
			return config.Attach(ProcessingStep.AsSeq<T>());
		}

		/// <summary>
		/// Casts an arbitrary sequence to a specific sequence using the LINQ <code>Cast<typeparamref name="T"/>()</code> method.
		/// </summary>
		/// <typeparam name="T">The type of the target sequence.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<T>> SeqCast<T>(this IForClause<IEnumerable> config)
		{
			return config.Attach(ProcessingStep.SeqCast<T>());
		}

		/// <summary>
		/// Casts an arbitrary sequence to a specific sequence using the LINQ <code>OfType()</code> method.
		/// </summary>
		/// <typeparam name="T">The type of the target sequence.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<T>> SeqOfType <T>(this IForClause<IEnumerable> config)
		{
			return config.Attach(ProcessingStep.SeqOfType<T>());
		}

		/// <summary>
		/// Casts an arbitrary value to a sequence of type <code>Object</code>.
		/// </summary>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<object>> AsSeq(this IForClause config)
		{
			return config.Attach(ProcessingStep.As<IEnumerable<object>>());
		}

		/// <summary>
		/// Binds one or more static inputs using implicit lambda syntax.
		/// </summary>
		/// <param name="template">The target template.</param>
		/// <param name="definitions">The lambdas or other methods that define the static inputs. The parameter names must match input labels.</param>
		/// <returns></returns>
		public static FluTePrototype BindStatic(this FluTePrototype template, params Func<string, object>[] definitions)
		{
			foreach (var definition in definitions)
			{
				var name = definition.Method.GetParameters().First().Name;
				var localDef = definition;
				template = template.BindStatic(name, () => localDef(name));
			}
			return template;
		}

		/// <summary>
		/// Performs a strongly typed, arbitrary transformation.
		/// </summary>
		/// <typeparam name="TIn">The type of the input.</typeparam>
		/// <typeparam name="TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name="transform">The transformation to perform.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do<TIn, TOut>
			(this IForClause<TIn> config, Func<TIn, TOut> transform)
		{
			return config.Attach(ProcessingStep.Do(transform));
		}

		/// <summary>
		///  Performs a dynamically typed, arbitrary transformation.
		/// </summary>
		/// <typeparam name="TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name = "transform">The transformation to perform.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do<TOut>(this IForClause config, Func<dynamic, TOut> transform)
		{
			return config.Attach(ProcessingStep.Do(transform));
		}


		/// <summary>
		/// On for clauses that configure tokens with two inputs, performs a strongly typed, arbitrary transformation that makes use of both inputs.
		/// </summary>
		/// <typeparam name = "TIn1">The type of the first input.</typeparam>
		/// <typeparam name = "TIn2">The type of the second input</typeparam>
		/// <typeparam name = "TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name = "transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do2<TIn1, TIn2, TOut>
			(this IForClause<IMultiInput> config, Func<TIn1, TIn2, TOut> transform)
		{
			Func<IMultiInput, TOut> func = x => transform(x.Get<TIn1>(0), x.Get<TIn2>(1));
			return config.As<IMultiInput>().Do(func);
		}

		/// <summary>
		/// On for clauses that configure tokens with two inputs, performs a dynamically typed, arbitrary transformation that makes use of both inputs.
		/// </summary>
		/// <typeparam name = "TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name = "transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do2<TOut>
			(this IForClause<IMultiInput> config, Func<dynamic, dynamic, TOut> transform)
		{
			return config.Do2<dynamic, dynamic, TOut>(transform);
		}

		/// <summary>
		/// On for clauses that configure tokens with three inputs, performs a strongly typed, arbitrary transformation that makes use of all inputs.
		/// </summary>
		/// <typeparam name = "TIn1">The type of the first input.</typeparam>
		/// <typeparam name = "TIn2">The type of the seconbd input</typeparam>
		/// <typeparam name = "TIn3">The type of the third input</typeparam>
		/// <typeparam name = "TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name = "transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do3<TIn1, TIn2, TIn3, TOut>
			(this IForClause<IMultiInput> config, Func<TIn1, TIn2, TIn3, TOut> transform)
		{
			Func<IMultiInput, TOut> func = x => transform(x.Get<TIn1>(0), x.Get<TIn2>(1), x.Get<TIn3>(2));
			return config.Do(func);
		}

		/// <summary>
		/// On for clauses that configure tokens with three inputs, performs a dynamically typed, arbitrary transformation that makes use of all inputs.
		/// </summary>
		/// <typeparam name = "TOut">The type of the output.</typeparam>
		/// <param name="config">The for clause to which the step is added.</param>
		/// <param name = "transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> Do3<TOut>
			(this IForClause<IMultiInput> config, Func<dynamic, dynamic, dynamic, TOut> transform)
		{
			return config.Do3<dynamic, dynamic, dynamic, TOut>(transform);
		}


		/// <summary>
		/// Begins configuring a token with the specified identifier, and adds a processing step to it. The token is chosen based on the parameter name of the supplied method.
		/// </summary>
		/// <typeparam name = "TIn">The type of the input value.</typeparam>
		/// <typeparam name = "TOut">The type of the output value.</typeparam>
		/// <param name = "config">The config.</param>
		/// <param name = "transform">The transformation. The token is chosen based on the input parameter name of this method.</param>
		/// <returns></returns>
		public static IForClause<TOut> For<TIn, TOut>
			(this IForClauseOrigin config, Func<TIn, TOut> transform)
		{
			var name = transform.Method.GetParameters().First().Name;
			return config.For<TIn>(name).Do(transform);
		}


		/// <summary>
		/// Begins configuring a dynamically typed token with the specified identifier.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="identifier">The identifier.</param>
		/// <returns></returns>
		public static IForClause<dynamic> For(this IForClauseOrigin config, string identifier)
		{
			return config.For<dynamic>(identifier);
		}
		/// <summary>
		///   Begins configuring the specified token, and attaches a processing step to it. The token is chosen based on the parameter name of the supplied method.
		/// </summary>
		/// <typeparam name = "TOut">The type of the out.</typeparam>
		/// <param name = "config">The config.</param>
		/// <param name = "transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> For<TOut>(this IForClauseOrigin config, Func<dynamic, TOut> transform)
		{
			return config.For<dynamic, TOut>(transform);
		}

		/// <summary>
		/// Begins configuring a single token that has many inputs.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="identifier">The identifiero f hte token.</param>
		/// <returns></returns>
		public static IForClause<IMultiInput> ForMany
			(this IForClauseOrigin config, string identifier)
		{
			return config.For<IMultiInput>(identifier);
		}
		/// <summary>
		/// Invokes the LINQ method <code>Select</code> on the input.
		/// </summary>
		/// <typeparam name="TIn">The type of the in.</typeparam>
		/// <typeparam name="TOut">The type of the out.</typeparam>
		/// <param name="config">The config.</param>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<TOut>> Select<TIn, TOut>(this IForClause<IEnumerable<TIn>> config, Func<TIn, TOut> selector)
		{
			return config.Attach(ProcessingStep.Select(selector));
		}

		/// <summary>
		/// Invokes the LINQ method <code>Where</code> on the input.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="config">The config.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static IForClause<IEnumerable<T>> Where<T>(this IForClause<IEnumerable<T>> config, Func<T, bool> predicate)
		{
			return config.Attach(ProcessingStep.Where(predicate));
		}

		/// <summary>
		/// Joins a series of strings.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="delimeter">The delimeter.</param>
		/// <returns></returns>
		public static IForClause<string> TextJoin(this IForClause<IEnumerable<string>> config, string delimeter)
		{
			return config.Attach(ProcessingStep.TextJoin(delimeter));
		}

		/// <summary>
		/// Begins configuring the post-processing steps of the template by adding a specific transformation.
		/// </summary>
		/// <typeparam name="TOut">The type of the output/</typeparam>
		/// <param name="config">The config.</param>
		/// <param name="transform">The transformation.</param>
		/// <returns></returns>
		public static IForClause<TOut> Finally<TOut>(this IForClauseOrigin config, Func<string, TOut> transform)
		{
			return config.Finally().Do(transform);
		}

		/// <summary>
		/// Joins a series of strings using one delimeter, and the last item with a different delimeter, they way one might join a sequence of words.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="delimeter">The delimeter.</param>
		/// <param name="lastDelimeter">The last delimeter.</param>
		/// <returns></returns>
		public static IForClause<string> WordJoin(this IForClause<IEnumerable<string>> config, string delimeter, string lastDelimeter)
		{
			return config.Attach(ProcessingStep.WordJoin(delimeter, lastDelimeter));
		}
	}
}