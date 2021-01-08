using Enums;

namespace Structs.Market
{
	public struct MainScreenData
	{
		public bool HasDebris;
		public bool HasBuilding;
		public bool HasFoundation;
		public bool HasSoil;

		public BuildingType BuildingType;
		public FoundationType FoundationType;
		public SoilType SoilType;

		public void SetBuildingFoundationSoil(bool hasBuilding, bool hasFoundation, bool hasSoil)
		{
			HasBuilding   = hasBuilding;
			HasFoundation = hasFoundation;
			HasSoil       = hasSoil;
		}

		public void SetBuildingFoundationSoil(BuildingType buildingType, FoundationType foundationType, SoilType soilType)
		{
			BuildingType   = buildingType;
			FoundationType = foundationType;
			SoilType       = soilType;
		}
	}
}