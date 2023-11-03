using System.Collections.Generic;
using System.Collections;

using IDisposable = System.IDisposable;

namespace PHATASS.Utils.Enumerables
{
// Enumerators allowing casted element access for any IEnumerables/IEnumerators
//	Casts returned items to type <TOut>	
//	if skipMiscasts == false, miscasts are returned as default(TOut)/null.
//	otherwise, elements without valid implicit cast
	public static class TypeCastedEnumerables
	{
//extension methods
		public static IEnumerator<TOut> ETypeCast <TOut> (this IEnumerator enumerator, bool skipMiscasts = true)
		{ return new TypeCastedEnumerator<TOut>(enumerator, skipMiscasts); }

		public static IEnumerable<TOut> ETypeCast <TOut> (this IEnumerable enumerable, bool skipMiscasts = true)
		{ return new TypeCastedEnumerable<TOut>(enumerable, skipMiscasts); }
//ENDOF extension methods	

// IEnumerables/IEnumerators
		private struct TypeCastedEnumerator <TOut> :
			IEnumerator<TOut>
			//where TOut : class
		{
		//IEnumerator
			object IEnumerator.Current { get { return this.enumerator.Current; }}
			bool IEnumerator.MoveNext () { return this.MoveNext(); }
			void IEnumerator.Reset () { this.enumerator.Reset(); }
		//ENDOF IEnumerator

		//IEnumerator<TOut>
			TOut IEnumerator<TOut>.Current { get { return this.castedCurrent; }}
		//ENDOF IEnumerator<TOut>

		//IDisposable
			void IDisposable.Dispose () { (this.enumerator as IDisposable)?.Dispose(); }
		//ENDOF IDisposable

		//Constructor
			public TypeCastedEnumerator (IEnumerator enumerator, bool skipMiscasts)
			{
				this.enumerator = enumerator;
				this.skipMiscasts = skipMiscasts;
			}
		//ENDOF Constructor

		//private
			private IEnumerator enumerator;
			private bool skipMiscasts;

			private TOut castedCurrent
			{
				get
				{
					if (this.enumerator.Current is TOut)
					{ return (TOut) this.enumerator.Current; }
					else { return default(TOut); };
				}
			}
			
			private bool MoveNext ()
			{
				while (true)
				{
					if (this.enumerator.MoveNext() == false)
					{ return false; }
					if (this.skipMiscasts || this.enumerator.Current is TOut)
					{ return true; }
				}
			}
		//ENDOF private
		}

		private struct TypeCastedEnumerable<TOut> : IEnumerable<TOut>
		{
			IEnumerator IEnumerable.GetEnumerator ()
			{ return new TypeCastedEnumerator<TOut>(enumerable.GetEnumerator(), skipMiscasts); }

			IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator ()
			{ return new TypeCastedEnumerator<TOut>(enumerable.GetEnumerator(), skipMiscasts); }

			public TypeCastedEnumerable (IEnumerable enumerable, bool skipMiscasts)
			{
				this.enumerable = enumerable;
				this.skipMiscasts = skipMiscasts;
			}

			private IEnumerable enumerable;
			private bool skipMiscasts;
		}
	}
//ENDOF IEnumerables/IEnumerators
}
