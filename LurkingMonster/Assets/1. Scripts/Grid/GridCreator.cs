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
	public class GridCreator : BetterMonoBehaviour
	{
		//NOTE: the variable can be replaced by SerializedEnumDictionary<TileType, List<GameObject>>
		[SerializeField]
		private List<PrefabsPerTileType> prefabsPerTileTypes = new List<PrefabsPerTileType>();

		/// <summary>
		/// Generate the grid using the GridData attached to this gameobject will return the grid as a 2d array of tiles
		/// </summary>
		public AbstractTile[,] GenerateGrid()
		{
			GridData data = GetComponent<GridData>();

			return data ? GenerateGrid(data, data.CachedTransform) : new AbstractTile[0, 0];
		}

		/// <summary>
		/// Generate the grid using the provided GridData will return the grid as a 2d array of tiles
		/// </summary>
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

				AbstractTile tile = InstantiateTile(data, parent, tileDatum.Value, gridPosition);
				grid[gridPosition.y, gridPosition.x] = tile;
				AddNeighborsToTile(grid, tile);
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

			tile.Initialize(gridPosition);

			return tile;
		}

		private static Vector3 CalculatePosition(GridData data, Transform parent, Vector2Int gridPosition)
		{
			Vector3 position = Vector3.zero;

			float deltaX = gridPosition.x * (data.TileSize.x + data.TileSpacing.x);
			float deltaZ = gridPosition.y * (data.TileSize.y + data.TileSpacing.y);

			position += parent.right * deltaX;
			position += parent.forward * deltaZ;

			return position;
		}

		private static void AddNeighborsToTile(AbstractTile[,] grid, AbstractTile tile)
		{
			Vector2Int gridPosition = tile.GridPosition;

			// Make sure we can never sample below 0,0
			Vector2Int samplePosition = Vector2Int.zero;
			samplePosition.x = Mathf.Max(0, gridPosition.x - 1);
			samplePosition.y = Mathf.Max(0, gridPosition.y - 1);

			AbstractTile neighbor = grid[samplePosition.y, gridPosition.x]; // One below us, in the same column

			if (neighbor != tile)
			{
				tile.AddNeighbor(neighbor);
				neighbor.AddNeighbor(tile);
			}

			neighbor = grid[gridPosition.y, samplePosition.x]; // One before us, in the same row

			if (neighbor != tile)
			{
				tile.AddNeighbor(neighbor);
				neighbor.AddNeighbor(tile);
			}
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

		[ContextMenu("Remove Grid")]
		public void DestroyChildrenImmediate()
		{
			for (int i = CachedTransform.childCount - 1; i >= 0; --i)
			{
				DestroyImmediate(CachedTransform.GetChild(i).gameObject);
			}
		}

		/// <summary>
		/// Will add a KeyValuePair{TileType, List{GameObject}} for every tileType (called by the editor script) 
		/// </summary>
		public void PopulateDictionaries()
		{
			// The variable can be replaced by SerializedEnumDictionary<TileType, List<GameObject>>
			EnumDictionaryUtil.PopulateEnumDictionary<PrefabsPerTileType, TileType, List<GameObject>>(prefabsPerTileTypes);
		}
#endif
	}
}