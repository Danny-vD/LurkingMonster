using Enums.Grid;
using Grid;
using UnityEditor;
using UnityEngine;
using VDFramework;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(GridData), true)]
	public class GridDataEditor : Editor
	{
		private GridData gridData;

		private static bool tileDataFoldout;

		//////////////////////////////////////////////////

		private SerializedProperty gridSize;
		private SerializedProperty tileSize;
		private SerializedProperty tileSpacing;

		private SerializedProperty tileData;

		private void OnEnable()
		{
			gridData = target as GridData;

			gridSize    = serializedObject.FindProperty("gridSize");
			tileSize    = serializedObject.FindProperty("tileSize");
			tileSpacing = serializedObject.FindProperty("tileSpacing");
			tileData    = serializedObject.FindProperty("tileData");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(gridSize);
			EditorGUILayout.PropertyField(tileSize);
			EditorGUILayout.PropertyField(tileSpacing);

			if (IsFoldOut(ref tileDataFoldout, "Tile Data"))
			{
				++EditorGUI.indentLevel;

				DrawTileData();

				--EditorGUI.indentLevel;
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawTileData()
		{
			Vector2Int dimensions = gridSize.vector2IntValue;

			tileData.arraySize = dimensions.x * dimensions.y;

			for (int i = 0; i < tileData.arraySize; i++)
			{
				SerializedProperty @struct = tileData.GetArrayElementAtIndex(i);
				SerializedProperty position = @struct.FindPropertyRelative("key");   // Vector2Int
				SerializedProperty tileType = @struct.FindPropertyRelative("value"); // TileType

				int x = i % dimensions.x;
				int y = i / dimensions.x;

				position.vector2IntValue = new Vector2Int(x, y);
				
				FlexibleLabel("{" + x + "," + y + "}");

				TileType type = ConvertIntToEnum<TileType>(tileType.enumValueIndex);
				tileType.enumValueIndex = EnumPopup(ref type, "");

				EditorGUILayout.Space();
			}
		}
	}
}