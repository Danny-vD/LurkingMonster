using System;
using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid.Tiles
{
	public abstract class AbstractTile : BetterMonoBehaviour
	{
		public abstract TileType TileType { get; }

		public Vector2Int GridPosition { get; private set; }

		public void Instantiate(Vector2Int position)
		{
			GridPosition = position;
		}
	}
}