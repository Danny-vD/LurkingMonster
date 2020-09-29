using System;
using UnityEngine;
using VDFramework;

namespace Camera
{
	public class CameraMovement : BetterMonoBehaviour
	{
		[SerializeField]
		private float speed = 5;

		[SerializeField]
		private float deadZone = 0.2f;

		[SerializeField, Tooltip("The % that the movement slows down and speeds up when fully zoomed in and out respectively"), Range(0, 1)]
		private float zoomSlowDown = 0.75f;

		private CameraZoom cameraZoom;

		private float speedFactor;
		private Action moveMethod;

		private Vector2 lastPosition = Vector2.zero;

		private void Awake()
		{
			cameraZoom = GetComponent<CameraZoom>();

			moveMethod = SystemInfo.deviceType == DeviceType.Handheld ? (Action) SwipeMove : KeyBoardMove;
		}

		private void Update()
		{
			CalculateSpeedFactor();
			moveMethod();
		}

		private void KeyBoardMove()
		{
			Vector3 input = new Vector3();

			if (Input.GetKey(KeyCode.W))
			{
				input.y += 1;
			}

			if (Input.GetKey(KeyCode.S))
			{
				input.y -= 1;
			}

			if (Input.GetKey(KeyCode.A))
			{
				input.x -= 1;
			}

			if (Input.GetKey(KeyCode.D))
			{
				input.x += 1;
			}

			if (input == Vector3.zero) // To prevent unnecessary calculations while not moving
			{
				return;
			}

			CachedTransform.Translate(speedFactor * speed * input.normalized, Space.Self);
		}

		private void SwipeMove()
		{
			if (Input.touchCount != 1) // Can only move with 1 finger
			{
				return;
			}

			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				lastPosition = touch.rawPosition;
				return;
			}

			Vector2 moveDirection = lastPosition - touch.position;

			if (moveDirection.magnitude > deadZone)
			{
				CachedTransform.Translate(speedFactor * (1 + speed) * moveDirection.normalized, Space.Self);
			}

			lastPosition = touch.position;
		}

		/// <summary>
		/// Increase movement by 75% if max zoomed out, decrease by 75% if max zoomed in
		/// </summary>
		private void CalculateSpeedFactor()
		{
			float zoomfactor = cameraZoom.CalculateNormalizedZoom();

			speedFactor = 1;
			float percentageApplied;

			if (zoomfactor > 0.5f) // zoomed in, so decrease speed
			{
				percentageApplied = Mathf.InverseLerp(0.5f, 1, zoomfactor);

				speedFactor -= zoomSlowDown * percentageApplied;
			}
			else if (zoomfactor < 0.5f) // zoomed out, so increase speed
			{
				percentageApplied = Mathf.InverseLerp(0.5f, 0, zoomfactor);

				speedFactor += zoomSlowDown * percentageApplied;
			}
		}
	}
}