using Enums.Grid;

namespace Grid.Tiles.Road
{
	public class RoadStraightTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadStraight;

		protected override void AddRoadNeighbor(AbstractRoadTile tile)
		{
			base.AddRoadNeighbor(tile);

			CachedTransform.LookAt(tile.CachedTransform, CachedTransform.up);
		}
	}
}