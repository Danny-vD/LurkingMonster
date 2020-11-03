using System;
using Singletons;
using UnityEngine;
using VDFramework;

namespace CameraScripts
{
	using UnityEngine.Serialization;

	public class CameraZoom : BetterMonoBehaviour
	{
		[SerializeField]
		private float zoomSpeed = 0.1f;

		[SerializeField, Tooltip("The size to which you can zoom out")]
		private float MinimumZoom = 150;

		[SerializeField, Tooltip("The size to which you can zoom in")]
		private float MaximumZoom = 10;

		private UnityEngine.Camera playerCamera; // Fully qualified to not conflict with Camera namespace
		
		private float lastDistance;

		private Action zoomMethod;

		private void Awake()
		{
			playerCamera = GetComponent<UnityEngine.Camera>();

			zoomMethod = SystemInfo.deviceType == DeviceType.Handheld ? (Action) PinchZoom : ScrollZoom;

			MaximumZoom = Mathf.Max(MaximumZoom, 0); // Make sure it can't go below 0
		}

		private void Update()
		{
			if (TimeManager.Instance.IsPaused())
			{
				return;
			}
			
			zoomMethod();

			EnforceMinMaxZoom();
		}

		/// <summary>
		/// Returns the inversed lerp between Min and Max zoom
		/// </summary>
		public float CalculateNormalizedZoom()
		{
			return Mathf.InverseLerp(MinimumZoom, MaximumZoom, playerCamera.orthographicSize);
		}
		
		private void ScrollZoom()
		{
			float scroll = Input.mouseScrollDelta.y;

			playerCamera.orthographicSize -= scroll * 5; // Hard coded for non-mobile
		}

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
				return;
			}

			float deltaDistance = lastDistance - distance;
			playerCamera.orthographicSize += deltaDistance * zoomSpeed;

			lastDistance = distance;
		}

		private void EnforceMinMaxZoom()
		{
			playerCamera.orthographicSize =
				Mathf.Min(Mathf.Max(playerCamera.orthographicSize, MaximumZoom), MinimumZoom);
		}
	}
}