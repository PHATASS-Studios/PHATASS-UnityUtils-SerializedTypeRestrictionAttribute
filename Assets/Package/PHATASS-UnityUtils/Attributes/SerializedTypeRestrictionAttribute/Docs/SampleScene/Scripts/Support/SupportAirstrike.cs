using UnityEngine;

namespace PHATASS.Docs.Examples
{
//
// Airstrike bombardment support source
//	It implements the ISupport interface so it can be called as a support source
//
	// You might want to add a CreateAssetMenu to your ScriptableObjects
	[CreateAssetMenu(menuName = "PHATASS/Examples/New SupportAirstrike", order = 0, fileName = "SupportAirstrike")]
	public class SupportAirstrike :
		ScriptableObject,
		ISupport
	{
	//ISupport implementation
		void ISupport.Call()
		{
			Debug.Log("Airstrike goes WHOOOOSHHH-BOOM!!");
		}
	//ENDOF ISupport
	}
}