using Math = System.Math;
using MathF = System.MathF;

using static PHATASS.Utils.Types.Angles.IAngle2DFactory;

namespace PHATASS.Utils.Types.Angles
{
//	Provides simple extensions to work with angles and operations with angles
//
//	Trigonometric functions
	public static partial class IAngle2DExtensions
	{
	//Cosine
		public static float ECosine (this IAngle2D angle) { return MathF.Cos(angle.radians); }
		public static double ECosineDouble (this IAngle2D angle) { return Math.Cos(angle.radians); }

	//Sine
		public static float ESine (this IAngle2D angle) { return MathF.Sin(angle.radians); }
		public static double ESineDouble (this IAngle2D angle) { return Math.Sin(angle.radians); }

	//ArcCosine
		public static IAngle2D EArcCosine (this float cosine) { return MathF.Acos(cosine).ERadiansToAngle2D(); }
		public static IAngle2D EArcCosine (this double cosine) { return Math.Acos(cosine).ERadiansToAngle2D(); }

	//ArcSine
		public static IAngle2D EArcSine (this float cosine) { return MathF.Asin(cosine).ERadiansToAngle2D(); }
		public static IAngle2D EArcSine (this double cosine) { return Math.Asin(cosine).ERadiansToAngle2D(); }
	}
}