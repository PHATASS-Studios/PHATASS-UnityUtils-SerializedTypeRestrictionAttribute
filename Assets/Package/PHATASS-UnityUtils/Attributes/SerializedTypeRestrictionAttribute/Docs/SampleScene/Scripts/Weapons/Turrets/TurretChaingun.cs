using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Small turret with a chaingun that can be affixed to any structure or vehicle
//	It INHERITS the base class TurretBase, so it behaves like any other turret
//	It also IMPLEMENTS the interface IWeapon, so it can be fired as a weapon
//
	public class TurretChaingun : TurretBase, IWeapon
	{
	//IWeapon implementation
		void IWeapon.Shoot()
		{
			Debug.Log("Chaingun goes RA-TA-TA-TA!");
		}
	//ENDOF IWeapon
	}
}