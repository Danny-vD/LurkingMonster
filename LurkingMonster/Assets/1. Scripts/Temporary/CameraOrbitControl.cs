using UnityEngine;
using VDFramework;

namespace Temporary
{
	public class CameraOrbitControl : BetterMonoBehaviour
	{
		[SerializeField]
		private float cameraDistance = 10.0f;

		[SerializeField, Tooltip("1 pixel equals {value} degrees")]
		private float PixelToDegreeRatio = 0.25f;

		private Transform cameraTransform;

		private Vector2 previousFramePosition;

		private void Start()
		{
			// No null check to allow it to crash as the earliest point
			cameraTransform = Camera.main.transform;

			previousFramePosition = Vector2.zero;
		}

		private void LateUpdate()
		{
			// In late update to make sure the player has already moved
			cameraTransform.position = CachedTransform.position - cameraTransform.forward * cameraDistance;
		}

		private void Update()
		{
			if (!Input.GetMouseButton(1))
			{
				if (Input.GetMouseButtonUp(1))
				{
					previousFramePosition = Vector2.zero;
				}

				return;
			}

			if (Input.GetMouseButtonDown(1))
			{
				previousFramePosition = Input.mousePosition;
				return;
			}

			// Cast to vector2 to prevent ambiguity
			Vector2 mouseDelta = (Vector2) Input.mousePosition - previousFramePosition;

			cameraTransform.RotateAround(CachedTransform.position, Vector3.up, mouseDelta.x * PixelToDegreeRatio);

			// Ignore delta Y for now
			//cameraTransform.RotateAround(cameraTransform.right, Vector3.right, mouseDelta.y * PixelToDegreeRatio);

			previousFramePosition = Input.mousePosition;
		}
	}
}