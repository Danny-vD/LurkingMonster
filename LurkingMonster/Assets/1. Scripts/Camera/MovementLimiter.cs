using System;
using UnityEngine;
using VDFramework;

namespace Camera
{
	public class MovementLimiter : BetterMonoBehaviour
	{
		[SerializeField, Header("X: Right, Y: Top, Z: Left, W: Bottom")]
		private Vector4 RelativeLimits;

		private Vector3 startPos;

		private void Awake()
		{
			startPos = CachedTransform.position;

			// Inverse the sign to make the min-max calculations correct
			RelativeLimits.z *= -1;
			RelativeLimits.w *= -1;
		}

		// Late update to be certain the camera had a chance to move
		private void LateUpdate()
		{
			// In local space we are origin, so the startPos position is the inverse vector we moved
			Vector3 delta = -CachedTransform.InverseTransformPoint(startPos);

			if (NotOutsideBounds(delta))
			{
				return;
			}

			// apply the x and y limits to the delta vector
			delta.x = Mathf.Max(Mathf.Min(delta.x, RelativeLimits.x), RelativeLimits.z);
			delta.y = Mathf.Max(Mathf.Min(delta.y, RelativeLimits.y), RelativeLimits.w);

			delta.z = 0; // Keep Z the same
			
			// Set the position back to where we started and translate over the limited delta
			CachedTransform.position = startPos;
			CachedTransform.Translate(delta, Space.Self);
		}

		private bool NotOutsideBounds(Vector3 delta)
		{
			return delta.x >= RelativeLimits.z &&
				   delta.x <= RelativeLimits.x &&
				   delta.y >= RelativeLimits.w &&
				   delta.y <= RelativeLimits.y;
		}
	}
}