using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid.Tiles
{
	public abstract class AbstractTile : BetterMonoBehaviour
	{
		public abstract TileType TileType { get; }

		private Vector2Int gridPosition;

		public void Instantiate(Vector2Int position)
		{
			gridPosition = position;
		}
	}
}