using System;
using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid.Tiles
{
	public abstract class AbstractTile : BetterMonoBehaviour
	{
		[SerializeField]
		private TileType tileType;
		
		public abstract TileType TileType { get; }

		public Vector2Int GridPosition;

		public void Instantiate(Vector2Int position)
		{
			GridPosition = position;
		}
	}
}