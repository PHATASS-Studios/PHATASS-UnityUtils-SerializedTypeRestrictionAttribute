#nullable disable

using UnityEngine;

// You must import the attribute to be able to use it.
// Notice how the full class name of the attribute is actually SerializedTypeRestrictionAttribute.
using SerializedTypeRestrictionAttribute = PHATASS.Utils.Attributes.SerializedTypeRestrictionAttribute;

namespace PHATASS.Docs.Examples
{
//
//This class is a weapon operator
//  It uses a given IWeapon object to shoot by calling IWeapon.Shoot()
//
	public class Gunner : MonoBehaviour
	{
	// Single-Element serialization sample
		// Editor tooltip attribute
		[Tooltip("Single-Element serialization sample. It can be any Component or ScriptableObject that implements the interface IWeapon. IWeapon.Shoot() will be called on each frame.")]

		// UnityEngine.SerializeField attribute is necessary to indicate the engine we need to serialize the following field
		[SerializeField]

		// SerializedTypeRestriction attribute is used to require serialized items to implement the given interface (IWeapon in this case)
		[SerializedTypeRestriction(typeof(IWeapon))]

		// This is the serialized field used to store a reference to our IWeapon. We use a private backing field typed UnityEngine.Object
		//	it must be of type UnityEngine.Object so references to any UnityEngine.MonoBehaviour or UnityEngine.ScriptableObject objects can be stored
		//	note it's important to always write the full UnityEngine.Object type name to differentiate from the System.Object base class
		private UnityEngine.Object _mainWeapon = null;

		// Because the UnityEngine.Object class does not implement the IWeapon interface, casting UnityEngine.Object to IWeapon is not compiler safe code.
		//	that's why we hide the serialized field as private and use a property that handles type checking and casting for simplicity
		//	when this property is called it will always return either an IWeapon object or a null reference
		public IWeapon mainWeapon //Alternatively: /*private IWeapon mainWeapon*/ /*protected IWeapon mainWeapon*/
		{ get {
			// We MUST manually check if the backing fields contents are null first
			//	this is due to a quirk with how Unity handles boxing of UnityEngine.Object null objects, which would cause a NULL UnityEngine.Object casted as an interface (IWeapon) to fail null checks.
			if (this._mainWeapon == null) { return null; }
			// We use the "as" casting operator to make elements that don't implement IWeapon to be returned as null instead of causing an exception.
			else { return this._mainWeapon as IWeapon; }
		}}
	//ENDOF Single-Element serialization sample

	//MonoBehaviour lifecycle
		private void Update ()
		{
			// On Update, we invoke the mainWeapon IWeapon.Shoot() method
			if (this.mainWeapon != null)
			{ this.mainWeapon.Shoot(); }
		}
	//ENDOF MonoBehaviour lifecycle
	}
}