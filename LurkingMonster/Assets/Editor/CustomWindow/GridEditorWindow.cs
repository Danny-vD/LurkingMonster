﻿using System.Collections.Generic;
using System.Linq;
using Enums.Grid;
using Grid;
using Structs.Grid;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomWindow
{
	public class GridEditorWindow : EditorWindow
	{
		[MenuItem("Level Editor/Grid Editor")]
		public static void ShowWindow()
		{
			GetWindow<GridEditorWindow>("Level Editor");
		}

		private static Vector2 scroll;

		private GridData gridData;

		private void OnEnable()
		{
			gridData = FindObjectOfType<GridData>();
		}

		private void OnGUI()
		{
			if (gridData == null)
			{
				TryToFindData();
				return;
			}

			DrawGridProperties();

			DrawGenerateButton();

			scroll = EditorGUILayout.BeginScrollView(scroll, true, true);
			{
				EditorGUILayout.Space(20.0f);

				DrawTileData();

				EditorGUILayout.Space(20.0f);
			}
			EditorGUILayout.EndScrollView();
		}

		private void TryToFindData()
		{
			EditorGUILayout.LabelField("Could not find grid data", EditorStyles.boldLabel);

			if (GUILayout.Button("Find grid data in scene", EditorStyles.miniButtonMid))
			{
				gridData = FindObjectOfType<GridData>();
			}
		}

		private void DrawGridProperties()
		{
			EditorGUILayout.BeginHorizontal();
			{
				FlexibleLabel("Grid Dimensions");
				gridData.GridSize = DrawVector2Int(gridData.GridSize.x, "X", gridData.GridSize.y, "Y");
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				FlexibleLabel("Tile model size");
				gridData.TileSize = DrawVector2(gridData.TileSize.x, "X", gridData.TileSize.y, "Y");
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				FlexibleLabel("Tile spacing");
				gridData.TileSpacing = DrawVector2(gridData.TileSpacing.x, "X", gridData.TileSpacing.y, "Y");
			}
			EditorGUILayout.EndHorizontal();
		}

		private void DrawGenerateButton()
		{
			if (!GUILayout.Button("Generate Grid", EditorStyles.miniButtonMid)) return;

			gridData.GetComponent<GridCreator>().GenerateGrid(gridData, gridData.transform);
		}

		private void DrawTileData()
		{
			int length = gridData.TileData.Count;

			EditorGUILayout.BeginHorizontal();

			// position.width = window width
			float maxWidth = Mathf.Max(80, CalculateMaxItemSizeForLimit(gridData.GridSize.x, position.width));

			List<TileTypePerPosition> tileData = new List<TileTypePerPosition>(gridData.TileData);
			tileData.Sort(TileDataComparer);

			for (int i = 0; i < length; i++)
			{
				if (i % gridData.GridSize.x == 0)
				{
					// Begin new line
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
				}

				TileTypePerPosition tileDatum = tileData[i];
				TileType tileType = tileDatum.Value;

				tileDatum.Value = (TileType) EditorGUILayout.EnumPopup(tileType, GUILayout.MaxWidth(maxWidth));

				// The original is not sorted, so we need to find the respective index
				int index = gridData.TileData.IndexOf(gridData.TileData.First(datum => datum.Key.Equals(tileDatum.Key)));
				
				gridData.TileData[index] = tileDatum;
			}

			EditorGUILayout.EndHorizontal();
		}

		private static float CalculateMaxItemSizeForLimit(int amountOfItems, float limit)
		{
			return limit / amountOfItems;
		}

		private static int TileDataComparer(TileTypePerPosition me, TileTypePerPosition other)
		{
			if (me.Key.Equals(other.Key))
			{
				return 0;
			}

			if (me.Key.y > other.Key.y)
			{
				return -1;
			}

			if (me.Key.y < other.Key.y)
			{
				return 1;
			}

			if (me.Key.x < other.Key.x)
			{
				return -1;
			}

			return 1;
		}
	}
}