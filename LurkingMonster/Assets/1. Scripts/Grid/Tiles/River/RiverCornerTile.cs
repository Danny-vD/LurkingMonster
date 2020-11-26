using System.Linq;
using Enums.Grid;
using Enums.Utility;
using Grid.Tiles.Utility;
using UnityEngine;

namespace Grid.Tiles.River
{
	public class RiverCornerTile : AbstractRiverTile
	{	
		[Header("The endings of the curve, as seen from the transform")]
		[SerializeField]
		private Direction ending1 = Direction.Forward;
		
		[SerializeField]
		private Direction ending2 = Direction.Left;
		
		public override TileType TileType => TileType.RiverCorner;

		protected override void AddRiverNeighbor(AbstractTile tile)
		{
			base.AddRiverNeighbor(tile);
			
			if (RiverNeighbors.Count == 2) // Corners should only have 2 tiles connected to it
			{
				TileRotateUtil.RotateCorner(this, ending1, ending2, RiverNeighbors.Select(roadTile => roadTile as AbstractTile));
			}
		}
	}
}