using System;
using UnityEngine;
using VDFramework;

namespace Temporary
{
	public class PlayerMovement : BetterMonoBehaviour
	{
		[SerializeField]
		private float speed = 5.0f;

		private Transform cameraTransform;

		private CharacterController characterController;

		private void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			if (Camera.main)
			{
				cameraTransform = Camera.main.transform;
			}
		}

		private void Update()
		{
			Vector3 input = new Vector3();

			if (Input.GetKey(KeyCode.W))
			{
				input.z += 1;
			}

			if (Input.GetKey(KeyCode.S))
			{
				input.z -= 1;
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

			Quaternion cameraOrientation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
			
			// Rotate the input vector to the y-rotation of the camera; so that forward moves in the direction the camera is facing
			input = cameraOrientation * input;

			characterController.Move(Time.deltaTime * speed * input.normalized);

			// Makes the character look towards movement direction
			CachedTransform.LookAt(CachedTransform.position + input);
		}
	}
}