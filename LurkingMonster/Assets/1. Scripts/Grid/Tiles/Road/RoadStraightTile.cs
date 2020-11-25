using Enums.Grid;
using Enums.Utility;
using ExtentionMethods;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace Grid.Tiles.Road
{
	public class RoadStraightTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadStraight;

		protected override void AddRoadNeighbor(AbstractRoadTile tile)
		{
			base.AddRoadNeighbor(tile);

			if (RoadNeighbors.Count > 1)
			{
				return;
			}

			bool shouldRotate = CachedTransform.DirectionIsFacingTransform(Direction.Forward, tile.CachedTransform) ||
								CachedTransform.DirectionIsFacingTransform(Direction.Right, tile.CachedTransform);

			CachedTransform.LookAt(tile.CachedTransform, CachedTransform.up);

			// Get a random boolean to randomly rotate 180°
			//bool shouldRotate = RandomUtil.RandomBool();

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