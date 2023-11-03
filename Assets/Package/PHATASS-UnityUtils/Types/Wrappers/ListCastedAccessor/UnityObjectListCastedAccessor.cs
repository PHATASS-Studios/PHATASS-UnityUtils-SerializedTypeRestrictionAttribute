using System.Collections.Generic;

namespace PHATASS.Utils.Types.Wrappers
{
	// variant implicitly linked to a list of UnityEngine.Objects
	public class UnityObjectListCastedAccessor <TOut> :
		ListCastedAccessor <UnityEngine.Object, TOut>
		where TOut : class
	{
	//Constructor
		public UnityObjectListCastedAccessor (IList<UnityEngine.Object> inputList)
			: base(inputList)
		{}
	//ENDOF Constructor
	}
}