using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events.BuildingEvents
{
	public class SelectedBuildingEvent : VDEvent<SelectedBuildingEvent>
	{
		public readonly AbstractBuildingTile Tile;

		public SelectedBuildingEvent(AbstractBuildingTile tile)
		{
			Tile = tile;
		}
	}
}