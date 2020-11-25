using Enums.Grid;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadIntersection : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadIntersection;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);
			
			CachedTransform.Rotate(CachedTransform.up, Random.Range(0, 4) * 90.0f);
		}
	}
}