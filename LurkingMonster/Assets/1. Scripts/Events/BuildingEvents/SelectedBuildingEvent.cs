using Grid.Tiles;
using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events.BuildingEvents
{
	public class SelectedBuildingEvent : VDEvent<SelectedBuildingEvent>
	{
		public readonly AbstractBuildingTile tile;

		public SelectedBuildingEvent(AbstractBuildingTile tile)
		{
			this.tile = tile;
		}
	}
}