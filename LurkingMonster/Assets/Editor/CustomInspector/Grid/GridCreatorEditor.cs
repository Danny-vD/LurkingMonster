using Enums.Grid;
using Grid;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
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
			gridCreator = target as GridCreator;
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

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawGenerateButton()
		{
			if (!GUILayout.Button("Generate Grid", EditorStyles.toolbarButton)) return;

			gridCreator.GenerateGrid();
		}

		private void DrawPrefabsPerTileTypes()
		{
			DrawFoldoutKeyValueArray<TileType>(prefabsPerTileTypes, "tileType", "prefabs", prefabsPerTileTypesFoldout,
				new GUIContent("Prefabs"));
		}
	}
}