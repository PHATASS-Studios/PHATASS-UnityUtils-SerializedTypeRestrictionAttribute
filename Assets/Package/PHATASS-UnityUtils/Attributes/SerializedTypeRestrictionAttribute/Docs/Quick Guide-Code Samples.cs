using UnityEngine;

// You must import the attribute to be able to use it.
// Notice how the full class name of the attribute is actually SerializedTypeRestrictionAttribute.
using SerializedTypeRestrictionAttribute = PHATASS.Utils.Attributes.SerializedTypeRestrictionAttribute;

// IF we want to serialize Lists, we need to import System.Collections.Generic
// This lets us use the generic List<T> type
using System.Collections.Generic;

namespace PHATASS.Docs.Examples
{
//		> Single-Element serialization sample <
//	This attribute can be applied to serialized fields of type UnityEngine.Object. Full name must be included to differentiate from System.Object.
//	The SerializedTypeRestriction attribute requires the [SerializeField] attribute

// Copy-pasteable code sample.
	// replacements:
	//	@INTERFACE_TYPE@ > the Type name of your desired interface or base class
	//	@FIELD_NAME@ > the name of the field
/*

		[Tooltip("Tooltip text")]
		[SerializeField]
		[SerializedTypeRestriction(typeof(@INTERFACE_TYPE@))]
		private UnityEngine.Object _@FIELD_NAME@ = null;	// Private backing field. Don't access this outside the getter property
		private @INTERFACE_TYPE@ @FIELD_NAME@				// Getter property. Access this to get a usable @INTERFACE_TYPE@. May be null.
		{ get {
			if (this._@FIELD_NAME@ == null) { return null; }	// Manually handle if field is null first
			else { return this._@FIELD_NAME@ as @INTERFACE_TYPE@; }	// Return a properly boxed reference otherwise
		}}

*/
//ENDOF Copy-pasteable code sample.



//		> List serialization sample <
// This attribute can also be used on lists. They must be of type List<UnityEngine.Object>, and include the [SerializeField] attribute

// Copy-pasteable code sample.
	// replacements:
	//	@INTERFACE_TYPE@ > the Type name of your desired interface or base class
	//	@FIELD_NAME@ > the name of the field
/*

		[Tooltip("Tooltip text")]
		[SerializeField]
		[SerializedTypeRestriction(typeof(@INTERFACE_TYPE@))]
		private List<UnityEngine.Object> _@FIELD_NAME@ = null;			// Private backing field. Don't access this outside the getter property
		private IList<@INTERFACE_TYPE@> _@FIELD_NAME@Accessor = null;	// Wrapper cache. We store here our IList<@INTERFACE_TYPE@> wrapper for easy access
		public IList<@INTERFACE_TYPE@> @FIELD_NAME@						// Getter property. Access this to get a usable IList<@INTERFACE_TYPE@>
		{ get {
				if (this._@FIELD_NAME@Accessor == null && this._@FIELD_NAME@ != null) //create accessor if unavailable
				{ this._@FIELD_NAME@Accessor = new PHATASS.Utils.Types.Wrappers.UnityObjectListCastedAccessor<@INTERFACE_TYPE@>(this._@FIELD_NAME@); }

				return this._@FIELD_NAME@Accessor;
			}
		}

*/
//ENDOF Copy-pasteable code sample.
}