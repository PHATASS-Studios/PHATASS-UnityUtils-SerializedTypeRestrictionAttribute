#nullable disable

using UnityEngine;

using System.Collections.Generic;   //We need to import System.Collections.Generic in order to use the generic List<T> type

// You must import the attribute to be able to use it.
// Notice how the full class name of the attribute is actually SerializedTypeRestrictionAttribute.
using SerializedTypeRestrictionAttribute = PHATASS.Utils.Attributes.SerializedTypeRestrictionAttribute;

namespace PHATASS.Docs.Examples
{
//
// This class is a radio operator
//  It uses a list of ISupport objects and calls
//
	public class RadioCaller : MonoBehaviour
	{
	// Multi-Element serialization sample
		// Editor tooltip attribute
		[Tooltip("Multi-Element serialization sample. This list can contain any number of Components or ScriptableObjects that implement the interface ISupport. ISupport.Call() will be called when the spacebar is pressed.")]

		// UnityEngine.SerializeField attribute is necessary to indicate the engine we need to serialize the following field
		[SerializeField]

		// SerializedTypeRestriction attribute is used to require serialized items to implement the given interface (ISupport in this case)
		[SerializedTypeRestriction(typeof(ISupport))]

		// This is the serialized field used to store a list of ISupport references. We use a private backing field typed List<UnityEngine.Object>()
		//  List<>() type must be UnityEngine.Object? so references to any UnityEngine.MonoBehaviour or UnityEngine.ScriptableObject objects can be stored
		//  the ? indicates this field can contain null values
		//  note it's important to always write the full UnityEngine.Object type name to differentiate from the System.Object base class
		private List<UnityEngine.Object> _supportSources = null;

		// Because the UnityEngine.Object class does not implement the ISupport interface, casting UnityEngine.Object to ISupport is not compiler safe code.
		//  that's why we hide the serialized field as private and use a type-casted wrapper for accessibility
		//  the wrapper is returned as an IList<T> of the desired ISupport type, so we can use it as a normal List
		private IList<ISupport> _supportSourcesAccessor = null;	//this is the wrapper cache. We store here our IList<ISupport> wrapper for easy access
		public IList<ISupport> supportSources
		{ get {
				if (this._supportSourcesAccessor == null && this._supportSources != null) //create accessor if unavailable
				{ this._supportSourcesAccessor = new PHATASS.Utils.Types.Wrappers.UnityObjectListCastedAccessor<ISupport>(this._supportSources); }

				return this._supportSourcesAccessor;
			}
		}
	//ENDOF Multi-Element serialization sample

	/* Copy-pasteable code sample.
	// replacements:
	//	{INTERFACE_TYPE} > the name of your desired interface or base class
	//	{FIELD_NAME} > the name of your desired interface or base class

		[Tooltip("Tooltip text")]
		[SerializeField]
		[SerializedTypeRestriction(typeof({INTERFACE_TYPE}))]
		private List<UnityEngine.Object> _{FIELD_NAME} = null;			// Private backing field. Don't access this outside the getter property
		private IList<{INTERFACE_TYPE}> _{FIELD_NAME}Accessor = null;	// Wrapper cache. We store here our IList<{INTERFACE_TYPE}> wrapper for easy access
		public IList<{INTERFACE_TYPE}> {FIELD_NAME}						// Getter property. Access this to get a usable IList<{INTERFACE_TYPE}>
		{ get {
				if (this._{FIELD_NAME}Accessor == null && this._{FIELD_NAME} != null) //create accessor if unavailable
				{ this._{FIELD_NAME}Accessor = new PHATASS.Utils.Types.Wrappers.UnityObjectListCastedAccessor<{INTERFACE_TYPE}>(this._{FIELD_NAME}); }

				return this._{FIELD_NAME}Accessor;
			}
		}

	*/ //ENDOF Copy-pasteable code sample.

	//MonoBehaviour lifecycle
		private void Update ()
		{
			// If SPACE key is pressed, invoke every available support source
			if (Input.GetKey(KeyCode.Space))
			{ this.CallSupport(); }
		}

		private void CallSupport ()
		{
			// We iterate over each ISupport available with a simple foreach loop
			foreach (ISupport supportSource in this.supportSources)
			{
				if (supportSource != null)	//we must check for nulls in case of empty entries or miscasts
				{ supportSource.Call(); }

			}
		}
	//ENDOF MonoBehaviour lifecycle
	}
}