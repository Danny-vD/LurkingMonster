using Gameplay.Buildings;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events
{
	public class OpenMarketEvent : VDEvent
	{
		public readonly AbstractBuildingTile BuildingTile;
		public readonly Building Building;
		
		public OpenMarketEvent(AbstractBuildingTile buildingTile)
		{
			BuildingTile = buildingTile;
		}

		public OpenMarketEvent(Building building)
		{
			Building = building;
		}
	}
}