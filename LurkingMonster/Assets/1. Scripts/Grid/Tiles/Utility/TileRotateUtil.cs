using System.Collections.Generic;
using System.Linq;
using Enums.Utility;
using ExtentionMethods;

namespace Grid.Tiles.Utility
{
	public static class TileRotateUtil
	{
		/// <summary>
		///  Rotate a corner tile to match the tiles it should connect to, decided by the ending directions
		/// </summary>
		/// <param name="tile">The tile to rotate</param>
		/// <param name="ending1">The transform direction towards one of the endings of the curve</param>
		/// <param name="ending2">The transform direction towards the other ending of the curve</param>
		/// <param name="connectTo">A collection of the tiles that the curve should connect to</param>
		public static void RotateCorner(AbstractTile tile, Direction ending1, Direction ending2, IEnumerable<AbstractTile> connectTo)
		{
			AbstractTile neighbor1 = connectTo.ElementAt(0);
			AbstractTile neighbor2 = connectTo.ElementAt(1);
			
			tile.CachedTransform.DirectionLookAt(ending1, neighbor1.CachedTransform);

			// Because it's a 90° corner, the other ending either looks away or towards the other neighbor at this point
			// If it's facing away, flip the tile by making ending1 look at the other neighbor
			if (!tile.CachedTransform.DirectionIsFacingTransform(ending2, neighbor2.CachedTransform))
			{
				tile.CachedTransform.DirectionLookAt(ending1, neighbor2.CachedTransform);
			}
		}
	}
}