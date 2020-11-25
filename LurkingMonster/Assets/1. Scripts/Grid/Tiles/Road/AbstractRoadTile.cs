using System.Collections.Generic;
using Interfaces.Tiles;

namespace Grid.Tiles.Road
{
	public abstract class AbstractRoadTile : AbstractTile, IRoadTile
	{
		protected readonly List<AbstractTile> RoadNeighbors = new List<AbstractTile>();

		public override void AddNeighbor(AbstractTile tile)
		{
			base.AddNeighbor(tile);

			if (tile is IRoadTile)
			{
				AddRoadNeighbor(tile);
			}
		}

		protected virtual void AddRoadNeighbor(AbstractTile roadTile)
		{
			RoadNeighbors.Add(roadTile);
		}
	}
}