using System.Linq;
using Enums.Grid;
using Enums.Utility;
using Grid.Tiles.Utility;
using UnityEngine;

namespace Grid.Tiles.Road
{
	public class RoadCornerTile : AbstractRoadTile
	{
		[Header("The endings of the curve, as seen from the transform")]
		[SerializeField]
		private Direction ending1 = Direction.Forward;
		
		[SerializeField]
		private Direction ending2 = Direction.Left;
		
		public override TileType TileType => TileType.RoadCorner;

		protected override void AddRoadNeighbor(AbstractTile tile)
		{
			base.AddRoadNeighbor(tile);
			
			if (RoadNeighbors.Count == 2) // Corners should only have 2 roads connected to it
			{
				TileRotateUtil.RotateCorner(this, ending1, ending2, RoadNeighbors.Select(roadTile => roadTile as AbstractTile));
			}
		}
	}
}