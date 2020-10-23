using Enums.Grid;
using Grid;
using Grid.Tiles;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector.Grid
{
	using System.Collections.Generic;
	using System.Linq;

	[CustomEditor(typeof(GridModifier))]
	public class GridModifierEditor : Editor
	{
		private GridModifier gridModifier;
		private static bool regenerateGrid = true;

		private bool showSelectedFoldout;

		//////////////////////////////////////////////////

		private SerializedProperty selectedPositions;
		private SerializedProperty newType;

		private void OnEnable()
		{
			gridModifier = target as GridModifier;

			selectedPositions = serializedObject.FindProperty("selectedPositions");
			newType           = serializedObject.FindProperty("newType");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			GetAllSelectedTiles();

			DrawNewType();

			DrawModifyTilesButton();

			DrawSelectedPositions();

			serializedObject.ApplyModifiedProperties();
		}

		private void GetAllSelectedTiles()
		{
			AbstractTile[] selectedTiles = Selection.GetFiltered<AbstractTile>(SelectionMode.Deep);
			Vector2Int[] gridPositions = new Vector2Int[selectedTiles.Length];

			for (int i = 0; i < selectedTiles.Length; i++)
			{
				AbstractTile tile = selectedTiles[i];
				gridPositions[i] = tile.GridPosition;
			}

			gridModifier.selectedPositions = gridPositions;
		}

		private void DrawNewType()
		{
			TileType tileType = (TileType) newType.enumValueIndex;
			newType.enumValueIndex = EnumPopup(ref tileType, "New type");
		}

		private void DrawModifyTilesButton()
		{
			regenerateGrid = EditorGUILayout.Toggle("Regenerate Grid", regenerateGrid);

			if (!GUILayout.Button("Modify tiles", EditorStyles.toolbarButton)) return;

			gridModifier.ModifyTiles(regenerateGrid);
		}

		private void DrawSelectedPositions()
		{
			int length = selectedPositions.arraySize;

			if (IsFoldOut(ref showSelectedFoldout, $"Selected Positions    [{length}]"))
			{
				for (int i = 0; i < length; i++)
				{
					SerializedProperty gridPosition = selectedPositions.GetArrayElementAtIndex(i); // Vector2Int

					EditorGUILayout.LabelField(gridPosition.vector2IntValue.ToString());
				}
			}
		}
	}
}