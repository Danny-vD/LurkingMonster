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

		/// <summary>
		/// Set the GridPosition of this tile, intended to only set after instantiating the tile
		/// </summary>
		/// <param name="position"></param>
		public void Initialize(Vector2Int position)
		{
			GridPosition = position;
		}

		/// <summary>
		/// Tell the tile it has a new neighbor
		/// </summary>
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