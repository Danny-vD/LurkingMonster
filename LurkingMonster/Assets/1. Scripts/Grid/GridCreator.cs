using System.Collections.Generic;
using System.Linq;
using Enums.Grid;
using Grid.Tiles;
using Structs.Grid;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace Grid
{
	[RequireComponent(typeof(GridData))]
	public class GridCreator : BetterMonoBehaviour
	{
		private GridData gridData;
		
		[SerializeField]
		private List<PrefabsPerTileType> prefabsPerTileTypes = new List<PrefabsPerTileType>();

		private AbstractTile[,] grid;
		
		private void Awake()
		{
			gridData = GetComponent<GridData>();
			grid = new AbstractTile[gridData.GridSize.y, gridData.GridSize.x];
		}

		private void Start()
		{
			GenerateGrid();
		}

		public void GenerateGrid()
		{
			List<TileTypePerPosition> tileData = gridData.TileData; 
			int length = tileData.Count;
			
			for (int i = 0; i < length; i++)
			{
				TileTypePerPosition tileDatum = tileData[i];
				Vector2Int position = tileDatum.Key;

				grid[position.y, position.x] = InstantiateTile(tileDatum.Value, position);
			}
		}

		private AbstractTile InstantiateTile(TileType type, Vector2Int gridPosition)
		{
			GameObject prefab = prefabsPerTileTypes.First(pair => pair.Key.Equals(type)).Value.GetRandomItem();

			GameObject instance = Instantiate(prefab, CalculatePosition(gridPosition), CachedTransform.rotation);
			return instance.GetComponent<AbstractTile>();
		}

		private Vector3 CalculatePosition(Vector2Int gridPosition)
		{
			Vector3 position = CachedTransform.position;
			
			float deltaX = gridPosition.x * gridData.TileSize.x + gridPosition.x * gridData.TileSpacing.x;
			float deltaZ = gridPosition.y * gridData.TileSize.y + gridPosition.y * gridData.TileSpacing.y;

			position.x += deltaX;
			position.z += deltaZ;

			return position;
		}
		
#if UNITY_EDITOR
		[ContextMenu("PopulateDictionaries")]
		public void PopulateDictionaries()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<PrefabsPerTileType, TileType, List<GameObject>>(
				prefabsPerTileTypes);
		}
#endif
	}
}