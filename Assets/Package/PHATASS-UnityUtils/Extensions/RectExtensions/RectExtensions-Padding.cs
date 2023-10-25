using UnityEngine;

namespace PHATASS.Utils.Extensions
{
//Extension methods for UnityEngine.Rect
//	Padding methods: alter a Rect by moving inwards or outwards its borders
	public static partial class RectExtensions
	{
//public structs
		[System.Serializable]
		public struct Padding
		{
			[Tooltip("Padding value. Unless absoluteUnits = true this represents a proportion of the total container size.")]
			[SerializeField]
			public float paddingValue;

			[Tooltip("If true, padding size is measured as absolute units. Otherwise padding size is a proportion of the container size, with 1 being the full container size.")]
			[SerializeField]
			public bool absoluteUnits;

			public Padding (float value = 0f, bool absolute = false)
			{
				paddingValue = value;
				absoluteUnits = absolute;
			}
		}
//ENDOF public structs

//Extension methods
	//EInnerPad Adds an inner padding to the rect by shrinking it on-place. Negative padding values will grow the rect.
		public static Rect EInnerPad (
			this Rect rect,
			Padding upperPadding = default(Padding),
			Padding lowerPadding = default(Padding),
			Padding leftPadding = default(Padding),
			Padding rightPadding = default(Padding)
		) {
			float padding;
			Rect newRect = rect;

		//Upper padding
			if (upperPadding.absoluteUnits == true)
			{ padding = upperPadding.paddingValue; }
			else
			{ padding = upperPadding.paddingValue * rect.height; }

			newRect.yMax -= padding;

		//Lower padding
			if (lowerPadding.absoluteUnits == true)
			{ padding = lowerPadding.paddingValue; }
			else
			{ padding = lowerPadding.paddingValue * rect.height; }

			newRect.yMin += padding;

		//Left padding
			if (leftPadding.absoluteUnits == true)
			{ padding = leftPadding.paddingValue; }
			else
			{ padding = leftPadding.paddingValue * rect.width; }

			newRect.xMin += padding;

		//Right padding
			if (rightPadding.absoluteUnits == true)
			{ padding = rightPadding.paddingValue; }
			else
			{ padding = rightPadding.paddingValue * rect.width; }

			newRect.xMax -= padding;
			
			return newRect;
		}

		//EInverseInnerPad performs the opposite of EInnerPad
		// NOTE: behaviour is undefined if fractional padding adds up to 1f on each or any axis.
		public static Rect EInverseInnerPad (
			this Rect rect,
			Padding upperPadding = default(Padding),
			Padding lowerPadding = default(Padding),
			Padding leftPadding = default(Padding),
			Padding rightPadding = default(Padding)
		) {
		 //first we need to determine the rect's original dimensions
			float originalAbsoluteHeight = rect.height;
			float originalHeightFractionalPart = 0f;

			if (upperPadding.absoluteUnits == true)
			{ originalAbsoluteHeight += upperPadding.paddingValue; }
			else
			{ originalHeightFractionalPart += upperPadding.paddingValue; }

			if (lowerPadding.absoluteUnits == true)
			{ originalAbsoluteHeight += lowerPadding.paddingValue; }
			else
			{ originalHeightFractionalPart += lowerPadding.paddingValue; }

			if (originalHeightFractionalPart != 1f)
			{ originalAbsoluteHeight = originalAbsoluteHeight / (1f - originalHeightFractionalPart); }


			float originalAbsoluteWidth = rect.width;
			float originalWidthFractionalPart = 0f;

			if (leftPadding.absoluteUnits == true)
			{ originalAbsoluteWidth += leftPadding.paddingValue; }
			else
			{ originalWidthFractionalPart += leftPadding.paddingValue; }

			if (rightPadding.absoluteUnits == true)
			{ originalAbsoluteWidth += rightPadding.paddingValue; }
			else
			{ originalWidthFractionalPart += rightPadding.paddingValue; }

			if (originalWidthFractionalPart != 1f)
			{ originalAbsoluteWidth = originalAbsoluteWidth / (1f - originalWidthFractionalPart); }

		//knowing the rects original, final, and fractional dimensions, we reconstruct each of its sides before padding was applied.

			float padding;
			Rect newRect = rect;

		//Upper padding
			if (upperPadding.absoluteUnits == true)
			{ padding = upperPadding.paddingValue; }
			else
			{ padding = originalAbsoluteHeight * upperPadding.paddingValue; }

			newRect.yMax += padding;

		//Lower padding
			if (lowerPadding.absoluteUnits == true)
			{ padding = lowerPadding.paddingValue; }
			else
			{ padding = originalAbsoluteHeight * lowerPadding.paddingValue; }

			newRect.yMin -= padding;

		//Left padding
			if (leftPadding.absoluteUnits == true)
			{ padding = leftPadding.paddingValue; }
			else
			{ padding = originalAbsoluteWidth * leftPadding.paddingValue; }

			newRect.xMin -= padding;

		//Right padding
			if (rightPadding.absoluteUnits == true)
			{ padding = rightPadding.paddingValue; }
			else
			{ padding = originalAbsoluteWidth * rightPadding.paddingValue; }

			newRect.xMax += padding;
			
			return newRect;
		}
//ENDOF Extension methods
	}
}