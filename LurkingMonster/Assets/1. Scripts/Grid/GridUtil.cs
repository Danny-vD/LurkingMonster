using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
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
			GetGridData();

			InstantiateGrid();

			if (UserSettings.SettingsExist && UserSettings.GameData.GridData.Count != 0)
			{
				LoadGameplayElements();
			}
			else
			{
				FirstTimeSetup();
			}

			UserSettings.OnGameQuit += SaveDictionary;
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

		// Go over the data, and set the roads correctly (Corners, Straight, T-Sections etc.)
		private static void SetupData()
		{
		}

		private static void LoadGameplayElements()
		{
			SpawnBuildings();
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
					buildingTile.SetFoundationType(tileData.FoundationType);
					buildingTile.SetBuildingType(tileData.BuildingType);

					if (tileData.HasDebris)
					{
						buildingTile.SpawnDebris(tileData.BuildingType, tileData.BuildingTier);
						continue;
					}

					// If there's a building, the tier would be higher than 0
					if (tileData.BuildingTier > 0)
					{
						buildingTile.SpawnBuilding(false);
						
						BuildingHealth buildingHealth = buildingTile.Building.GetComponent<BuildingHealth>();

						buildingHealth.SetCurrentHealth(tileData.SoilHealth, tileData.FoundationHealth, tileData.BuildingHealth);
					}
					else
					{
						if (tileData.HasSoil)
						{
							buildingTile.SpawnSoil();
							
							if (tileData.HasFoundation)
							{
								buildingTile.SpawnFoundation();
							}	
						}

						continue;
					}

					BuildingUpgrade buildingUpgrade = buildingTile.Building.GetComponent<BuildingUpgrade>();

					while (buildingTile.Building.CurrentTier != tileData.BuildingTier)
					{
						buildingUpgrade.Upgrade();
					}
				}
			}
		}

		private void FirstTimeSetup()
		{
			foreach (AbstractTile tile in grid)
			{
				if (tile is AbstractBuildingTile buildingTile)
				{
					buildingTile.SetSoilType(SoilType.Sand);
					buildingTile.SetFoundationType(FoundationType.Wooden_Poles);
					buildingTile.SetBuildingType(BuildingType.House);

					buildingTile.SpawnBuilding(false);

					BuildingHealth buildingHealth = buildingTile.Building.GetComponent<BuildingHealth>();

					buildingHealth.DamageFoundation(buildingHealth.CurrentFoundationHealth * 0.80f);
					buildingHealth.DamageSoil(buildingHealth.CurrentSoilHealth * 0.75f);
					
					return;
				}
			}
		}
		
		private void GetGridData()
		{
			gridData = GetComponent<GridData>();

			if (UserSettings.SettingsExist && UserSettings.GameData.GridData.Count != 0)
			{
				LoadData();
			}
			else
			{
				SetupData();
			}
		}

		private void InstantiateGrid()
		{
			grid = GetComponent<GridCreator>().GenerateGrid(GridData, CachedTransform);
		}
		
		private static void SaveDictionary()
		{
			UserSettings.GameData.GridData.Clear();

			AbstractTile[,] grid = GridUtil.Grid;

			for (int y = 0; y < grid.GetLength(0); y++)
			{
				for (int x = 0; x < grid.GetLength(1); x++)
				{
					SaveTile(grid[y, x]);
				}
			}
		}

		private static void SaveTile(AbstractTile tile)
		{
			UserSettings.GameData.GridData.Add(tile.GridPosition, new TileData(tile));
		}
	}
}