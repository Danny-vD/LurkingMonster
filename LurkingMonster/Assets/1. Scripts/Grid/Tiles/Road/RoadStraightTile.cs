using Enums.Grid;
using VDFramework.Utility;

namespace Grid.Tiles.Road
{
	public class RoadStraightTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadStraight;

		protected override void AddRoadNeighbor(AbstractRoadTile tile)
		{
			base.AddRoadNeighbor(tile);

			CachedTransform.LookAt(tile.CachedTransform, CachedTransform.up);

			// Get a random boolean to randomly rotate 180°
			bool shouldRotate = RandomUtil.RandomBool();

			if (shouldRotate)
			{
				CachedTransform.Rotate(CachedTransform.up, 180.0f);
			}
		}
	}
}