using Vector2 = UnityEngine.Vector2;

using static PHATASS.Utils.Extensions.Vector2Extensions;

namespace PHATASS.Utils.Types.Angles
{
//	Provides simple extensions to work with angles and operations with angles
//
//	Calculations
	public static partial class IAngle2DExtensions
	{
		// Returns the angular directiom from fromVector to toVector
		public static IAngle2D EAngularDirection (this Vector2 fromVector, Vector2 toVector)
		{	//EFromToAngle2D
			return fromVector.EFromToDegrees(toVector).EDegreesToAngle2D();
		}
	}
}