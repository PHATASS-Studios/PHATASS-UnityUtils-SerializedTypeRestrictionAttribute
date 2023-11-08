using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Airstrike bombardment support source
//	It implements the ISupport interface so it can be called as a support source
//
	// You might want to add a CreateAssetMenu to your ScriptableObjects
	//[CreateAssetMenu(menuName = "PHATASS/Examples/New SupportTroopTransport", order = 0, fileName = "SupportTroopTransport")]
	public class SupportTroopTransport :
		ScriptableObject,
		ISupport
	{
	//ISupport implementation
		void ISupport.Call()
		{
			Debug.Log("Troop Transport goes VROOOM-VROOM!!");
		}
	//ENDOF ISupport
	}
}