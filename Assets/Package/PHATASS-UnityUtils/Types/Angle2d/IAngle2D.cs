namespace PHATASS.Utils.Types.Angles
{
	// Exposes properties and ways to manage a single angle
	// Degrees/radians returned must always be wrapped around the 0-360 degrees range
	// Degrees/radians taken by constructor methods are not necessarily within 0-360º
	public interface IAngle2D
	{
	// Value accessors
		float degrees { get; }
		float radians { get; }

	// Mathematical operations
		IAngle2D Invert ();	//inverts the sign of the angle

		IAngle2D Add (IAngle2D angle);
		IAngle2D Subtract (IAngle2D angle);

		IAngle2D Multiply (float multiplier);
		IAngle2D Divide (float divisor);

		IAngle2D Modulus (IAngle2D divisor);

	//Other manipulation operations
		//Lerps the angle from current to destination, by a proportion of the distance given by step (0.0f no movement - 1.0f destination)
		//if clamped = false, step value won't be restricted between 0 and 1
		IAngle2D Lerp (IAngle2D destination, float step, bool clamped = true);

		IAngle2D ShortestDistance (IAngle2D other); //returns the shortest angular distance between both angles from either direction, always with positive sign

		int ShortestDirectionSign (IAngle2D other); //returns the sign of the shortest distance from this angle to other angle. Will always be 1 or -1

		IAngle2D RotateTowardsByShortestDirection (IAngle2D destination, IAngle2D maxDelta); //rotates towards desired angle at a maximum step of maxDelta, in the direction (sign) that is shortest

	// Operator overrides
		// Mathematical operations
		public static IAngle2D operator - (IAngle2D angle)
		{ return angle.Invert(); }

		public static IAngle2D operator + (IAngle2D a, IAngle2D b)
		{ return a.Add(b); }

		public static IAngle2D operator - (IAngle2D a, IAngle2D b)
		{ return a.Subtract(b); }

		public static IAngle2D operator * (IAngle2D a, float b)
		{ return a.Multiply(b); }

		public static IAngle2D operator / (IAngle2D a, float b)
		{ return a.Divide(b); }

		public static IAngle2D operator % (IAngle2D a, IAngle2D b)
		{ return a.Modulus(b); }
	//ENDOF Operator overrides
	}
}
