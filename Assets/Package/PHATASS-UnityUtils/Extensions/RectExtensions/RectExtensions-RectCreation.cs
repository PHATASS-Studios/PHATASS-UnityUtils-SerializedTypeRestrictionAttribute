using UnityEngine;

namespace PHATASS.Utils.Extensions
{
	//methods for Rect manipulation
	public static partial class RectExtensions
	{
	//Rect creation methods
		//Creates a Rect with desired aspect ratio. unless set, height is 1f and position is Vector2(0,0)
		public static Rect ERectFromAspectRatio (this float aspect, float height = 1f, Vector2 position = default(Vector2))
		{
			return new Rect(
				x: position.x,
				y: position.y,
				width: height * aspect,
				height: height
			);
		}
	//ENDOF Rect scaling
	}
}