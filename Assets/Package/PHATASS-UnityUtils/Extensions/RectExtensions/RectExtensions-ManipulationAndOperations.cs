using UnityEngine;

namespace PHATASS.Utils.Extensions
{
	//methods for Rect manipulation
	public static partial class RectExtensions
	{
	//Rect interpolation and movement
		//moves a rect
		public static Rect EMoveRect (this Rect rect, Vector3 movement)
		{ return RectExtensions.EMoveRect(rect: rect, movement: (Vector2) movement); }
		public static Rect EMoveRect (this Rect rect, Vector2 movement)
		{
			rect.position = rect.position + movement;
			return rect;
		}
		
		//interpolates position and size
		public static Rect ELerpRect (this Rect from, Rect to, float positionLerpRate, float sizeLerpRate)
		{
			return RectExtensions.ERectFromCenterAndSize(
				position: Vector2.Lerp(from.center, to.center, positionLerpRate),
				width: Mathf.Lerp(from.width, to.width, sizeLerpRate),
				height: Mathf.Lerp(from.height, to.height, sizeLerpRate)
			);
		}
	//ENDOF Rect interpolation

	//Rect scaling
		//Scales a rect. Pivot is the point of the rectangle that will remain stationary, in local space, while the rest grows/shrinks.
		public static Rect EScale (this Rect rect, float scale, Vector2 pivot = default (Vector2))
		{
			Vector2 offset = (rect.size * (scale - 1f)) * pivot;
			return new Rect(
				x: rect.x - offset.x,
				y: rect.y - offset.y,
				width: rect.width * scale,
				height:	rect.height * scale
			);
		}		

		//Scales a rect to make it snugly fit OUTSIDE given rect
		//Scaling respects rect aspect ratio and position. At least one dimension will be equal to innerBound's, with the other dimension scaled to fit at or outside innerBound 
		public static Rect EScaleToFitOutside (this Rect self, Rect innerBound)
		{
			Vector2 boundsToSelfScale = innerBound.size / self.size;	//calculate the bounds-by-size ratio to get necessary scale
			return self.EScale(boundsToSelfScale.EMaximumDimension());	//scale initial rect by the largest dimension of the bounds-by-size ratio to fit outside desired rect
		}

		//Scales a rect to make it snugly fit INSIDE given rect
		//Scaling respects rect aspect ratio and position. At least one dimension will be equal to outerBound's, with the other dimension scaled to fit at or inside outerBound 
		public static Rect EScaleToFitInside (this Rect self, Rect outerBound)
		{
			Vector2 boundsToSelfScale = outerBound.size / self.size;	//calculate the bounds-by-size ratio to get necessary scale
			return self.EScale(boundsToSelfScale.EMinimumDimension());	//scale initial rect by the largest dimension of the bounds-by-size ratio to fit outside desired rect
		}

		//ensures a rect is not larger than an outerBound rect. If it is, scale rect down until it fits inside. Doesn't change position, only size.
		public static Rect EDownscaleIfLarger (this Rect self, Rect outerBound)
		{
			//if self is already smaller than outerBound, return it as is
			if (self.size.x <= outerBound.size.x && self.size.y <= outerBound.size.y)
			{ return self; }
			//otherwise, scale to fit inside
			return self.EScaleToFitInside(outerBound);
		}
	//ENDOF Rect scaling

	//Rect point calculation
		//returns the absolute position of a normalized point within rect, as an extension method
		public static Vector2 ENormalizedToVector2 (this Rect self, Vector2 normalizedPoint)
		{ return Rect.NormalizedToPoint(rectangle: self, normalizedRectCoordinates: normalizedPoint); }

		//returns the absolute position of a normalized point within rect, as an extension method, as a Vector3 with the assigned default Z position
		public static Vector3 ENormalizedToVector3 (this Rect self, Vector2 normalizedPoint, float zPosition = 0f)
		{ return self.ENormalizedToVector2(normalizedPoint).EToVector3(zPosition); }
	//ENDOF Rect point calculation
	}
}