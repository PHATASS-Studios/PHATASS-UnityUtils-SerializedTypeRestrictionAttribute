using System.Collections;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;


using static PHATASS.Utils.Enumerables.TypeCastedEnumerables;

namespace PHATASS.Utils.Types.Wrappers
{
	//[TO-DO] hide this class as a private sub-class and expose only access extension methods
	//MAYBE TOO remove where : class constraints, maybe make struct

	// Wraps any IList<TIn> and offers public access to it as an IList<TOut>
	// miscasts between TIn and TOut are handled like so:
	//	> on read, objects not casting to TOut return null
	//	> on write, objects not casting to TIn will raise an exception
	// TOut should be an interface or class
	public class ListCastedAccessor <TIn, TOut> :
		IList <TOut>
		where TIn : class
		where TOut : class
	{
	//IEnumerable
		IEnumerator IEnumerable.GetEnumerator ()
		{ return this.GetEnumerator(); }
	//ENDOF IEnumerable

	//IEnumerable<TOut>
		IEnumerator<TOut> IEnumerable<TOut>.GetEnumerator ()
		{ return this.GetEnumerator(); }

		private IEnumerator<TOut> GetEnumerator()
		{ return this.list.GetEnumerator().ETypeCast<TOut>(); }
	//ENDOF IEnumerable<TOut>

	//ICollection<TOut>
		int ICollection<TOut>.Count { get { return this.list.Count; }}
		bool ICollection<TOut>.IsReadOnly { get { return this.list.IsReadOnly; }}

		void ICollection<TOut>.Add (TOut item)
		{
			this.CheckObjectIsTIn(item, "Add(TOut)");
			this.list.Add(item as TIn);
		}

		void ICollection<TOut>.Clear ()
		{ this.list.Clear(); }

		bool ICollection<TOut>.Contains (TOut item)
		{
			this.CheckObjectIsTIn(item, "Contains(TOut)");
			return this.list.Contains(item as TIn);
		}

		//objects not matching TIn will automatically convert to null
		void ICollection<TOut>.CopyTo (TOut[] array, int arrayIndex)
		{ this.CopyTo(array, arrayIndex); }

		bool ICollection<TOut>.Remove (TOut item)
		{ return this.list.Remove((item as TIn)); }
	//ENDOF ICollection<TOut>

	//IList<TOut>
		TOut IList<TOut>.this[int index]
		{
			get { return (this.list[index] as TOut); }
			set
			{
				this.CheckObjectIsTIn(value, "this[index]");
				this.list[index] = (value as TIn);
			}
		}

		int IList<TOut>.IndexOf (TOut item)
		{
			this.CheckObjectIsTIn(item, "IndexOf(TOut)");
			return this.list.IndexOf(item as TIn);
		}

		void IList<TOut>.Insert (int index, TOut item)
		{
			this.CheckObjectIsTIn(item, "Insert(int, TOut)");
			this.list.Insert(index, item as TIn);
		}

		void IList<TOut>.RemoveAt (int index) { this.list.RemoveAt(index); }
	//ENDOF IList<TOut>

	//Constructor
		public ListCastedAccessor (IList<TIn> inputList)
		{ this.list = inputList; }
	//ENDOF Constructor

	//Private fields
		private IList<TIn> list;
	//ENDOF Private fields

	//Private methods
		//checks if given object is of type TIn. Raises an exception if it is not
		private void CheckObjectIsTIn (object item, string methodName = "")
		{
			if ((item == null) || (item is TIn))
			{ return; }

			throw new System.ArgumentException(string.Format(
				"ListCastedAccessor: {0} object provided is not valid type TIn {1}. It is type {2}.",
				methodName,
				typeof(TIn).ToString(),
				item.GetType().ToString())
			);
		}

		//CopyTo generic implementation
		//objects not matching TIn will automatically convert to null
		private void CopyTo (TOut[] array, int arrayIndex)
		{
		//Exception control
			if (array == null) { throw new System.ArgumentNullException("array is null"); }
			if (arrayIndex < 0) { throw new System.ArgumentOutOfRangeException("arrayIndex is less than 0"); }
			if ((arrayIndex + this.list.Count) > array.Length)
			{ throw new System.ArgumentException(string.Format(
				"Source list length [{0}] is greater than the space available from arrayIndex [{1}] to the end of destination array [{2}]",
				this.list.Count,
				arrayIndex,
				array.Length
			));}

			//Debug.Log(string.Format("ListCastedAccessor.CopyTo(): array type:({0})", array.GetType().ToString()));

		//implementation loop: cast every item as TOut. non-matching-typed objects convert to null
			for (int i = 0, iLimit = this.list.Count; i < iLimit; i++)
			{
				//Debug.Log(string.Format("element {0} type:({1})", i, this.list[i]));
				array[arrayIndex + i] = this.list[i] as TOut;
			}
		}
	//ENDOF Private methods
	}
}