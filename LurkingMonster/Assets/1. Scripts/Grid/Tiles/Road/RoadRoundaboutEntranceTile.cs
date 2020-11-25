using System.Linq;
using Enums.Grid;
using Enums.Utility;
using Grid.Tiles.Utility;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadRoundaboutEntranceTile : AbstractRoadTile
	{
		[Header("The endings of the intersection, as seen from the transform")]
		[SerializeField]
		private Direction perpendicular = Direction.Forward;

		public override TileType TileType => TileType.RoadRoundaboutEntrance;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);

			if (RoadNeighbors.Count == 3) // TIntersections should only have 3 roads connected to it
			{
				TileRotateUtil.RotateTIntersection(this, perpendicular, RoadNeighbors.Select(roadTile => roadTile as AbstractTile));
			}
		}
	}
}