using System;
using UnityEngine;
using VDFramework;

namespace Camera
{
	public class CameraZoom : BetterMonoBehaviour
	{
		[SerializeField]
		private float zoomFactor = 0.01f;

		private UnityEngine.Camera playerCamera;

		private float lastDistance;

		private Action ZoomMethod;
		
		private void Awake()
		{
			playerCamera = GetComponent<UnityEngine.Camera>();

			ZoomMethod = SystemInfo.deviceType == DeviceType.Handheld ? (Action) PinchZoom : ScrollZoom;
		}

		private void Update()
		{
			ZoomMethod();
		}

		private void ScrollZoom()
		{
		}

		// TODO: Add some max limit (make sure size can't be negative)
		private void PinchZoom()
		{
			if (Input.touchCount < 2) // need at least 2 fingers to pinch
			{
				return;
			}

			Touch touch1 = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);

			float distance = Vector2.Distance(touch1.position, touch2.position);

			if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
			{
				lastDistance = distance;
			}

			float deltaDistance = lastDistance - distance;
			playerCamera.orthographicSize += deltaDistance * zoomFactor;

			lastDistance = distance;
		}
	}
}