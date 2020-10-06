using Enums.Grid;

namespace Grid.Tiles.Road
{
	public abstract class AbstractRoadTile : AbstractTile
	{
		public override TileType TileType => TileType.Road;
	}
}