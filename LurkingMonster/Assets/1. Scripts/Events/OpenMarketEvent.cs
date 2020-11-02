using Gameplay.Buildings;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events
{
	public class OpenMarketEvent : VDEvent
	{
		public readonly AbstractBuildingTile BuildingTile;
		
		public OpenMarketEvent(AbstractBuildingTile buildingTile)
		{
			BuildingTile = buildingTile;
		}
	}
}