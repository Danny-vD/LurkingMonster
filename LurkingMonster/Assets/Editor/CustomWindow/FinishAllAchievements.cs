using System.IO;
using Singletons;
using UnityEditor;
using UnityEngine;

namespace CustomWindow
{
	public class RemoveSaveWindow : EditorWindow
	{
		[MenuItem("SaveGame/Delete")]
		public static void ShowWindow()
		{
			GetWindow<RemoveSaveWindow>("Remove Save");
		}

		private void OnGUI()
		{
			if (UserSettings.SettingsExist)
			{
				if (!GUILayout.Button("Delete Save", EditorStyles.miniButtonMid)) return;

				File.Delete(UserSettings.SavePath);
			}
			else
			{
				GUILayout.Label("No savefile found at " + UserSettings.SavePath);
			}
		}
	}
}