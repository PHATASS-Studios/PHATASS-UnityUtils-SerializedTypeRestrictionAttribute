using UnityEngine;

using static PHATASS.Utils.Extensions.IComparableExtensions;

namespace PHATASS.Utils.Extensions
{
//Extension methods for UnityEngine.Rect
	public static partial class RectExtensions
	{
	//Rect creation methods
		//Creates a new rect with given dimensions at target position
		public static Rect ERectFromCenterAndSize (this Vector2 position, Vector3 size)
		{ return RectExtensions.ERectFromCenterAndSize(position, size.x, size.y); }
		public static Rect ERectFromCenterAndSize (this Vector2 position, Vector2 size)
		{ return RectExtensions.ERectFromCenterAndSize(position, size.x, size.y); }
		public static Rect ERectFromCenterAndSize (this Vector2 position, float width, float height)
		{
			return new Rect(
				x: position.x - (width / 2),
				y: position.y - (height / 2),
				width: width,
				height: height
			);
		}
		// Vector3 variants
		public static Rect ERectFromCenterAndSize (this Vector3 position, Vector3 size)
		{ return ((Vector2) position).ERectFromCenterAndSize(size.x, size.y); }
		public static Rect ERectFromCenterAndSize (this Vector3 position, Vector2 size)
		{ return ((Vector2) position).ERectFromCenterAndSize(size.x, size.y); }
		public static Rect ERectFromCenterAndSize (this Vector3 position, float width, float height)
		{ return ((Vector2) position).ERectFromCenterAndSize(width, height); }

		//Creates the smallest rect possible that contains given list of points
		public static Rect ERectFromPoints (this Vector2[] points)
		{
			//prepare given coordinates for sorting
			float[] xPositions = new float[points.Length];
			float[] yPositions = new float[points.Length];
			for (int i = 0, iLimit = points.Length; i < iLimit; i++)
			{
				xPositions[i] = points[i].x;
				yPositions[i] = points[i].y;
			}

			//determine the corners of the rect from the minimum and maximum values
			float xMin = xPositions.EMinimum();
			float xMax = xPositions.EMaximum();
			float yMin = yPositions.EMinimum();
			float yMax = yPositions.EMaximum();

			//create a Rect from its corners
			return Rect.MinMaxRect(
				xmin: xMin,
				xmax: xMax,
				ymin: yMin,
				ymax: yMax
			);
		}
	//ENDOF Rect creation methods
	}
}