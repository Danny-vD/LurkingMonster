using System.Collections.Generic;
using Structs.Grid;
using UnityEngine;
using VDFramework;

namespace Grid
{
	public class GridData : BetterMonoBehaviour
	{
		[SerializeField]
		private Vector2Int gridSize = Vector2Int.one;

		[SerializeField]
		private Vector2 tileSize = Vector2.one;

		[SerializeField]
		private Vector2 tileSpacing = Vector2.zero;
		
		[SerializeField]
		private List<TileTypePerPosition> tileData = new List<TileTypePerPosition>();

		public Vector2Int GridSize => gridSize;
		public Vector2 TileSize => tileSize;
		public Vector2 TileSpacing => tileSpacing;
		
		// Return a copy of the list
		public List<TileTypePerPosition> TileData => new List<TileTypePerPosition>(tileData);
		
	}
}