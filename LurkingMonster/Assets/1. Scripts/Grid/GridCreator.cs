using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Grid;
using Grid.Tiles;
using Structs.Grid;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;
using VDFramework.UnityExtensions;
using VDFramework.Utility;

namespace Grid
{
	using VDFramework.EventSystem;

	public class GridCreator : BetterMonoBehaviour
	{
		[SerializeField]
		private List<PrefabsPerTileType> prefabsPerTileTypes = new List<PrefabsPerTileType>();

		public AbstractTile[,] GenerateGrid()
		{
			GridData data = GetComponent<GridData>();

			return data ? GenerateGrid(data, data.CachedTransform) : new AbstractTile[0, 0];
		}

		public AbstractTile[,] GenerateGrid(GridData data, Transform parent)
		{
			DestroyChildren();

			AbstractTile[,] grid = new AbstractTile[data.GridSize.y, data.GridSize.x];

			List<TileTypePerPosition> tileData = data.TileData;
			int length = tileData.Count;

			for (int i = 0; i < length; i++)
			{
				TileTypePerPosition tileDatum = tileData[i];
				Vector2Int gridPosition = tileDatum.Key;

				grid[gridPosition.y, gridPosition.x] = InstantiateTile(data, parent, tileDatum.Value, gridPosition);
			}
			
			return grid;
		}

		private AbstractTile InstantiateTile(GridData data, Transform parent, TileType type, Vector2Int gridPosition)
		{
			GameObject prefab = prefabsPerTileTypes.First(pair => pair.Key.Equals(type)).Value.GetRandomItem();

			if (prefab == null)
			{
				throw new NullReferenceException($"A prefab for {type} is not assigned!");
			}
			
			GameObject instance = Instantiate(prefab, parent);
			instance.transform.position += CalculatePosition(data, parent, gridPosition);

			instance.name = $"{type.ToString()} {gridPosition.ToString()}";
			
			AbstractTile tile = instance.GetComponent<AbstractTile>();

			if (tile == null)
			{
				throw new NullReferenceException($"Prefab {prefab.name} does not have an AbstractTileComponent attached to it!");
			}
			
			tile.Instantiate(gridPosition);
			
			return tile;
		}

		private static Vector3 CalculatePosition(GridData data, Transform parent, Vector2Int gridPosition)
		{
			Vector3 position = Vector3.zero;

			float deltaX = gridPosition.x * data.TileSize.x + gridPosition.x * data.TileSpacing.x;
			float deltaZ = gridPosition.y * data.TileSize.y + gridPosition.y * data.TileSpacing.y;

			position += parent.right * deltaX;
			position += parent.forward * deltaZ;

			return position;
		}

		private void DestroyChildren()
		{
#if UNITY_EDITOR
			if (!UnityEditor.EditorApplication.isPlaying)
			{
				DestroyChildrenImmediate();
			}
			else
#endif
				CachedTransform.DestroyChildren();
		}

#if UNITY_EDITOR
		private void DestroyChildrenImmediate()
		{
			for (int i = CachedTransform.childCount - 1; i >= 0; --i)
			{
				DestroyImmediate(CachedTransform.GetChild(i).gameObject);
			}
		}
		
		public void PopulateDictionaries()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<PrefabsPerTileType, TileType, List<GameObject>>(
				prefabsPerTileTypes);
		}
#endif
	}
}