namespace PHATASS.Docs.Examples
{
//
//	Interface representing a battle support source
//	Any class implementing IWeapon can be called for battle support by providing an implementation
//
	public interface ISupport
	{
		//ISupport.Call() should be given an implementation that deploys the desired battle effect
		void Call ();
	}
}