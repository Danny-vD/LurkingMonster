using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enums.Grid;
using Grid;
using Singletons;
using Structs.Grid;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

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