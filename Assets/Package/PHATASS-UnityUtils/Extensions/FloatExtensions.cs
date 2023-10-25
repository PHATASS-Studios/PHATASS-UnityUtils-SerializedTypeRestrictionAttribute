using System;
using System.Collections.Generic;

namespace PHATASS.Utils.Extensions
{
	//simple mathematics on floats
	public static class FloatExtensions
	{
		//steps a float towards another by given step value
		public static float EStepTowards (this float value, float target, float step = 1)
		{
			//limit step to avoid overshooting
			if (value.EDifference(target) < step)
			{ step = value.EDifference(target); }

			return	(value > target)	
						? value - step	//if greater than target, decrement
				  :	(value < target)
						? value + step //if smaller than target, increment
						: target;	//if on target return target
		}

		//returns the absolute value of a float
		public static float EAbs (this float a)
		{
			return System.MathF.Abs(a);
		}

		//returns the absolute difference between two floats
		public static float EDifference (this float a, float b)
		{
			return System.MathF.Abs(a - b);
		}

		//returns the sign of this number (1 for positive, -1 for negative, 0 for zero)
		public static int ESign (this float number)
		{
			return System.MathF.Sign(number);
		}

		//clamps the number between minimum and maximum
		public static float EClamp (this float number, float minimum, float maximum)
		{
			return UnityEngine.Mathf.Clamp(number, minimum, maximum);
		}

	//comparison methods
		//returns the SMALLEST from an enumerable of float values
		//implementation provided by PHATASS.Utils.Types.IComparableExtensions
		public static float EMinimum (this float[] floatArray)
		{
			IComparable<float>[] comparableArray = Array.ConvertAll<float, IComparable<float>>(
				array: floatArray,
				converter: item => (IComparable<float>)item
			);
			return (float) ((IEnumerable<IComparable<float>>) comparableArray).EMinimum();
		}

		//returns the LARGEST from an enumerable of float values
		//implementation provided by PHATASS.Utils.Types.IComparableExtensions
		public static float EMaximum (this float[] floatArray)
		{
			IComparable<float>[] comparableArray = Array.ConvertAll<float, IComparable<float>>(
				array: floatArray,
				converter: item => (IComparable<float>)item
			);
			return (float) ((IEnumerable<IComparable<float>>) comparableArray).EMaximum();
		}

		//returns true if value is between a and b
		public static bool EIsBetween (this float value, float a, float b)
		{
			if (a < b && a < value && value < b) { return true; }
			if (a > b && a > value && value > b) { return true; }
			return false;
		}

	//ENDOF comparison methods
	}
}