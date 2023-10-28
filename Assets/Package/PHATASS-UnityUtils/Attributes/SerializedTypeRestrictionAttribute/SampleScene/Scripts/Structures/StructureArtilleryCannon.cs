using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Big artillery cannon building.
//	It INHERITS the base class StructureBase, so it behaves like any other building
//	It also IMPLEMENTS the interface IWeapon, so it can be fired as a weapon
//
	public class StructureArtilleryCannon : StructureBase, IWeapon
	{
	//IWeapon implementation
		void IWeapon.Shoot()
		{
			Debug.Log("Artillery goes KA-BOOOM!");
		}
	//ENDOF IWeapon
	}
}