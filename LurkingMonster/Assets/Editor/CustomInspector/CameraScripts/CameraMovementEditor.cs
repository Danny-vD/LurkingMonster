using CameraScripts;
using UnityEditor;
using UnityEngine;

namespace CustomInspector.CameraScripts
{
	[CustomEditor(typeof(CameraMovement)), CanEditMultipleObjects]
	public class CameraMovementEditor : Editor
	{
		private CameraZoom zoom;

		private void OnEnable()
		{
			zoom = ((CameraMovement) target).GetComponent<CameraZoom>();
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUILayout.Space();
			DrawHelper(); // Draws a label that shows the 'tipping point' between slowing down and speeding up
		}

		private void DrawHelper()
		{
			Vector2 minMax = zoom.GetMinMaxZoom();

			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField("Speed up zoom");
				EditorGUILayout.LabelField($"{Mathf.Lerp(minMax.x, minMax.y, 0.5f)}", EditorStyles.boldLabel);
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}