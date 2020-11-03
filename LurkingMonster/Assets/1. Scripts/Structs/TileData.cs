﻿using System;
using Enums;
using Enums.Grid;
using Gameplay.Buildings;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using Grid.Tiles.Road;

namespace Structs
{
	[Serializable]
	public class TileData
	{
		private TileType tileType;
		private BuildingType buildingType;
		private int buildingTier;
		private SoilType soilType;
		private FoundationType foundationType;
		private bool foundationExists;
		private bool debrisExists;

		public TileType TileType => tileType;

		public BuildingType BuildingType => buildingType;

		public int BuildingTier => buildingTier;

		public SoilType SoilType => soilType;

		public FoundationType FoundationType => foundationType;

		public bool FoundationExists => foundationExists;
		
		public bool DebrisExists => debrisExists;

		public TileData(AbstractTile tile)
		{
			tileType = tile.TileType;
			GetType(tile);
		}

		public void GetType(AbstractTile tile)
		{
			// check and cast to approriate type directly
			if (tile is AbstractBuildingTile buildingTile)
			{
				foundationExists = buildingTile.HasFoundation;
				debrisExists     = buildingTile.HasDebris;
				
				if (buildingTile.Building == null)
				{
					return;
				}

				buildingType     = buildingTile.GetBuildingType();
				buildingTier     = buildingTile.Building.CurrentTier;
				soilType         = buildingTile.GetSoilType();
				foundationType   = buildingTile.GetFoundationType();
			}
		}
	}
}