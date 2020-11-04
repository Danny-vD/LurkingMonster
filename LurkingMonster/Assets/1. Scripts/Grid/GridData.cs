using System.Collections.Generic;
using System.Linq;
using Enums.Grid;
using Structs.Grid;
using UnityEngine;
using VDFramework;

namespace Grid
{
	public class GridData : BetterMonoBehaviour
	{
		public Vector2Int GridSize = Vector2Int.one;

		public Vector2 TileSize = new Vector2(10, 10); // 10,10 is the default plane size

		public Vector2 TileSpacing = Vector2.zero;
		
		public List<TileTypePerPosition> TileData = new List<TileTypePerPosition>();

		public void ChangeTile(Vector2Int gridPosition, TileType newType)
		{
			int length = TileData.Count;
			
			for (int i = 0; i < length; i++)
			{
				TileTypePerPosition pair = TileData[i];

				if (pair.Key.Equals(gridPosition))
				{
					pair.Value = newType;
					TileData[i]     = pair;
					break;
				}
			}
		}
	}
}