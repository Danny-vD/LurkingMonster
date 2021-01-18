using UnityEditor;
using UnityEngine;

namespace CustomWindow.DebugWindows
{
	public class TimeScaleSetterWindow : EditorWindow
	{
		[MenuItem("Debug/Timescale Setter")]
		private static void ShowWindow()
		{
			TimeScaleSetterWindow window = GetWindow<TimeScaleSetterWindow>();
			window.titleContent = new GUIContent("Timescale Setter");
			window.Show();
		}

		private static float defaultTime = 1;
		private static float newTime = 30;

		private static bool modifiedTime = false;

		private void OnGUI()
		{
			defaultTime = EditorGUILayout.FloatField("Default scale", defaultTime);
			newTime     = EditorGUILayout.FloatField("New scale", newTime);

			float currentScale = Time.timeScale;
			EditorGUILayout.LabelField($"Current timescale: {currentScale}");

			if (GUILayout.Button("Switch time", EditorStyles.miniButtonMid))
			{
				Time.timeScale = modifiedTime ? defaultTime : newTime;

				modifiedTime ^= true;
			}

			bool isPaused = currentScale == 0;

			EditorGUILayout.Space(20.0f);

			if (GUILayout.Button(isPaused ? "Resume" : "Pause", EditorStyles.miniButtonMid))
			{
				Time.timeScale = isPaused ? defaultTime : 0;
			}

			EditorGUILayout.Space(50.0f);

			if (GUILayout.Button("Reset", EditorStyles.miniButtonMid))
			{
				defaultTime = 1;
				newTime     = 30;

				Time.timeScale = defaultTime;
			}
		}
	}
}