using System;
using Enums;
using Enums.Grid;
using Grid.Tiles;
using Grid.Tiles.Building;
using Grid.Tiles.Road;

namespace Structs
{
	[Serializable]
	public class TileData
	{
		private TileType tileType;
		private BuildingType buildingType;
		private int buildingTier;
		private SoilType SoilType;
		private FoundationType foundationType;

		public TileData(AbstractTile tile)
		{
			tileType = tile.TileType;
			getType(tile);
		}

		public void getType(AbstractTile tile)
		{
			// Using a switch to check and cast to approriate type directly
			switch (tile)
			{
				case AbstractBuildingTile buildingTile:
					// You can get all your information through this one
					if (buildingTile.Building == null)
					{
						return;
					}
					
					buildingType   = buildingTile.BuildingType;
					buildingTier   = buildingTile.Building.CurrentTier;
					SoilType       = buildingTile.GetSoilType();
					foundationType = buildingTile.GetFoundationType();
					
					break;
				default:
					break;
			}
		}

		public TileType TileType
		{
			get => tileType;
			set => tileType = value;
		}

		public BuildingType BuildingType
		{
			get => buildingType;
			set => buildingType = value;
		}

		public int BuildingTier
		{
			get => buildingTier;
			set => buildingTier = value;
		}

		public SoilType SoilType1
		{
			get => SoilType;
			set => SoilType = value;
		}

		public FoundationType FoundationType
		{
			get => foundationType;
			set => foundationType = value;
		}
	}
}