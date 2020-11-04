using System.Collections.Generic;
using System.Linq;
using Enums.Grid;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadCornerTile : AbstractRoadTile
	{
		public override TileType TileType => TileType.RoadCorner;

		protected override void AddRoadNeighbor(AbstractRoadTile tile)
		{
			base.AddRoadNeighbor(tile);

			if (RoadNeighbors.Count == 2) // Corners should only have 2 roads connected to it
			{
				CachedTransform.LookAt(RoadNeighbors[0].CachedTransform);

				Transform neighbor2 = RoadNeighbors[1].CachedTransform;

				Vector3 delta = neighbor2.position - CachedTransform.position;

				if (-CachedTransform.right != delta.normalized)
				{
					CachedTransform.LookAt(neighbor2);
				}
			}
		}
	}
}