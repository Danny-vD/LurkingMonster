using Enums.Grid;
using Grid;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utility.EditorUtils;

namespace CustomInspector.Grid
{
	[CustomEditor(typeof(GridCreator))]
	public class GridCreatorEditor : Editor
	{
		private static bool[] prefabsPerTileTypesFoldout;

		private GridCreator gridCreator;

		private bool prefabsFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty prefabsPerTileTypes;

		private void OnEnable()
		{
			gridCreator = (GridCreator) target;
			gridCreator.PopulateDictionaries();

			prefabsPerTileTypes = serializedObject.FindProperty("prefabsPerTileTypes");

			if (prefabsPerTileTypesFoldout == null || prefabsPerTileTypesFoldout.Length != prefabsPerTileTypes.arraySize)
			{
				prefabsPerTileTypesFoldout = new bool[prefabsPerTileTypes.arraySize];
			}
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawGenerateButton();

			if (IsFoldOut(ref prefabsFoldout, "Prefabs Per Tile"))
			{
				DrawPrefabsPerTileTypes();
			}

			EditorGUILayout.Space();
			DrawSeperatorLine();
			EditorGUILayout.Space();

			DrawDeleteButton();
			
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawGenerateButton()
		{
			if (!GUILayout.Button("Generate Grid", EditorStyles.miniButtonMid)) return;

			gridCreator.GenerateGrid();

			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}

		private void DrawPrefabsPerTileTypes()
		{
			DrawFoldoutKeyValueArray<TileType>(prefabsPerTileTypes, "tileType", "prefabs", prefabsPerTileTypesFoldout,
				DrawPrefabArray);
		}
		
		private static void DrawPrefabArray(int index, SerializedProperty tileType, SerializedProperty prefabs)
		{
			prefabs.arraySize = Mathf.Clamp(EditorGUILayout.IntField("Variations", prefabs.arraySize), 0, int.MaxValue);
			
			for (int i = 0; i < prefabs.arraySize; i++)
			{
				EditorGUILayout.PropertyField(prefabs.GetArrayElementAtIndex(i), new GUIContent($"Prefab"));
			}
		}

		private void DrawDeleteButton()
		{
			if (!GUILayout.Button("Remove Grid", EditorStyles.miniButtonMid)) return;

			gridCreator.DestroyChildrenImmediate();
			
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}