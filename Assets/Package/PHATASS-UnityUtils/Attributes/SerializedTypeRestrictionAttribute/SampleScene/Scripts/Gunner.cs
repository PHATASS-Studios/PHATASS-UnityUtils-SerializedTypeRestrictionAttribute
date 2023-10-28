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
	//serialized fields
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Update ()
		{
			//call the serialized weapon Shoot method
			//this.weapon.Shoot();
		}
	//ENDOF MonoBehaviour lifecycle
	}
}