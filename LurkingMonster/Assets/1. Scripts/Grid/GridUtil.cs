using System.Collections.Generic;
using System.Linq;
using Gameplay.Buildings;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using Singletons;
using Structs;
using Structs.Grid;
using UnityEngine;
using VDFramework;

namespace Grid
{
	[RequireComponent(typeof(GridCreator))]
	public class GridUtil : BetterMonoBehaviour
	{
		private static AbstractTile[,] grid;
		private static GridData gridData;

		public static AbstractTile[,] Grid => grid ?? new AbstractTile[0, 0];

		public static GridData GridData => gridData;

		private void Start()
		{
			gridData = GetComponent<GridData>();

			if (UserSettings.SettingsExist && UserSettings.GameData.GridData.Count != 0)
			{
				LoadData();
			}

			grid = GetComponent<GridCreator>().GenerateGrid();

			if (UserSettings.SettingsExist && UserSettings.GameData.GridData.Count != 0)
			{
				SpawnBuildings();
			}
		}

		private static void LoadData()
		{
			for (int index = 0; index < gridData.TileData.Count; index++)
			{
				TileTypePerPosition tileTypePerPosition = gridData.TileData[index];
				tileTypePerPosition.Value = UserSettings.GameData.GridData[tileTypePerPosition.Key].TileType;

				gridData.TileData[index] = tileTypePerPosition;
			}
		}

		private static void SpawnBuildings()
		{
			for (int y = 0; y < grid.GetLength(0); y++)
			{
				for (int x = 0; x < grid.GetLength(1); x++)
				{
					AbstractTile tile = grid[y, x];

					if (!(tile is AbstractBuildingTile buildingTile))
					{
						continue;
					}

					TileData tileData = UserSettings.GameData.GridData[new Vector2IntSerializable(x, y)];

					buildingTile.SetSoilType(tileData.SoilType);
					buildingTile.SetFoundation(tileData.FoundationType);
					buildingTile.SetBuildingType(tileData.BuildingType);

					if (tileData.BuildingTier > 0)
					{
						buildingTile.SpawnBuilding();
					}
					else
					{
						continue;
					}

					BuildingUpgrade buildingUpgrade = buildingTile.Building.GetComponent<BuildingUpgrade>();

					while (buildingTile.Building.CurrentTier != tileData.BuildingTier)
					{
						buildingUpgrade.Upgrade(false);
					}
				}
			}
		}
	}
}