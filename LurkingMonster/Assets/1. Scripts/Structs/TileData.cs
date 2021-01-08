using System;
using Enums;
using Enums.Grid;
using Gameplay.Buildings;
using Grid.Tiles;
using Grid.Tiles.Buildings;

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
		private bool hasSoil;
		private bool hasFoundation;
		private bool hasDebris;
		
		private float buildingHealth;
		private float foundationHealth;
		private float soilHealth;

		public TileType TileType => tileType;

		public BuildingType BuildingType => buildingType;

		public int BuildingTier => buildingTier;

		public SoilType SoilType => soilType;

		public FoundationType FoundationType => foundationType;

		public bool HasSoil => hasSoil;
		public bool HasFoundation => hasFoundation;

		public bool HasDebris => hasDebris;

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
				hasSoil       = buildingTile.HasSoil;
				hasFoundation = buildingTile.HasFoundation;
				hasDebris     = buildingTile.HasDebris;

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