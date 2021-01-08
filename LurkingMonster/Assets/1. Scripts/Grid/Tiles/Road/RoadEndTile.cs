using Enums.Grid;

namespace Grid.Tiles.Road
{
	public class RoadEndTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadEnd;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);

			if (RoadNeighbors.Count > 1)
			{
				return;
			}

			CachedTransform.LookAt(tile.CachedTransform, CachedTransform.up);
		}
	}
}