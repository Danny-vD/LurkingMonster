using Gameplay.Buildings;
using Grid.Tiles;
using VDFramework.EventSystem;

namespace Events
{
	public class OpenMarketEvent : VDEvent
	{
		public readonly AbstractBuildingTile BuildingTile;
		public readonly Building Building;
		
		public OpenMarketEvent(AbstractBuildingTile buildingTile, Building building)
		{
			BuildingTile = buildingTile;
			Building = building;
		}
	}
}