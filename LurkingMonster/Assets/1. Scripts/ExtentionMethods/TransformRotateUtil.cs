using System.Collections.Generic;
using System.Linq;
using Enums.Utility;
using UnityEngine;
using VDFramework.Extensions;

namespace ExtentionMethods
{
	public static class TransformRotateUtil
	{
		private static readonly float rotateStepDegrees; // transform.Rotate uses degrees
		private static readonly List<Direction> allDirections;

		static TransformRotateUtil()
		{
			allDirections = default(Direction).GetValues().ToList();
			
			rotateStepDegrees = 360.0f / allDirections.Count; // Divide a full circle by the amount of directions
		}
		
		/// <summary>
		/// Rotates the transform so that a certain direction will be facing a transform
		/// </summary>
		public static void DirectionLookAt(this Transform transformToRotate, Direction directionToRotate, Transform lookAtTransform)
		{
			transformToRotate.LookAt(lookAtTransform);

			// The Enum values are ordered in a circle.
			// Because we are facing forward now, if we rotate in the negative direction with index of the direction that needs to be forward
			// That direction will be facing what was the forward direction
			int stepsToRotate = allDirections.IndexOf(directionToRotate);
			
			transformToRotate.Rotate(transformToRotate.up, -stepsToRotate * rotateStepDegrees);
		}
		
		/// <summary>
		/// Rotates the transform so that a certain direction will be facing a point
		/// </summary>
		public static void DirectionLookAt(this Transform transformToRotate, Direction directionToRotate, Vector3 lookAtPoint)
		{
			transformToRotate.LookAt(lookAtPoint);

			// The Enum values are ordered in a circle.
			// Because we are facing forward now, if we rotate in the negative direction with index of the direction that needs to be forward
			// That direction will be facing what was the forward direction
			int stepsToRotate = allDirections.IndexOf(directionToRotate);
			
			transformToRotate.Rotate(transformToRotate.up, -stepsToRotate * rotateStepDegrees);
		}

		/// <summary>
		/// Rotates the transform so that a certain direction will be facing another direction
		/// </summary>
		public static void DirectionLookAt(this Transform transformToRotate, Direction directionToRotate, Direction lookAtDirection)
		{
			// The Enum values are ordered in a circle.
			// If we rotate with the delta index of the directions
			// A specified direction will be facing in the new direction
			int stepsToRotate = allDirections.IndexOf(lookAtDirection) - allDirections.IndexOf(directionToRotate);
			
			transformToRotate.Rotate(transformToRotate.up, stepsToRotate * rotateStepDegrees);
		}

		/// <summary>
		/// Rotates the transform so that the forward looks in a certain direction
		/// </summary>
		public static void LookAt(this Transform transformToRotate, Direction lookAtDirection)
		{
			transformToRotate.DirectionLookAt(Direction.Forward, lookAtDirection);
		}
		
		/// <summary>
		/// Returns a normalized vector from the transformPosition facing a certain direction
		/// </summary>
		public static Vector3 GetTransformDirectionVector(this Transform transform, Direction directionToGet)
		{
			Quaternion oldRotation = transform.rotation;

			transform.LookAt(directionToGet);
			Vector3 deltaVector = transform.forward;

			transform.rotation = oldRotation;
			
			return deltaVector.normalized;
		}

		/// <summary>
		/// Checks whether a direction is facing a transform
		/// </summary>
		public static bool DirectionIsFacingTransform(this Transform transform, Direction directionToCheck, Transform shouldBefacing)
		{
			return transform.DirectionIsFacingPoint(directionToCheck, shouldBefacing.position);
		}

		/// <summary>
		/// Checks whether a direction is facing a point
		/// </summary>
		public static bool DirectionIsFacingPoint(this Transform transform, Direction directionToCheck, Vector3 shouldBefacing)
		{
			Vector3 directionToPoint = (shouldBefacing - transform.position).normalized;

			return transform.DirectionIsFacingDirection(directionToCheck, directionToPoint);
		}

		/// <summary>
		/// Checks whether a direction is facing a direction
		/// </summary>
		public static bool DirectionIsFacingDirection(this Transform transform, Direction directionToCheck, Vector3 shouldBefacing)
		{
			Vector3 direction1 = transform.GetTransformDirectionVector(directionToCheck);
			Vector3 direction2 = shouldBefacing.normalized;

			return direction1 == direction2;
		}
	}
}