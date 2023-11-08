using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Big artillery cannon building.
//	It INHERITS the base class StructureBase, so it behaves like any other building
//	It also IMPLEMENTS the interface IWeapon, so it can be fired as a weapon
//	Also IMPLEMENTS ISupport since artillery cannon can be also called for a support bombardment
//
	public class StructureArtilleryCannon :
		StructureBase,
		IWeapon,
		ISupport
	{
	//IWeapon implementation
		void IWeapon.Shoot()
		{
			Debug.Log("Artillery goes KA-BOOOM!");
		}
	//ENDOF IWeapon

	//ISupport implementation
		void ISupport.Call()
		{
			Debug.Log("Artillery bombardment goes KA-BOOM-BOOM-BOOM!!");
		}
	//ENDOF ISupport
	}
}