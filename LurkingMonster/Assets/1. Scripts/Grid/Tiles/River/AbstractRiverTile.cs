using System.Collections.Generic;
using Interfaces.Tiles;

namespace Grid.Tiles.River
{
	public abstract class AbstractRiverTile : AbstractTile, IWaterTile
	{
		protected readonly List<AbstractTile> RiverNeighbors = new List<AbstractTile>();

		public override void AddNeighbor(AbstractTile tile)
		{
			base.AddNeighbor(tile);

			if (tile is IWaterTile)
			{
				AddRiverNeighbor(tile);
			}
		}

		protected virtual void AddRiverNeighbor(AbstractTile tile)
		{
			RiverNeighbors.Add(tile);
		}
	}
}