using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events.BuildingEvents
{
	public class SelectedBuildingTileEvent : VDEvent<SelectedBuildingTileEvent>
	{
		public readonly bool SelectedBuildingTile;
		public readonly AbstractBuildingTile Tile;

		public SelectedBuildingTileEvent(AbstractBuildingTile tile, bool selectedBuildingTile)
		{
			Tile                      = tile;
			SelectedBuildingTile = selectedBuildingTile;
		}
	}
}