using Enums.Grid;
using Enums.Utility;
using ExtentionMethods;
using Interfaces.Tiles;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadBridge : AbstractRoadTile, IWaterTile
	{
		[SerializeField]
		private Direction roadDirection = Direction.Right;

		public override TileType TileType => TileType.RoadBridge;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);

			bool shouldRotate = CachedTransform.DirectionIsFacingTransform(Direction.Forward, tile.CachedTransform) ||
								CachedTransform.DirectionIsFacingTransform(Direction.Right, tile.CachedTransform);

			CachedTransform.DirectionLookAtTransform(roadDirection, tile.CachedTransform);

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