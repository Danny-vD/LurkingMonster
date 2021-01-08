using Enums.Grid;
using Random = UnityEngine.Random;

namespace Grid.Tiles
{
	public class RoundaboutCentre : AbstractTile
	{
		public override TileType TileType => TileType.RoadRoundaboutCentre;

		private void Start()
		{
			// Rotate randomly, for all 4 possiblities
			CachedTransform.Rotate(CachedTransform.up, Random.Range(0, 4) * 90);
		}
	}
}