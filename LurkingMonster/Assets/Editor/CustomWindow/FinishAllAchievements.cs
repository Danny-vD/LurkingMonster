﻿using Events;
using UnityEditor;
using UnityEngine;
using VDFramework.EventSystem;

namespace CustomWindow
{
	public class FinishAllAchievements : EditorWindow
	{
		[MenuItem("Achievements/Finish All")]
		public static void ShowWindow()
		{
			GetWindow<FinishAllAchievements>("Finish All Achievements");
		}

		private void OnGUI()
		{
			if (!EditorApplication.isPlaying)
			{
				return;
			}
			
			if (!GUILayout.Button("Finish All Achievements", EditorStyles.miniButtonMid)) return;

			EventManager.Instance.RaiseEvent(new EndGameEvent());
		}
	}
}