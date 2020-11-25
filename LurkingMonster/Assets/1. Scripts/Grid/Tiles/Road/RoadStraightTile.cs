using Enums.Grid;
using Enums.Utility;
using ExtentionMethods;

namespace Grid.Tiles.Road
{
	public class RoadStraightTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadStraight;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);

			if (RoadNeighbors.Count > 1)
			{
				return;
			}

			bool shouldRotate = CachedTransform.DirectionIsFacingTransform(Direction.Forward, tile.CachedTransform) ||
								CachedTransform.DirectionIsFacingTransform(Direction.Right, tile.CachedTransform);

			CachedTransform.LookAt(tile.CachedTransform, CachedTransform.up);

			if ((GridPosition.x + GridPosition.y & 1) == 0)
			{
				shouldRotate ^= true;
			}

			if (shouldRotate)
			{
				CachedTransform.Rotate(CachedTransform.up, 180.0f);
			}
		}
	}
}