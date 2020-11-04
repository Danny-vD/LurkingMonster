using System.Collections.Generic;

namespace Grid.Tiles.Road
{
	public abstract class AbstractRoadTile : AbstractTile
	{
		protected readonly List<AbstractRoadTile> RoadNeighbors = new List<AbstractRoadTile>();

		public override void AddNeighbor(AbstractTile tile)
		{
			base.AddNeighbor(tile);

			if (tile is AbstractRoadTile roadTile)
			{
				AddRoadNeighbor(roadTile);
			}
		}

		protected virtual void AddRoadNeighbor(AbstractRoadTile roadTile)
		{
			RoadNeighbors.Add(roadTile);
		}
	}
}