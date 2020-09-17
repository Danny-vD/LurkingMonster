using Enums.Grid;
using Grid;
using Grid.Tiles;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector.Grid
{
	[CustomEditor(typeof(GridModifier))]
	public class GridModifierEditor : Editor
	{
		private GridModifier gridModifier;
		private static bool regenerateGrid;
		
		//////////////////////////////////////////////////
		
		private SerializedProperty selectedPositions;
		private SerializedProperty newType;
		
		private void OnEnable()
		{
			gridModifier = target as GridModifier;

			selectedPositions = serializedObject.FindProperty("selectedPositions");
			newType = serializedObject.FindProperty("newType");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			GetAllSelectedTiles();

			DrawNewType();

			DrawModifyTilesButton();
			
			serializedObject.ApplyModifiedProperties();
		}

		private void GetAllSelectedTiles()
		{
			AbstractTile[] selectedTiles = Selection.GetFiltered<AbstractTile>(SelectionMode.Editable);
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
	}
}