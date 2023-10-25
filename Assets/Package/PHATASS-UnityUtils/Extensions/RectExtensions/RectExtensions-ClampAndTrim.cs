using UnityEngine;

namespace PHATASS.Utils.Extensions
{
//Extension methods for UnityEngine.Rect
	public static partial class RectExtensions
	{
	//Rect clamping and trimming methods 
		//clamp a x/y position within a rect
		public static Vector2 EClampWithinRect (this Vector2 position, Rect outerRect)
		{
			return new Vector2
			(
				x: Mathf.Clamp(position.x, outerRect.xMin, outerRect.xMax),
				y: Mathf.Clamp(position.y, outerRect.yMin, outerRect.yMax)
			);
		}
		public static Vector3 EClampWithinRect (this Vector3 position, Rect outerRect)
		{
			return new Vector3
			(
				x: Mathf.Clamp(position.x, outerRect.xMin, outerRect.xMax),
				y: Mathf.Clamp(position.y, outerRect.yMin, outerRect.yMax),
				z: position.z
			);
		}

		//ensures innerRect bounds stay within outerRect by moving innerRect if protruding.
		//if innerRect dimensions exceed outerRect, they will be centered
		public static Rect EMoveToFitWithinRect (this Rect innerRect, Rect outerRect)
		{
			return new Rect (
				x: (innerRect.width <= outerRect.width)
					? //if innerRect is thinner than outerRect, clamp its position within outerRect
						Mathf.Clamp(					
							value: innerRect.x,
							min: outerRect.xMin,
							max: outerRect.xMax - innerRect.width
						)
					: //if innerRect is wider than outerRect, center their position
						outerRect.x - ((innerRect.width - outerRect.width) / 2),
				y: (innerRect.height <= outerRect.height)
					? //if innerRect is shorter than outerRect clamp its position
						Mathf.Clamp(
							value: innerRect.y,
							min: outerRect.yMin,
							max: outerRect.yMax - innerRect.height
						)
					: //if innerRect is taller than outerRect, center their position
						innerRect.y - ((innerRect.height - outerRect.width) / 2),
				width: innerRect.width,
				height: innerRect.height
			);
		}

		//moves self rect to make its center the same as referenceRect
		public static Rect EMakeConcentric (this Rect self, Rect referenceRect)
		{
			self.center = referenceRect.center;
			return self;
		}

		//truncates innerRect dimensions to fit outerRect. may return the same rect if already small enough.
		//only alters size, returned rect's position will be the same as innerRect's
		public static Rect ETrimRectSizeToRect (this Rect innerRect, Rect outerRect)
		{
			if (innerRect.width <= outerRect.width && innerRect.height <= outerRect.height)
			{ return innerRect; }
			return new Rect (
				x: innerRect.x,
				y: innerRect.y,
				width: Mathf.Clamp(innerRect.width, 0, outerRect.width),
				height: Mathf.Clamp(innerRect.height, 0, outerRect.height)
			);
		}

		//fits a rect within another, trimming its size 
		public static Rect ETrimAndCenterRectWithinRect (this Rect innerRect, Rect outerRect)
		{
			return RectExtensions.EMoveToFitWithinRect(
				innerRect: innerRect.ETrimRectSizeToRect(outerRect),
				outerRect: outerRect
			);
		}
	//ENDOF Rect clamping and trimming methods
	}
}