using Enums.Grid;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadIntersection : AbstractRoadTile
	{
		private const float rotationValue = 90; // Transform.Rotate uses degrees
		
		public override TileType TileType => TileType.RoadIntersection;

		private void Start()
		{
			CachedTransform.Rotate(CachedTransform.up, rotationValue * Random.Range(0, 4));
		}
	}
}