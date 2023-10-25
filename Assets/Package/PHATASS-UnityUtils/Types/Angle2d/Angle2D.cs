using Mathf = UnityEngine.Mathf;

using static PHATASS.Utils.Extensions.FloatExtensions;

namespace PHATASS.Utils.Types.Angles
{
	// Represents an angle, both accessible as degrees or radians
	// value is automatically clamped to 0-360 degrees (0 - 2pi rads)
	public readonly struct Angle2D : IAngle2D
	{
	//IAngle2D Implementation
		float IAngle2D.degrees { get { return this.degrees; }}
		float IAngle2D.radians { get { return this.radians; }}

		IAngle2D IAngle2D.Invert () { return this.Invert(); }

		IAngle2D IAngle2D.Add (IAngle2D angle) { return this.Add(angle); }
		IAngle2D IAngle2D.Subtract (IAngle2D angle) { return this.Subtract(angle); }

		IAngle2D IAngle2D.Multiply (float multiplier) { return this.Multiply(multiplier); }
		IAngle2D IAngle2D.Divide (float divisor) { return this.Divide(divisor); }

		IAngle2D IAngle2D.Modulus (IAngle2D divisor) { return this.Modulus(divisor); }

		IAngle2D IAngle2D.Lerp (IAngle2D destination, float step, bool clamped) { return this.Lerp(destination, step, clamped); }

		IAngle2D IAngle2D.ShortestDistance (IAngle2D other) { return this.ShortestDistance(other); }

		int IAngle2D.ShortestDirectionSign (IAngle2D other) { return this.ShortestDirectionSign(other); }

		IAngle2D IAngle2D.RotateTowardsByShortestDirection (IAngle2D destination, IAngle2D maxDelta)
		{ return this.RotateTowardsByShortestDirection(destination, maxDelta); }
	//ENDOF IAngle2D

	//Constructor
		//no public constructor to avoid ambiguity between degrees and radians
		//construction must be made through one of the properly named static factory methods
		private Angle2D (float __degrees)
		{
			this._degrees = Angle2D.ClampDegrees(__degrees);
		}

		public static Angle2D FromDegrees (float degrees)
		{
			return new Angle2D(degrees);
		}

		public static Angle2D FromRadians (float radians)
		{
			return new Angle2D(radians * Mathf.Rad2Deg);
		}

		public static Angle2D FromAngle2D (Angle2D originalAngle)
		{
			return Angle2D.FromDegrees(originalAngle.degrees);
		}
	//ENDOF Constructor


	//private properties
		private float degrees { get { return this._degrees; }}
		private float radians {	get { return this.degrees * Mathf.Deg2Rad; }}

		private readonly float _degrees;
	//ENDOF public properties

	//private fields
	//private fields

	//private methods
		private static float ClampDegrees (float degrees)
		{
			float clampedDegrees = degrees % 360;

			//if initial degrees value was negative, clampedDegrees result will be negative and need to be wrapped upwards one last time
			if (clampedDegrees < 0) { clampedDegrees += 360; }

			return clampedDegrees;
		}

		//Mathematical operations
		private IAngle2D Invert ()
		{ return Angle2D.FromDegrees(360 - this.degrees); }

		private IAngle2D Add (IAngle2D other)
		{ return Angle2D.FromDegrees(this.degrees + other.degrees); }

		private IAngle2D Subtract (IAngle2D other)
		{ return Angle2D.FromDegrees(this.degrees - other.degrees); }

		private IAngle2D Multiply (float multiplier)
		{ return Angle2D.FromDegrees(this.degrees * multiplier); }

		private IAngle2D Divide (float divisor)
		{ return Angle2D.FromDegrees(this.degrees / divisor); }
			
		private IAngle2D Modulus (IAngle2D divisor)
		{ return Angle2D.FromDegrees(this.degrees % divisor.degrees); }

		IAngle2D Lerp (IAngle2D destination, float step, bool clamped)
		{
			if (clamped) { step = step.EClamp(0f, 1f); }
			IAngle2D change = this.ShortestDistance(destination) * step;
			if ((this - destination).degrees < 180)
			{ return this - change; }
			else
			{ return this + change; }
		}

		private IAngle2D ShortestDistance (IAngle2D other)
		{
			IAngle2D difference = this.Subtract(other);
			if (difference.degrees > 180) { difference = difference - Angle2D.FromDegrees(180); }
			return difference;
		}

		 //returns the sign of the shortest distance from this angle to other angle. Will always be 1 or -1
		private int ShortestDirectionSign (IAngle2D other)
		{
			IAngle2D difference = this.Subtract(other);
			if (difference.degrees > 180) { return -1; }
			return 1;
		}

		//Other manipulation methods
		private IAngle2D RotateTowardsByShortestDirection (IAngle2D destination, IAngle2D maxDelta)
		{
			IAngle2D difference = this.Subtract(destination);

			float sign = 1f;
			if (difference.degrees < 180){ sign = -1f; }

			IAngle2D shortestDistance = this.ShortestDistance(destination);
			if (maxDelta.degrees > shortestDistance.degrees) { maxDelta = shortestDistance; }

			return this.Add(maxDelta * sign);
		}
	//ENDOF private methods

	//System overrides
		public override string ToString () { return $"{this.degrees}°"; }
	//ENDOF System overrides
	}
}