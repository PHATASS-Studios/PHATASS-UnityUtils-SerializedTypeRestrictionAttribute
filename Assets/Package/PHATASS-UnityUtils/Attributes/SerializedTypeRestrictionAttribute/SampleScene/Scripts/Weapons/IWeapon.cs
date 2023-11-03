namespace PHATASS.Docs.Examples
{
//
//	Interface representing a weapon.
//	Any class implementing IWeapon can be used as a weapon, by providing an implementation 
//
	public interface IWeapon
	{
		//IWeapon.Shoot() should be given an implementation that makes that weapon shoot
		void Shoot ();
	}
}