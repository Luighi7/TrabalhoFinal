using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluTeLib.Core.helper.Linq;
using FluTeLib.Core.helper.Text;
using FluTeLib.Core.Token;


namespace FluTeLib.Fluent
{
	internal static class ProcessingStep
	{
		public static IProcessingStep<object, TOut> As<TOut>()
		{
			return new DelegateProcessingStep<object, TOut>(string.Format("Cast<{0}>", typeof(TOut).Name), x => (TOut) x);
		}

		public static IProcessingStep<object, IEnumerable<TOut>> AsSeq<TOut>()
		{
			return new DelegateProcessingStep<object, IEnumerable<TOut>>
				(string.Format("CastToSeq<{0}>", typeof (TOut).Name), x => (IEnumerable<TOut>) x);
		}

		public static IProcessingStep<IEnumerable, IEnumerable<T>> SeqCast<T>()
		{
			return new DelegateProcessingStep<IEnumerable, IEnumerable<T>>(string.Format("SeqCast<{0}>", typeof(T).Name), x => x.Cast<T>());
		}

		public static IProcessingStep<IEnumerable, IEnumerable<T>> SeqOfType<T>()
		{
			return new DelegateProcessingStep<IEnumerable, IEnumerable<T>>(string.Format("SeqOf<{0}>", typeof(T).Name), x => x.OfType<T>());
		}

		public static IProcessingStep<TIn, TOut> Do<TIn, TOut>(Func<TIn, TOut> action)
		{
			return new DelegateProcessingStep<TIn, TOut>(string.Format("Do<{0}, {1}>", typeof(TIn).Name, typeof(TOut).Name), action);
		}

		public static IProcessingStep<IEnumerable<TIn>, IEnumerable<TOut>> Select<TIn, TOut>(Func<TIn, TOut> selector)
		{
			return new DelegateProcessingStep<IEnumerable<TIn>, IEnumerable<TOut>>(string.Format("Select<{0}, {1}>", typeof(TIn).Name, typeof(TOut).Name), x => x.Select(selector));
		}

		public static IProcessingStep<IEnumerable<TIn>, IEnumerable<TIn>> Where<TIn>(Func<TIn, bool> where)
		{
			return new DelegateProcessingStep<IEnumerable<TIn>, IEnumerable<TIn>>((string.Format("Where<{0}>", typeof(TIn).Name)), x => x.Where(where));
		}

		public static IProcessingStep<IEnumerable<string>, IEnumerable<string>> PadEach(string padding, int num)
		{
			return new DelegateProcessingStep<IEnumerable<string>, IEnumerable<string>>
				("PadEach", x => x.PadEach(padding, num));
		}

		public static IProcessingStep<IEnumerable<string>, string> TextJoin(string delimeter)
		{
			return new DelegateProcessingStep<IEnumerable<string>, string>("StringJoin", x => x.TextJoin());
		}

		public static IProcessingStep<IEnumerable<string>, string> WordJoin(string delimeter, string lastDelimeter)
		{
			return new DelegateProcessingStep<IEnumerable<string>, string>
				("WordJoin", x => x.SkipLast().TextJoin(delimeter) + lastDelimeter + x.Last());
		}
	}
}