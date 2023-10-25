using Vector2 = UnityEngine.Vector2;

using static PHATASS.Utils.Extensions.Vector2Extensions;

//using static PHATASS.Utils.Types.Angles.IAngle2DFactory;

namespace PHATASS.Utils.Types.Angles
{
//	Provides simple extensions to work with angles and operations with angles
//
//	Conversions
	public static partial class IAngle2DExtensions
	{
	//Creates a Vector2 of length 1 representing given angle
		public static Vector2 EAngle2DToVector2 (this IAngle2D angle)
		{ return angle.degrees.EDegreesToVector2(); }
	}
}