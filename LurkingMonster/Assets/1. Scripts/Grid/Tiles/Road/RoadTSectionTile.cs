using System.Linq;
using Enums.Grid;
using Enums.Utility;
using Grid.Tiles.Utility;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadTSectionTile : AbstractRoadTile
	{
		[Header("The endings of the intersection, as seen from the transform")]
		[SerializeField]
		private Direction perpendicular = Direction.Forward;
		
		[SerializeField]
		private Direction ending2 = Direction.Left;
		
		[SerializeField]
		private Direction ending3 = Direction.Right;
		
		public override TileType TileType => TileType.RoadTSection;

		protected override void AddRoadNeighbor(AbstractRoadTile tile)
		{
			base.AddRoadNeighbor(tile);
			
			if (RoadNeighbors.Count == 3) // TIntersections should only have 3 roads connected to it
			{
				TileRotateUtil.RotateTIntersection(this, perpendicular, ending2, ending3, RoadNeighbors.Select(roadTile => roadTile as AbstractTile));
			}
		}
	}
}