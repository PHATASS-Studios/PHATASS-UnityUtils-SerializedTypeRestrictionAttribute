using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Truck with a big ICBM missile.
//	It INHERITS the base class VehicleBase, so it behaves like any other vehicle
//	It also IMPLEMENTS the interface IWeapon, so it can be fired as a weapon
//
	public class VehicleMissileTruck : VehicleBase, IWeapon
	{
	//IWeapon implementation
		void IWeapon.Shoot()
		{
			Debug.Log("Missile goes WOOOOOOSH-BANG!!");
		}
	//ENDOF IWeapon
	}
}