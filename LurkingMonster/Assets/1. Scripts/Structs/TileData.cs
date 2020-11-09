using System;
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
		// TODO: split up between structs?
		
		private TileType tileType;
		private BuildingType buildingType;
		private int buildingTier;
		private SoilType soilType;
		private FoundationType foundationType;
		private bool foundationExists;
		private bool debrisExists;
		
		private float buildingHealth;
		private float foundationHealth;
		private float soilHealth;

		public TileType TileType => tileType;

		public BuildingType BuildingType => buildingType;

		public int BuildingTier => buildingTier;

		public SoilType SoilType => soilType;

		public FoundationType FoundationType => foundationType;

		public bool FoundationExists => foundationExists;

		public bool DebrisExists => debrisExists;

		public float BuildingHealth => buildingHealth;

		public float FoundationHealth => foundationHealth;

		public float SoilHealth => soilHealth;

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

				buildingType   = buildingTile.GetBuildingType();
				buildingTier   = buildingTile.Building.CurrentTier;
				soilType       = buildingTile.GetSoilType();
				foundationType = buildingTile.GetFoundationType();

				BuildingHealth buildingHealth = buildingTile.Building.GetComponent<BuildingHealth>();

				this.buildingHealth = buildingHealth.CurrentBuildingHealth;
				foundationHealth    = buildingHealth.CurrentFoundationHealth;
				soilHealth          = buildingHealth.CurrentSoilHealth;
			}
		}
	}
}