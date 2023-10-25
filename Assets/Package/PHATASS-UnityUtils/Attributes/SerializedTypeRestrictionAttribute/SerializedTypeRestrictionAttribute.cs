namespace PHATASS.Utils.Attributes
{
// Attribute defining a field as a type-restricted field (by base class or by interface)
// It should be assigned to UnityEngine.Object fields.
// When any object is assigned to it, the value is validated against typeRestriction type:
//	> if value is of type typeRestriction, value is valid
//	> if value is of type GameObject or Component, a picker appears letting user select any sibling component matching typeRestriction instead
//	> otherwise, value is invalid

	[System.AttributeUsage(
		System.AttributeTargets.Field,
		AllowMultiple = false,
		Inherited = true
	)]
	public class SerializedTypeRestrictionAttribute : UnityEngine.PropertyAttribute
	{
		public System.Type typeRestriction { get; private set; }

	//attribute constructor
		public SerializedTypeRestrictionAttribute (System.Type typeRestriction)
		{ this.typeRestriction = typeRestriction; }
	//ENDOF attribute constructor
	}
}