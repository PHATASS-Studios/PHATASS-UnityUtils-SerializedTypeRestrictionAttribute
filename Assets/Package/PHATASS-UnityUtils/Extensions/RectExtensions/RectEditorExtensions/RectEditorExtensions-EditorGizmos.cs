#if UNITY_EDITOR

using UnityEngine;

namespace PHATASS.Utils.Extensions
{
//Rect editor gizmo drawing extensions
	public static partial class RectEditorExtensions
	{
		//Draws a Rect as an outline with given color
		public static void EDrawGizmo (this Rect rect, Color? color = null, float zPosition = 0f)
		{
			//store original color and set desired color
			Color previousColor = Gizmos.color;
			if (color != null) { Gizmos.color = (Color) color; }

			//draw the gizmo
				//left line
			Gizmos.DrawLine(
				rect.ENormalizedToVector3(Vector2.zero, zPosition),
				rect.ENormalizedToVector3(Vector2.up, zPosition)
			);
				//top line
			Gizmos.DrawLine(
				rect.ENormalizedToVector3(Vector2.up, zPosition),
				rect.ENormalizedToVector3(Vector2.one, zPosition)
			);
				//right line
			Gizmos.DrawLine(
				rect.ENormalizedToVector3(Vector2.one, zPosition),
				rect.ENormalizedToVector3(Vector2.right, zPosition)
			);
				//bottom line
			Gizmos.DrawLine(
				rect.ENormalizedToVector3(Vector2.right, zPosition),
				rect.ENormalizedToVector3(Vector2.zero, zPosition)
			);
			
			//restore original gizmos color
			if (color != null) { Gizmos.color = previousColor; }
		}
	//ENDOF Rect scaling
	}
}
#endif