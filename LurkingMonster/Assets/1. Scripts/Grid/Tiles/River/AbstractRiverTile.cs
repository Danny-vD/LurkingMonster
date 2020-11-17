using System.Collections.Generic;
using Grid.Tiles.Road;
using VDFramework;

namespace Grid.Tiles.River
{
	public abstract class AbstractRiverTile : AbstractTile
	{
		protected readonly List<AbstractRiverTile> RiverNeighbors = new List<AbstractRiverTile>();

		public override void AddNeighbor(AbstractTile tile)
		{
			base.AddNeighbor(tile);

			if (tile is AbstractRiverTile riverTile)
			{
				AddRiverNeighbor(riverTile);
			}
		}

		protected virtual void AddRiverNeighbor(AbstractRiverTile tile)
		{
			RiverNeighbors.Add(tile);
		}
	}
}