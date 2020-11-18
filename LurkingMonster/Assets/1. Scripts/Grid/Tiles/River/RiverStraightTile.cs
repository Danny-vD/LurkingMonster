using Enums.Grid;
using VDFramework.Utility;

namespace Grid.Tiles.River
{
	public class RiverStraightTile : AbstractRiverTile
	{
		public override TileType TileType => TileType.RiverStraight;

		protected override void AddRiverNeighbor(AbstractRiverTile tile)
		{
			base.AddRiverNeighbor(tile);

			if (RiverNeighbors.Count >= 3)
			{
				return;
			}
			
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