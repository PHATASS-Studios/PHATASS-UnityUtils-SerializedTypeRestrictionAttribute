using System.Collections.Generic;
using System.Collections;
using IDisposable = System.IDisposable;
//using Object = System.Object;

namespace PHATASS.Utils.Enumerables
{
// Class offering enumerator wrappers that omit and skip over null values
// When finding a null value, pointer automatically advances to next element until a not null or end of stream is found
	public static class NullOmittingEnumerables
	{
//extension methods
		public static IEnumerator<T> EOmitNulls<T> (this IEnumerator<T> enumerator)
		{ return new NullOmittingEnumerator<T>(enumerator); }

		public static IEnumerator EOmitNulls (this IEnumerator enumerator)
		{ return new NullOmittingEnumerator(enumerator); }

		public static IEnumerable<T> EOmitNulls<T> (this IEnumerable<T> enumerable)
		{ return new NullOmittingEnumerable<T>(enumerable); }

		public static IEnumerable EOmitNulls (this IEnumerable enumerable)
		{ return new NullOmittingEnumerable(enumerable); }
//ENDOF extension methods

// IEnumerables/IEnumerators
	// Generic version
		private struct NullOmittingEnumerator<T> : IEnumerator<T>
		{
		//IDisposable implementation
			void IDisposable.Dispose () { this.enumerator.Dispose(); }
		//ENDOF IDisposable implementation		

		//IEnumerator
			System.Object IEnumerator.Current { get { return this.Current; }}
			bool IEnumerator.MoveNext () { return this.MoveNext(); }
			void IEnumerator.Reset () { this.enumerator.Reset(); }
		//ENDOF IEnumerator

		//IEnumerator<T>
			T IEnumerator<T>.Current { get { return this.Current; }}
		//ENDOF IEnumerator<T>

		//constructor & internals
			public NullOmittingEnumerator (IEnumerator<T> enumerator)
			{ this.enumerator = enumerator; }

			private IEnumerator<T> enumerator;

			private T Current { get { return this.enumerator.Current; }}

			//MoveNext advances UNTIL a non-null element is found
			private bool MoveNext ()
			{
				while (true)
				{
					if (!this.enumerator.MoveNext()) { return false; }
					if (this.Current != null) { return true; }
				} 
			}
		//ENDOF constructor & internals		
		}

		private struct NullOmittingEnumerable<T> : IEnumerable<T>
		{
			IEnumerator IEnumerable.GetEnumerator ()
			{ return new NullOmittingEnumerator<T>(enumerable.GetEnumerator()); }

			IEnumerator<T> IEnumerable<T>.GetEnumerator ()
			{ return new NullOmittingEnumerator<T>(enumerable.GetEnumerator()); }

			public NullOmittingEnumerable (IEnumerable<T> enumerable)
			{ this.enumerable = enumerable;	}

			private IEnumerable<T> enumerable;
		}
	//ENDOF Generic version
	// Non-Generic version
		private struct NullOmittingEnumerator : IEnumerator
		{
		//IEnumerator
			System.Object IEnumerator.Current { get { return this.enumerator.Current; }}
			bool IEnumerator.MoveNext () { return this.MoveNext(); }
			void IEnumerator.Reset () { this.enumerator.Reset(); }
		//ENDOF IEnumerator

		//constructor & internals
			public NullOmittingEnumerator (IEnumerator enumerator)
			{ this.enumerator = enumerator; }

			private IEnumerator enumerator;

			//MoveNext advances UNTIL a non-null element is found
			private bool MoveNext ()
			{
				while (true)
				{
					if (!this.enumerator.MoveNext()) { return false; }
					if (this.enumerator.Current != null) { return true; }
				} 
			}
		//ENDOF constructor & internals
		}

		private struct NullOmittingEnumerable : IEnumerable
		{
			IEnumerator IEnumerable.GetEnumerator ()
			{ return new NullOmittingEnumerator((enumerable as IEnumerable).GetEnumerator()); }

			public NullOmittingEnumerable (IEnumerable enumerable)
			{ this.enumerable = enumerable;	}

			private IEnumerable enumerable;
		}
	// Non-Generic version
// IEnumerables/IEnumerators
	}
}