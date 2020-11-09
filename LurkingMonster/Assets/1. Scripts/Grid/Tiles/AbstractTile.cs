using System.Collections.Generic;
using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid.Tiles
{
	public abstract class AbstractTile : BetterMonoBehaviour
	{
		// Set the underlaying array to 4, as that is the most common amount of neighbors
		protected readonly List<AbstractTile> Neighbors = new List<AbstractTile>(4);
		
		public abstract TileType TileType { get; }

		public Vector2Int GridPosition { get; private set; }

		public void Instantiate(Vector2Int position)
		{
			GridPosition = position;
		}

		public virtual void AddNeighbor(AbstractTile tile)
		{
			if (Neighbors.Contains(tile))
			{
				return;
			}

			Neighbors.Add(tile);
		}
	}
}