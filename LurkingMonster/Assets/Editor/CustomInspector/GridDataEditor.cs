using Enums.Grid;
using Grid;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(GridData), true)]
	public class GridDataEditor : Editor
	{
		private static bool tileDataFoldout;

		//////////////////////////////////////////////////

		private SerializedProperty gridSize;
		private SerializedProperty tileSize;
		private SerializedProperty tileSpacing;

		private SerializedProperty tileData;

		private void OnEnable()
		{
			gridSize    = serializedObject.FindProperty("GridSize");
			tileSize    = serializedObject.FindProperty("TileSize");
			tileSpacing = serializedObject.FindProperty("TileSpacing");
			tileData    = serializedObject.FindProperty("TileData");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(gridSize);
			EditorGUILayout.PropertyField(tileSize);
			EditorGUILayout.PropertyField(tileSpacing);

			DrawTileData();

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawTileData()
		{
			IsFoldOut(ref tileDataFoldout, "Tile Data"); // Shows the foldout label, but ignore boolean for now
			++EditorGUI.indentLevel;

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

				if (tileDataFoldout)
				{
					FlexibleLabel("{" + x + "," + y + "}");
				}

				TileType type = ConvertIntToEnum<TileType>(tileType.enumValueIndex);

				if (tileDataFoldout)
				{
					tileType.enumValueIndex = EnumPopup(ref type, string.Empty);

					EditorGUILayout.Space();
				}
			}

			--EditorGUI.indentLevel;
		}
	}
}