namespace PHATASS.Utils.Types.Angles
{
	public static class IAngle2DFactory
	{
	// Factory static methods
		public static IAngle2D FromDegrees (float degrees)
		{ return Angle2D.FromDegrees(degrees); }

		public static IAngle2D FromRadians (float radians)
		{ return Angle2D.FromRadians(radians); }

		public static IAngle2D FromAngle2D (Angle2D originalAngle)
		{
			return Angle2D.FromAngle2D(originalAngle);
		}

	// Extensions designed to transform floats into angle objects
		public static IAngle2D EDegreesToAngle2D (this float degrees)
		{ return Angle2D.FromDegrees(degrees); }
		public static IAngle2D EDegreesToAngle2D (this double degrees)
		{ return Angle2D.FromDegrees((float) degrees); }

		public static IAngle2D ERadiansToAngle2D (this float radians)
		{ return Angle2D.FromRadians(radians); }
		public static IAngle2D ERadiansToAngle2D (this double radians)
		{ return Angle2D.FromRadians((float) radians); }
	}
}